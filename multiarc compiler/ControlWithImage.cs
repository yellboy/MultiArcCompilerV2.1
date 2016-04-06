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
                points = new List<Point>
                {
                    new Point(1, 1),
                    new Point(2, 1),
                    new Point(3, 1),
                    new Point(4, 1),
                    new Point(5, 1),
                    new Point(6, 1),
                    new Point(7, 1),
                    new Point(8, 1),
                    new Point(9, 1),
                    new Point(10, 1),
                    new Point(11, 1),
                    new Point(12, 1),
                    new Point(13, 1),
                    new Point(14, 1),
                    new Point(15, 1),
                    new Point(16, 1),
                    new Point(17, 1),
                    new Point(18, 1),
                    new Point(19, 1),
                    new Point(19, 2),
                    new Point(19, 3),
                    new Point(19, 4),
                    new Point(19, 5),
                    new Point(19, 6),
                    new Point(19, 7),
                    new Point(19, 8),
                    new Point(19, 9),
                    new Point(19, 10),
                    new Point(19, 11),
                    new Point(19, 12),
                    new Point(19, 13),
                    new Point(19, 14),
                    new Point(19, 15),
                    new Point(19, 16),
                    new Point(19, 17),
                    new Point(19, 18),
                    new Point(19, 19),
                    new Point(18, 19),
                    new Point(17, 19),
                    new Point(16, 19),
                    new Point(15, 19),
                    new Point(14, 19),
                    new Point(13, 19),
                    new Point(12, 19),
                    new Point(11, 19),
                    new Point(10, 19),
                    new Point(9, 19),
                    new Point(8, 19),
                    new Point(7, 19),
                    new Point(6, 19),
                    new Point(5, 19),
                    new Point(4, 19),
                    new Point(3, 19),
                    new Point(2, 19),
                    new Point(1, 19),
                    new Point(1, 18),
                    new Point(1, 17),
                    new Point(1, 16),
                    new Point(1, 15),
                    new Point(1, 14),
                    new Point(1, 13),
                    new Point(1, 12),
                    new Point(1, 11),
                    new Point(1, 10),
                    new Point(1, 9),
                    new Point(1, 8),
                    new Point(1, 7),
                    new Point(1, 6),
                    new Point(1, 5),
                    new Point(1, 4),
                    new Point(1, 3),
                    new Point(1, 2),
                    new Point(1, 1)
                };
                var orderedPoints = new List<Point>();
                var currentPoint = points.OrderBy(p => p.Y).ThenBy(p => p.X).First();
                orderedPoints.Add(currentPoint);
                
                //points = points.OrderBy(p => p.X).ThenBy(p => p.Y).ToList();
                path.AddClosedCurve(points.ToArray());
                Region = new Region(path);
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
