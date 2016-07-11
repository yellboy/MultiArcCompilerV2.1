using System;
using System.Drawing;
using System.Windows.Forms;

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
                    var selectionStartPoint = PointToClient(new Point(_selectionRectangle.X, _selectionRectangle.Y));

                    // TODO This part needs to be reworked
                    if (_selectionRectangle.Width > 0)
                    {
                        if (dropableControl.Location.X >= selectionStartPoint.X && dropableControl.Location.X <= selectionStartPoint.X + _selectionRectangle.Width &&
                            dropableControl.Location.Y >= selectionStartPoint.Y && dropableControl.Location.Y <= selectionStartPoint.Y + _selectionRectangle.Height)
                        {
                            dropableControl.SelectControl();
                        }
                    }
                    else
                    {
                        if (dropableControl.Location.X >= selectionStartPoint.X + _selectionRectangle.Width && dropableControl.Location.X <= selectionStartPoint.X &&
                            dropableControl.Location.Y >= selectionStartPoint.Y + _selectionRectangle.Height && dropableControl.Location.Y <= selectionStartPoint.Y)
                        {
                            dropableControl.SelectControl();
                        }
                    }

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
