﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MultiArc_Compiler
{
    public abstract class DropableControl : UserControl, ISelectableControl
    {
        public virtual bool Selected { get; set; }

        public bool SelectingDisabled { get; set; }

        public Pen DefaultPen { get; protected set; }

        private DragAndDropPanel _parentPanel;

        protected DragAndDropPanel ParentPanel
        { 
            get
            {
                if (_parentPanel == null)
                {
                    var parent = Parent;
                    while (!(parent is DragAndDropPanel))
                    {
                        parent = parent.Parent;
                    }

                    _parentPanel = (DragAndDropPanel)parent;
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

        public abstract bool IsCompletelySelected(Rectangle rectangle);

        public abstract bool IsPartialySelected(Rectangle rectange);

        protected Point[] GetRectanglePoints(Rectangle rectangle)
        {
            var point1 = new Point(rectangle.X, rectangle.Y);
            var point2 = new Point(point1.X, point1.Y + rectangle.Height);
            var point3 = new Point(point1.X + rectangle.Width, point1.Y);
            var point4 = new Point(point1.X + rectangle.Width, point1.Y + rectangle.Height);

            var points = new[] { point1, point2, point3, point4 };
            return points.OrderBy(p => p.X).ThenBy(p => p.Y).ToArray();
        }

        public virtual void OnMouseDown(object sender, MouseEventArgs e)
        {
            ClickedX = e.X;
            ClickedY = e.Y;

            SelectControl();
            if (!ModifierKeys.HasFlag(Keys.Control) && e.Button != MouseButtons.Right)
            {
                DeselectOthers();
            }

            MouseDownAction(e);
        }

        public void SelectControl()
        {
            if (!SelectingDisabled && !Selected)
            {
                Selected = true;
                DefaultPen = Pens.LightGreen;
                PassSelectedToControlsWithImage();
                Refresh();
            }
        }

        public void DeselectControl()
        {
            if (Selected)
            {
                Selected = false;
                DefaultPen = Pens.Black;
                PassSelectedToControlsWithImage();
                Refresh();
            }
        }

        public void DeselectOthers()
        {
            ParentPanel.DeselectAllControlsExcept(this);
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
