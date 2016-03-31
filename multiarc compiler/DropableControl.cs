using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MultiArc_Compiler
{
    public abstract class DropableControl : Control
    {
        public DropableControl()
        {
            MouseDown += OnMouseDown;
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            ClickedX = e.X;
            ClickedY = e.Y;
            MouseDownAction(e);
        }

        protected abstract void MouseDownAction(MouseEventArgs e);

        public int ClickedX { get; protected set; }

        public int ClickedY { get; protected set; }
    }
}
