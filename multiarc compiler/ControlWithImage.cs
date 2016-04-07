using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MultiArc_Compiler
{
    public class ControlWithImage : DropableControl
    {
        private readonly Bitmap _image;

        private bool _transparent = false;

        private ContextMenuStrip _menu = new ContextMenuStrip();

        private bool _drawn = false;

        public ControlWithImage(Bitmap image)
        {
            _image = image;
            Size = image.Size;
            Paint += Draw;
            _menu.Items.Add("Remove");
            _menu.ItemClicked += MenuItemClicked;
        }

        private void MenuItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Parent.Controls.Remove(this);
        }

        protected override void MouseDownAction(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DoDragDrop(this, DragDropEffects.Move);
            }
            else
            {
                _menu.Show(this, new Point(((MouseEventArgs)e).X, ((MouseEventArgs)e).Y));
            }
        }

        public void MakeTransparent()
        {
            _transparent = true;
        }

        private void Draw(object sender, PaintEventArgs e)
        {
            var graphics = this.CreateGraphics();
            
            if (_transparent)
            {
                _image.MakeTransparent();
                SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                SetStyle(ControlStyles.Opaque, true);
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

                        if ((IsWhiteOrTransparent(leftColor) || IsWhiteOrTransparent(rightColor) || IsWhiteOrTransparent(upperColor) || IsWhiteOrTransparent(bottomColor)) 
                            && !IsWhiteOrTransparent(color) && !points.Any(p => p.X == x && p.Y == y))
                        {
                            points.Add(new Point(x, y));
                        }
                    }
                }

                // TODO Try ordering points from left to right, then to bottom, then to left, then to top again
                // TODO Example: {(1, 1), (1, 5), (5, 5), (5, 1)}
                // TODO Also try to make panel transparent
                //Size = new Size(rightBorder - leftBorder + 1, lowerBorder - upperBorder + 1);
                graphics.DrawImage(_image, new Rectangle(0, 0, rightBorder - leftBorder + 1, lowerBorder - upperBorder + 1), new Rectangle(leftBorder, upperBorder, rightBorder - leftBorder + 1, lowerBorder - upperBorder + 1), GraphicsUnit.Pixel);

                GraphicsPath path = new GraphicsPath();

                var orderedPoints = new List<Point>();
                var currentPoint = points.OrderBy(p => p.Y).ThenBy(p => p.X).First();
                var firstPoint = currentPoint;
                orderedPoints.Add(new Point(currentPoint.X > 0 ? currentPoint.X - 1 : currentPoint.X, currentPoint.Y > 0 ? currentPoint.Y - 1 : currentPoint.Y));
                orderedPoints.Add(currentPoint);

                for (int i = 0; i < points.Count() - 1; i++)
                {
                    if (points.Any(p => (p.X == currentPoint.X - 1) && (p.Y == currentPoint.Y)))
                    {
                        var point = points.FirstOrDefault(p => (p.X == currentPoint.X - 1) && (p.Y == currentPoint.Y));
                        if (point != null && !orderedPoints.Contains(point))
                        {
                            currentPoint = point;
                            orderedPoints.Add(currentPoint);
                            continue;
                        }
                    }

                    if (points.Any(p => p.X == (currentPoint.X - 1) && p.Y == (currentPoint.Y - 1)))
                    {
                        var point = points.First(p => p.X == (currentPoint.X - 1) && p.Y == (currentPoint.Y - 1));
                        if (point != null && !orderedPoints.Contains(point))
                        {
                            currentPoint = point;
                            orderedPoints.Add(currentPoint);
                            continue;
                        }
                    }

                    if (points.Any(p => p.X == currentPoint.X && (p.Y == currentPoint.Y - 1)))
                    {
                        var point = points.First(p => p.X == currentPoint.X && (p.Y == currentPoint.Y - 1));
                        if (point != null && !orderedPoints.Contains(point))
                        {
                            currentPoint = point;
                            orderedPoints.Add(currentPoint);
                            continue;
                        }
                    }

                    if (points.Any(p => p.X == (currentPoint.X + 1) && (p.Y == currentPoint.Y - 1))) {
                        var point = points.First(p => p.X == (currentPoint.X + 1) && (p.Y == currentPoint.Y - 1));
                        if (point != null && !orderedPoints.Contains(point))
                        {
                            currentPoint = point;
                            orderedPoints.Add(currentPoint);
                            continue;
                        }
                    }

                    if (points.Any(p => p.X == (currentPoint.X + 1) && p.Y == currentPoint.Y))
                    {
                        var point = points.FirstOrDefault(p => p.X == (currentPoint.X + 1) && p.Y == currentPoint.Y);
                        if (point != null && !orderedPoints.Contains(point))
                        {
                            currentPoint = point;
                            orderedPoints.Add(currentPoint);
                            continue;
                        }
                    }

                    if (points.Any(p => (p.X == currentPoint.X + 1) && (p.Y == currentPoint.Y + 1)))
                    {
                        var point = points.FirstOrDefault(p => (p.X == currentPoint.X + 1) && (p.Y == currentPoint.Y + 1));
                        if (point != null && !orderedPoints.Contains(point))
                        {
                            currentPoint = point;
                            orderedPoints.Add(currentPoint);
                            continue;
                        }
                    }

                    if (points.Any(p => p.X == currentPoint.X && (p.Y == currentPoint.Y + 1)))
                    {
                        var point = points.FirstOrDefault(p => p.X == currentPoint.X && (p.Y == currentPoint.Y + 1));
                        if (point != null && !orderedPoints.Contains(point))
                        {
                            currentPoint = point;
                            orderedPoints.Add(currentPoint);
                            continue;
                        }
                    }

                    if (points.Any(p => (p.X == currentPoint.X - 1) && (p.Y == currentPoint.Y + 1)))
                    {
                        var point = points.FirstOrDefault(p => (p.X == currentPoint.X - 1) && (p.Y == currentPoint.Y + 1));
                        if (point != null && !orderedPoints.Contains(point))
                        {
                            currentPoint = point;
                            orderedPoints.Add(currentPoint);
                            continue;
                        }
                    }
                }

                orderedPoints.Remove(firstPoint);
                //points = points.OrderBy(p => p.X).ThenBy(p => p.Y).ToList();
                path.AddClosedCurve(orderedPoints.ToArray());
                Region = new Region(path);
                Size = new Size(rightBorder - leftBorder + 1, lowerBorder - upperBorder + 1);
            }
            else
            {
                graphics.DrawImage(_image, new Point(0, 0));
            }
        }

        private static bool IsWhiteOrTransparent(int color)
        {
            return color == Color.White.ToArgb() || color == 0;
        }
    }
}
