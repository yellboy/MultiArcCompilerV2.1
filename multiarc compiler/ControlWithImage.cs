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

                //Size = new Size(rightBorder - leftBorder + 1, lowerBorder - upperBorder + 1);
                graphics.DrawImage(_image, new Rectangle(0, 0, rightBorder - leftBorder + 1, lowerBorder - upperBorder + 1), new Rectangle(leftBorder, upperBorder, rightBorder - leftBorder + 1, lowerBorder - upperBorder + 1), GraphicsUnit.Pixel);

                GraphicsPath path = new GraphicsPath();
                points = points.OrderBy(p => p.X).ThenBy(p => p.Y).ToList();
                path.AddCurve(points.ToArray(), 1);
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
