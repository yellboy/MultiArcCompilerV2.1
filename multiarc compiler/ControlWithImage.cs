using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace MultiArc_Compiler
{
    public class ControlWithImage : DropableControl, ICloneable
    {
        private readonly Bitmap _image;

        public bool Transparent { get; set; }

        private readonly ContextMenuStrip _menu = new ContextMenuStrip();
        
        private bool _addedToComponent;

        private readonly Designer _designer;

        public int Level { get; set; }

        public byte[] ImageStream
        {
            get
            {
                ImageConverter converter = new ImageConverter();
                return (byte[])converter.ConvertTo(_image, typeof(byte[]));
            }
        }

        public ControlWithImage(Bitmap image, Designer designer)
        {
            _image = image;
            _designer = designer;
            Size = image.Size;
            Paint += Draw;

            _menu.Items.Add("Bring to front");
            _menu.Items.Add("Send to back"); 
            _menu.Items.Add(new ToolStripSeparator());
            _menu.Items.Add("Remove");
            _menu.ItemClicked += MenuItemClicked;
        }

        private void MenuItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "Bring to front":
                    _designer.BringToFront(this);
                    break;
                case "Send to back":
                    _designer.SendToBack(this);
                    break;
                case "Remove":
                    _designer.Remove(this);
                    break;
            }
        }

        public void AddToSystemComponent(SystemComponent component)
        {
            component.Controls.Add(this);
            _addedToComponent = true;
        }

        public override void MouseDownAction(MouseEventArgs e)
        {
            if (_addedToComponent)
            {
                var parent = (SystemComponent)Parent;
                parent.ClickedX = Location.X + ClickedX;
                parent.ClickedY = Location.Y + ClickedY;
                parent.SelectControl();
                parent.MouseDownAction(e);
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                DoDragDrop(this, DragDropEffects.Move);
            }
            else
            {
                _menu.Show(this, new Point(e.X, e.Y));
            }
        }

        public void MakeTransparent()
        {
            Transparent = true;
        }

        private void Draw(object sender, PaintEventArgs e)
        {
            Draw();
        }

        public void Draw()
        {
            var graphics = CreateGraphics();
            
            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    var pixel = _image.GetPixel(x, y);
                    if (pixel.ToArgb() == Color.Black.ToArgb())
                    {
                        _image.SetPixel(x, y, DefaultPen.Color);
                    }
                }
            }

            if (Transparent)
            {
                _image.MakeTransparent();
                BackColor = Color.Transparent;
                int upperBorder = _image.Size.Height;
                int leftBorder = _image.Size.Width;
                int lowerBorder = 0;
                int rightBorder = 0;
                List<Point> points = new List<Point>();
                for (int x = 0; x < _image.Size.Width; x++)
                {
                    for (int y = 0; y < _image.Size.Height; y++)
                    {
                        var pixel = _image.GetPixel(x, y);
                        var color = pixel.ToArgb();
                        if (!IsWhiteOrTransparent(color))
                        {
                            if (y < upperBorder)
                            {
                                upperBorder = y;
                            }
                            if (x < leftBorder)
                            {
                                leftBorder = x;
                            }
                            if (y > lowerBorder)
                            {
                                lowerBorder = y;
                            }
                            if (x > rightBorder)
                            {
                                rightBorder = x;
                            }
                        }

                        var leftColor = x == 0 ? 0 : _image.GetPixel(x - 1, y).ToArgb();
                        var rightColor = x == _image.Width - 1 ? 0 : _image.GetPixel(x + 1, y).ToArgb();
                        var upperColor = y == 0 ? 0 : _image.GetPixel(x, y - 1).ToArgb();
                        var bottomColor = y == _image.Height - 1 ? 0 : _image.GetPixel(x, y + 1).ToArgb();

                        if ((!IsWhiteOrTransparent(leftColor) || !IsWhiteOrTransparent(rightColor) || !IsWhiteOrTransparent(upperColor) || !IsWhiteOrTransparent(bottomColor))
                            && IsWhiteOrTransparent(color) && !points.Any(p => p.X == x && p.Y == y))
                        {
                            points.Add(new Point(x, y));
                        }
                    }
                }

                graphics.DrawImage(_image, new Rectangle(0, 0, rightBorder - leftBorder + 1, lowerBorder - upperBorder + 1), new Rectangle(leftBorder, upperBorder, rightBorder - leftBorder + 1, lowerBorder - upperBorder + 1), GraphicsUnit.Pixel);

                var orderedPoints = new List<Point>();
                var currentPoint = points.OrderBy(p => p.Y).ThenBy(p => p.X).First();
                orderedPoints.Add(currentPoint);

                for (int i = 0; i < points.Count() - 1; i++)
                {
                    if (points.Any(p => (p.X == currentPoint.X - 1) && (p.Y == currentPoint.Y)))
                    {
                        var point = points.FirstOrDefault(p => (p.X == currentPoint.X - 1) && (p.Y == currentPoint.Y));
                        if (!orderedPoints.Contains(point))
                        {
                            currentPoint = point;
                            orderedPoints.Add(currentPoint);
                            continue;
                        }
                    }

                    if (points.Any(p => p.X == (currentPoint.X - 1) && p.Y == (currentPoint.Y - 1)))
                    {
                        var point = points.FirstOrDefault(p => p.X == (currentPoint.X - 1) && p.Y == (currentPoint.Y - 1));
                        if (!orderedPoints.Contains(point))
                        {
                            currentPoint = point;
                            orderedPoints.Add(currentPoint);
                            continue;
                        }
                    }

                    if (points.Any(p => p.X == currentPoint.X && (p.Y == currentPoint.Y - 1)))
                    {
                        var point = points.First(p => p.X == currentPoint.X && (p.Y == currentPoint.Y - 1));
                        if (!orderedPoints.Contains(point))
                        {
                            currentPoint = point;
                            orderedPoints.Add(currentPoint);
                            continue;
                        }
                    }

                    if (points.Any(p => p.X == (currentPoint.X + 1) && (p.Y == currentPoint.Y - 1)))
                    {
                        var point = points.First(p => p.X == (currentPoint.X + 1) && (p.Y == currentPoint.Y - 1));
                        if (!orderedPoints.Contains(point))
                        {
                            currentPoint = point;
                            orderedPoints.Add(currentPoint);
                            continue;
                        }
                    }

                    if (points.Any(p => p.X == (currentPoint.X + 1) && p.Y == currentPoint.Y))
                    {
                        var point = points.FirstOrDefault(p => p.X == (currentPoint.X + 1) && p.Y == currentPoint.Y);
                        if (!orderedPoints.Contains(point))
                        {
                            currentPoint = point;
                            orderedPoints.Add(currentPoint);
                            continue;
                        }
                    }

                    if (points.Any(p => (p.X == currentPoint.X + 1) && (p.Y == currentPoint.Y + 1)))
                    {
                        var point = points.FirstOrDefault(p => (p.X == currentPoint.X + 1) && (p.Y == currentPoint.Y + 1));
                        if (!orderedPoints.Contains(point))
                        {
                            currentPoint = point;
                            orderedPoints.Add(currentPoint);
                            continue;
                        }
                    }

                    if (points.Any(p => p.X == currentPoint.X && (p.Y == currentPoint.Y + 1)))
                    {
                        var point = points.FirstOrDefault(p => p.X == currentPoint.X && (p.Y == currentPoint.Y + 1));
                        if (!orderedPoints.Contains(point))
                        {
                            currentPoint = point;
                            orderedPoints.Add(currentPoint);
                            continue;
                        }
                    }

                    if (points.Any(p => (p.X == currentPoint.X - 1) && (p.Y == currentPoint.Y + 1)))
                    {
                        var point = points.FirstOrDefault(p => (p.X == currentPoint.X - 1) && (p.Y == currentPoint.Y + 1));
                        if (!orderedPoints.Contains(point))
                        {
                            currentPoint = point;
                            orderedPoints.Add(currentPoint);
                        }
                    }
                }

                var types = new byte[orderedPoints.Count];
                types[0] = 0;
                for (int i = 1; i < types.Length; i++)
                {
                    types[i] = 1;
                }

                GraphicsPath path = new GraphicsPath(orderedPoints.ToArray(), types);
                Region = new Region(path);
                Size = new Size(rightBorder - leftBorder + 1, lowerBorder - upperBorder + 1);
            }
            else
            {
                graphics.DrawImage(_image, new Point(0, 0));
                Region = new Region(new Rectangle(0, 0, _image.Width, _image.Height));
            }
        }

        private static bool IsWhiteOrTransparent(int color)
        {
            return color == Color.White.ToArgb() || color == 0;
        }

        public object Clone()
        {
            var clone = new ControlWithImage(_image, _designer)
            {
                Transparent = Transparent,
                _addedToComponent = _addedToComponent,
                Region = Region,
                Location = Location
            };

            return clone;
        }

        public void DisposeImage()
        {
            _image.Dispose();
        }
    }
}
