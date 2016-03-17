using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MultiArc_Compiler
{
    public class ControlWithImage : Control
    {
        private Bitmap _image;

        private bool _transparent = false;

        public ControlWithImage(Bitmap image)
        {
            _image = image;
            Size = image.Size;
            Paint += Draw;
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
                int upperBorder = _image.Size.Height;
                int leftBorder = _image.Size.Width;
                int lowerBorder = 0;
                int rightBorder = 0;
                for (int x = 0; x < _image.Size.Width; x++)
                {
                    for (int y = 0; y < _image.Size.Height; y++)
                    {
                        var pixel = _image.GetPixel(x, y);
                        var color = pixel.ToArgb();
                        var white = Color.White.ToArgb();
                        if (color != white)
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
                    }
                }
                Size = new Size(rightBorder - leftBorder + 1, lowerBorder - upperBorder + 1);
                _image.MakeTransparent();
                graphics.DrawImage(_image, new Rectangle(0, 0, rightBorder - leftBorder + 1, lowerBorder - upperBorder + 1), new Rectangle(leftBorder, upperBorder, rightBorder - leftBorder + 1, lowerBorder - upperBorder + 1), GraphicsUnit.Pixel);
            }
            else
            {
                graphics.DrawImage(_image, new Point(0, 0));
            }
        }
    }
}
