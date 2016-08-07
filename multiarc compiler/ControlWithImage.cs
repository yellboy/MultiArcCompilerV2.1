using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace MultiArc_Compiler
{
    public class ControlWithImage : NonPinDropableControl, ICloneable
    {
        private new const string BringToFront = "Bring to Front";
        private new const string SendToBack = "Send to Back";
        private const string Copy = "Copy     Ctrl + C";
        private const string Remove = "Remove        Del";

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

        private bool _wasSelected;

        private bool[ , ] _selectionApplied;

        public bool Selected
        {
            get
            {
                return base.Selected;
            }
            set
            {
                _wasSelected = base.Selected;
                if (_image != null)
                {
                    _selectionApplied = new bool[_image.Width, _image.Height];
                } 

                base.Selected = value;
            }
        }

        private List<Point> Points;

        private bool _firstDrawing = true;

        private int _leftBorder;
        private int _rightBorder;
        private int _lowerBorder;
        private int _upperBorder;

        public ControlWithImage(Bitmap image, Designer designer)
        {
            _image = image;
            _selectionApplied = new bool[_image.Width, _image.Height];
            _designer = designer;
            Size = image.Size;
            Paint += Draw;

            _menu.Items.Add(BringToFront);
            _menu.Items.Add(SendToBack);
            _menu.Items.Add(new ToolStripSeparator());
            _menu.Items.Add(Copy);
            _menu.Items.Add(Remove);
            _menu.ItemClicked += MenuItemClicked;
        }

        private void MenuItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case BringToFront:
                    _designer.BringToFront(this);
                    break;
                case SendToBack:
                    _designer.SendToBack(this);
                    break;
                case Copy:
                    ParentPanel.DoTheCopy(this);
                    break;
                case Remove:
                    ParentPanel.DoTheRemove();
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
            
            if (_wasSelected != Selected)
            {
                for (var x = 0; x < Width; x++)
                {
                    for (var y = 0; y < Height; y++)
                    {
                        var pixel = _image.GetPixel(x, y);

                        if (!IsWhiteOrTransparent(pixel.ToArgb()))
                        {
                            if (_selectionApplied != null && !_selectionApplied[x, y])
                            {
                                _selectionApplied[x, y] = true;

                                if (!_wasSelected && Selected)
                                {
                                    var color = Color.FromArgb(pixel.A, pixel.R, pixel.G + 50 < 255 ? pixel.G + 50 : 255, pixel.B);
                                    _image.SetPixel(x, y, color);
                                }

                                if (_wasSelected && !Selected)
                                {
                                    var color = Color.FromArgb(pixel.A, pixel.R, pixel.G - 50 > 0 ? pixel.G - 50 : 0, pixel.B);
                                    _image.SetPixel(x, y, color);
                                }
                            }
                        }
                    }
                }
            }

            if (Transparent)
            {
                if (_firstDrawing)
                {
                    _upperBorder = _image.Size.Height;
                    _leftBorder = _image.Size.Width;
                    _lowerBorder = 0;
                    _rightBorder = 0;

                    _image.MakeTransparent();
                    BackColor = Color.Transparent;
                    List<Point> points = new List<Point>();
                    for (int x = 0; x < _image.Size.Width; x++)
                    {
                        for (int y = 0; y < _image.Size.Height; y++)
                        {
                            var pixel = _image.GetPixel(x, y);
                            var color = pixel.ToArgb();
                            if (!IsWhiteOrTransparent(color))
                            {
                                if (y < _upperBorder)
                                {
                                    _upperBorder = y;
                                }
                                if (x < _leftBorder)
                                {
                                    _leftBorder = x;
                                }
                                if (y > _lowerBorder)
                                {
                                    _lowerBorder = y;
                                }
                                if (x > _rightBorder)
                                {
                                    _rightBorder = x;
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

                    Points = orderedPoints;

                    GraphicsPath path = new GraphicsPath(orderedPoints.ToArray(), types);
                    Region = new Region(path);
                    Size = new Size(_rightBorder - _leftBorder + 1, _lowerBorder - _upperBorder + 1);
                }

                graphics.DrawImage(_image, new Rectangle(0, 0, _rightBorder - _leftBorder + 1, _lowerBorder - _upperBorder + 1), new Rectangle(_leftBorder, _upperBorder, _rightBorder - _leftBorder + 1, _lowerBorder - _upperBorder + 1), GraphicsUnit.Pixel);
                _firstDrawing = false;
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

        public override object Clone()
        {
            var clone = new ControlWithImage(GetOriginalImage(), _designer)
            {
                Transparent = Transparent,
                _addedToComponent = _addedToComponent,
                Region = Region,
                Location = Location
            };

            return clone;
        }

        private Bitmap GetOriginalImage()
        {
            var originalImage = (Bitmap)_image.Clone();

            if (Selected)
            {
                for (var x = 0; x < Width; x++)
                {
                    for (var y = 0; y < Height; y++)
                    {
                        var pixel = originalImage.GetPixel(x, y);
                        if (!IsWhiteOrTransparent(pixel.ToArgb()))
                        {
                            var color = Color.FromArgb(pixel.A, pixel.R, pixel.G - 50 > 0 ? pixel.G - 50 : 0, pixel.B);
                            originalImage.SetPixel(x, y, color);
                        }
                    }
                }
            }

            return originalImage;
        }

        public void DisposeImage()
        {
            _image.Dispose();
        }

        public override bool IsPartialySelected(Rectangle rectangle)
        {
            var rectangleRegion = new Region(rectangle);
            foreach (var p in Points)
            {
                var point = AdaptPointCoordinates(p);

                if (PointInsideRectangle(point, rectangle))
                {
                    return true;
                }
            }

            return false;
        }

        private Point AdaptPointCoordinates(Point p)
        {
            var hasPanelForParent = Parent is Panel;
            var x = p.X + (hasPanelForParent ? Location.X : Parent.Location.X);
            var y = p.Y + (hasPanelForParent ? Location.Y : Parent.Location.Y); ;
            if (!(Parent is Panel))
            {
                x += Parent.Location.X;
                y += Parent.Location.Y;
            }

            var point = new Point(x, y);
            return point;
        }

        public override bool IsCompletelySelected(Rectangle rectangle)
        {
            foreach (var p in Points)
            {
                var point = AdaptPointCoordinates(p);

                if (!PointInsideRectangle(point, rectangle))
                {
                    return false;
                }
            }

            return true;
        }

        private bool PointInsideRectangle(Point point, Rectangle rectangle)
        {
            var rectanglePoints = GetRectanglePoints(rectangle);

            return rectanglePoints[0].X <= point.X && rectanglePoints[0].Y <= point.Y &&
                   rectanglePoints[1].X <= point.X && rectanglePoints[1].Y >= point.Y &&
                   rectanglePoints[2].X >= point.X && rectanglePoints[2].Y <= point.Y &&
                   rectanglePoints[3].X >= point.X && rectanglePoints[3].Y >= point.Y;
        }
    }
}
