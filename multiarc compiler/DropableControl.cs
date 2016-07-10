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
        public virtual bool Selected { get; set; }

        public Pen DefaultPen { get; protected set; }

        private Panel _parentPanel;

        private Panel ParentPanel { 
            get
            {
                if (_parentPanel == null)
                {
                    var parent = Parent;
                    while (!(parent is Panel))
                    {
                        parent = parent.Parent;
                    }

                    _parentPanel = (Panel)parent;
                }

                return _parentPanel;
            }
        }

        protected DropableControl()
        {
            Selected = false;
            DefaultPen = Pens.Black;
            PassSelectedToControlsWithImage();
            MouseDown += OnMouseDown;
        }

        public virtual void OnMouseDown(object sender, MouseEventArgs e)
        {
            ClickedX = e.X;
            ClickedY = e.Y;

            SelectControl();
            if (!ModifierKeys.HasFlag(Keys.Control))
            {
                DeselectOthers();
            }

            MouseDownAction(e);
        }

        public void SelectControl()
        {
            Selected = true;
            DefaultPen = Pens.LightGreen;
            PassSelectedToControlsWithImage();
            Refresh();
        }

        public void DeselectControl()
        {
            Selected = false;
            DefaultPen = Pens.Black;
            PassSelectedToControlsWithImage();
            Refresh();
        }

        public virtual void DeselectOthers()
        {
            foreach (var c in ParentPanel.Controls)
            {
                if (c != this)
                {
                    var dropableControl = c as DropableControl;

                    if (dropableControl != null)
                    {
                        dropableControl.DeselectControl();
                    }
                }
            }
        }

        private void PassSelectedToControlsWithImage()
        {
            foreach (var control in Controls)
            {
                var image = control as ControlWithImage;
                if (image != null)
                {
                    image.Selected = Selected;
                    image.DefaultPen = DefaultPen;
                }
            }
        }

        public abstract void MouseDownAction(MouseEventArgs e);

        public int ClickedX { get; set; }

        public int ClickedY { get; set; }
    }
}
