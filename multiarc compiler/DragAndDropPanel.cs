using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using MoreLinq;
using System.Collections.Generic;

namespace MultiArc_Compiler
{
    public class DragAndDropPanel : Panel
    {

        private bool _selecting;
        private bool _justStartedSelecting;
        private Point _startingPoint;
        private Rectangle _selectionRectangle = new Rectangle(0, 0, 0, 0);

        public DragAndDropPanel()
        {
            DragDrop += DragAndDrop;
            DragEnter += OnDragEnter;
            MouseDown += OnMouseDown;
            MouseMove += OnMouseMove;
            MouseUp += OnMouseUp;
            MouseClick += OnMouseClick;
        }

        private void DragAndDrop(object sender, DragEventArgs e)
        {
            string[] formats = e.Data.GetFormats();
            var item = (DropableControl)e.Data.GetData(formats[0]);
            //item.Location = new Point(e.X, e.Y);
            //item.Location = new Point(e.X - this.Location.X - 8, e.Y);
            var point = PointToClient(new Point(e.X - item.ClickedX, e.Y - item.ClickedY));

            var dx = item.Location.X - point.X;
            var dy = item.Location.Y - point.Y;
            
            item.Location = point;
            item.Refresh();

            foreach (var c in Controls)
            {
                if (c != item)
                {
                    var dropableControl = c as DropableControl;

                    if (dropableControl != null && dropableControl.Selected)
                    {
                        var newLocation = new Point(dropableControl.Location.X - dx, dropableControl.Location.Y - dy);
                        dropableControl.Location = newLocation;
                        dropableControl.Refresh();
                    }
                }
            }
        }

        private void OnDragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            var g = CreateGraphics();
            g.Clear(this.BackColor);

            foreach (Control c in Controls)
            {
                c.Refresh();
            }

            _selecting = true;
            _justStartedSelecting = true;
            _startingPoint = PointToScreen(new Point(e.X, e.Y));
        }

        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (_selecting)
            {
                if (!_justStartedSelecting)
                {
                    ControlPaint.DrawReversibleFrame(_selectionRectangle, Color.Black, FrameStyle.Dashed);
                    ControlPaint.FillReversibleRectangle(_selectionRectangle, Color.Red);
                }

                _justStartedSelecting = false;

                Point endPoint = ((Control)sender).PointToScreen(new Point(e.X, e.Y));

                int width = endPoint.X - _startingPoint.X;
                int height = endPoint.Y - _startingPoint.Y;
                _selectionRectangle = new Rectangle(_startingPoint.X, _startingPoint.Y, width, height);

                ControlPaint.DrawReversibleFrame(_selectionRectangle, Color.Black, FrameStyle.Dashed);
                ControlPaint.FillReversibleRectangle(_selectionRectangle, Color.Red);
            }
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (_selecting)
            {
                _selecting = false;

                //var g = CreateGraphics();
                //g.Clear(this.BackColor);

                if (!_justStartedSelecting)
                {
                    ControlPaint.DrawReversibleFrame(_selectionRectangle, Color.Black, FrameStyle.Dashed);
                    ControlPaint.FillReversibleRectangle(_selectionRectangle, Color.Red);

                    SelectAllCoveredControls();

                    RefreshAllControls();
                }
            }
        }

        private void RefreshAllControls()
        {
            foreach (Control c in Controls)
            {
                c.Refresh();
                c.Refresh();
                c.Refresh();
            }
        }

        private void SelectAllCoveredControls()
        {
            foreach (var c in Controls)
            {
                var dropableControl = c as DropableControl;

                if (dropableControl != null)
                {
                    //var point1 = PointToClient(new Point(_selectionRectangle.X, _selectionRectangle.Y));
                    //var point2 = new Point(point1.X, point1.Y + _selectionRectangle.Height);
                    //var point3 = new Point(point1.X + _selectionRectangle.Width, point1.Y);
                    //var point4 = new Point(point1.X + _selectionRectangle.Width, point1.Y + _selectionRectangle.Height);

                    //var points = new[] { point1, point2, point3, point4 };
                    //points = points.OrderBy(p => p.X).ThenBy(p => p.Y).ToArray();

                    // TODO This part needs to be reworked
                    if (_selectionRectangle.Width > 0)
                    {
                        
                        if (!dropableControl.IsPartialySelected(_selectionRectangle) && !dropableControl.IsCompletelySelected(_selectionRectangle))
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if (!dropableControl.IsCompletelySelected(_selectionRectangle))
                        {
                            continue;
                        }
                    }

                    dropableControl.SelectControl();
                }
            }
        }

        public void OnMouseClick(object sender, MouseEventArgs e)
        {
            foreach (var c in Controls)
            {
                var dropableControl = c as DropableControl;
                if (dropableControl != null)
                {
                    dropableControl.DeselectControl();
                }
            }
        }
    }
}
