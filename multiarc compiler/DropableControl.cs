using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MultiArc_Compiler
{
    public abstract class DropableControl : UserControl
    {
        public bool Selected { get; set; }

        public Pen DefaultPen { get; protected set; }

        protected DropableControl()
        {
            Selected = false;
            DefaultPen = Pens.Black;
            PassDefaultColorToControlsWithImages();
            MouseDown += OnMouseDown;
        }

        public virtual void OnMouseDown(object sender, MouseEventArgs e)
        {
            ClickedX = e.X;
            ClickedY = e.Y;

            SelectControl();
            Refresh();

            MouseDownAction(e);
        }

        public void SelectControl()
        {
            Selected = true;
            DefaultPen = Pens.LightGreen;
            PassDefaultColorToControlsWithImages();
        }

        private void PassDefaultColorToControlsWithImages()
        {
            foreach (var control in Controls)
            {
                var image = control as ControlWithImage;
                if (image != null)
                {
                    image.DefaultPen = DefaultPen;
                }
            }
        }

        public abstract void MouseDownAction(MouseEventArgs e);

        public int ClickedX { get; set; }

        public int ClickedY { get; set; }
    }
}
