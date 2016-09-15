using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using MoreLinq;
using System.Collections.Generic;
using System.Windows.Media.Animation;

namespace MultiArc_Compiler
{
    public class DragAndDropPanel : Panel
    {
        private bool _selecting;
        private bool _justStartedSelecting;
        private Point _startingPoint;
        private Rectangle _selectionRectangle = new Rectangle(0, 0, 0, 0);
        private bool _selectingFromLeftToRight;
        private int _clickedX;
        private int _clickedY;

        private ContextMenuStrip _menu;

        private List<NonPinDropableControl> _copiedControls = new List<NonPinDropableControl>();

        private Point _copiedControlsClickLocation;

        private List<Connector> Connectors
        {
            get
            {
                var connectors = new List<Connector>();

                foreach (var c in Controls)
                {
                    var line = c as Line;
                    if (line != null && line.ContainedByConnector != null && !connectors.Contains(line.ContainedByConnector))
                    {
                        connectors.Add(line.ContainedByConnector);
                    }
                }
                
                return connectors;
            }
        }

        public DragAndDropPanel()
        {
            DragDrop += DragAndDrop;
            DragEnter += OnDragEnter;
            MouseDown += OnMouseDown;
            MouseMove += OnMouseMove;
            MouseUp += OnMouseUp;
            MouseClick += OnMouseClick;
            KeyDown += OnKeyDown;

            _menu = new ContextMenuStrip();
            _menu.Items.Add("Paste      Ctrl + V");
            _menu.ItemClicked += MenuItemClicked;
        }

        public void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyValue == 'C' && e.Control)
            {
                DoTheCopy();
            }
            else if (e.KeyValue == 'V' && e.Control)
            {
                DoThePaste(0, 0);
            }
            else if (e.KeyValue == 'A' && e.Control)
            {
                SelectAllControls();   
            }
            else if (e.KeyCode == Keys.Delete)
            {
                DoTheRemove();
            }
        }

        private void SelectAllControls()
        {
            foreach (var c in Controls)
            {
                var dropableControl = c as NonPinDropableControl;

                if (dropableControl != null)
                {
                    dropableControl.SelectControl();
                    dropableControl.Refresh();
                }
            }
            
            foreach (var c in Connectors)
            {
                c.SelectControl();
            }
        }

        public void DoTheRemove()
        {
            var selectedControls = GetSelectedControls();

            foreach (var c in selectedControls)
            {
                Controls.Remove(c);
            }

            var clipboard = Parent as Clipboard;

            if (clipboard != null)
            {
                clipboard.RemoveControls(selectedControls);

                var selectedConnectors = GetSelectedConnectors();
                clipboard.RemoveConnectors(selectedConnectors);
                
                foreach (var c in selectedConnectors)
                {
                    foreach (var l in c.Lines)
                    {
                        Controls.Remove(l);
                    }
                }

                return;
            }

            var designer = Parent as Designer;
            
            if (designer != null)
            {
                designer.RemoveControls(selectedControls);
            }
        }

        private List<Connector> GetSelectedConnectors()
        {
            var selectedConnectors = new List<Connector>();

            foreach (var c in Controls)
            {
                var line = c as Line;
                if (line != null && line.ContainedByConnector != null && line.ContainedByConnector.Selected && !selectedConnectors.Contains(line.ContainedByConnector))
                {
                    selectedConnectors.Add(line.ContainedByConnector);
                }
            }

            return selectedConnectors;
        }

        private void MenuItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            DoThePaste(_clickedX, _clickedY);
        }

        public void DoTheCopy(NonPinDropableControl control = null)
        {
            _copiedControls = GetSelectedControls();

            var topLeftX = Width;
            var topLeftY = Height;

            if (_copiedControls.Count > 0)
            {
                foreach (var c in _copiedControls)
                {
                    if (c.Location.X < topLeftX)
                    {
                        topLeftX = c.Location.X;
                    }

                    if (c.Location.Y < topLeftY)
                    {
                        topLeftY = c.Location.Y;
                    }
                }

                _copiedControlsClickLocation = new Point(topLeftX + (control != null ? control.ClickedX : 0), topLeftY + (control != null ? control.ClickedY : 0));
            }
        }

        private List<NonPinDropableControl> GetSelectedControls()
        {
            var selectedControls = new List<NonPinDropableControl>();

            foreach (var c in Controls)
            {
                var dropableControl = c as NonPinDropableControl;
                if (dropableControl != null && dropableControl.Selected)
                {
                    selectedControls.Add(dropableControl);
                }
            }

            return selectedControls;
        }

        protected void DoThePaste(int x, int y)
        {
            var controlsToAdd = new List<NonPinDropableControl>();

            Cursor = Cursors.WaitCursor;
            foreach (var c in _copiedControls)
            {
                var newControl = (NonPinDropableControl)c.Clone();
                Controls.Add(newControl);
                var newX = c.Location.X + x - _copiedControlsClickLocation.X;
                if (newX < 0)
                {
                    newX = 0;
                }

                if (newX + newControl.Width >= Width)
                {
                    newX = Width - newControl.Width - 1;
                }

                var newY = c.Location.Y + y - _copiedControlsClickLocation.Y;
                if (newY < 0)
                {
                    newY = 0;
                }

                if (newY + newControl.Height >= Height)
                {
                    newY = Height - newControl.Height - 1;
                }

                newControl.Location = new Point(newX, newY);
                controlsToAdd.Add(newControl);
            }

            var clipboard = Parent as Clipboard;

            if (clipboard != null)
            {
                clipboard.DoThePaste(controlsToAdd);
            }

            Cursor = Cursors.Default;
        }

        private void DragAndDrop(object sender, DragEventArgs e)
        {
            string[] formats = e.Data.GetFormats();
            var item = (DropableControl)e.Data.GetData(formats[0]);
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

            foreach (Control c in Controls)
            {
                c.Refresh();
            }

            _selecting = true;
            _justStartedSelecting = true;
            _startingPoint = new Point(e.X, e.Y);
        }

        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (_selecting)
            {
                var g = CreateGraphics(); 
                if (!_justStartedSelecting)
                {
                    g.Clear(BackColor);
                }

                _justStartedSelecting = false;

                Point endPoint = new Point(e.X, e.Y);

                int width = endPoint.X - _startingPoint.X;
                int height = endPoint.Y - _startingPoint.Y;

                _selectingFromLeftToRight = width >= 0;

                _selectionRectangle = new Rectangle(width >= 0 ? _startingPoint.X : endPoint.X, height >= 0 ? _startingPoint.Y : endPoint.Y, Math.Abs(width), Math.Abs(height));
                
                g.DrawRectangle(Pens.Black, _selectionRectangle);
                g.FillRectangle(Brushes.Aqua, new Rectangle(_selectionRectangle.X + 1, _selectionRectangle.Y + 1, _selectionRectangle.Width - 1, _selectionRectangle.Height - 1));
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
                    CreateGraphics().Clear(BackColor);
                    SelectAllCoveredControlsAndDeselectAllNonCoveredControls();

                    RefreshAllControls();
                }
            }
        }

        private void RefreshAllControls()
        {
            foreach (Control c in Controls)
            {
                c.Refresh();
                //c.Refresh();
                //c.Refresh();
            }
        }

        private void SelectAllCoveredControlsAndDeselectAllNonCoveredControls()
        {
            foreach (var c in Controls)
            {
                var dropableControl = c as DropableControl;

                if (dropableControl != null)
                {
                    if (_selectingFromLeftToRight)
                    {
                        if (!dropableControl.IsPartialySelected(_selectionRectangle) && !dropableControl.IsCompletelySelected(_selectionRectangle))
                        {
                            dropableControl.DeselectControl();
                            continue;
                        }
                    }
                    else
                    {
                        if (!dropableControl.IsCompletelySelected(_selectionRectangle))
                        {
                            dropableControl.DeselectControl();
                            continue;
                        }
                    }

                    dropableControl.SelectControl();
                }
            }

            foreach (var c in Connectors)
            {
                if (_selectingFromLeftToRight)
                {
                    if (!c.IsPartialySelected(_selectionRectangle) && !c.IsCompletelySelected(_selectionRectangle))
                    {
                        continue;
                    }
                }
                else
                {
                    if (!c.IsCompletelySelected(_selectionRectangle))
                    {
                        continue;
                    }
                }

                c.SelectControl();
            }
        }

        public void OnMouseClick(object sender, MouseEventArgs e)
        {
            var clipboard = Parent as Clipboard;

            if (clipboard != null && clipboard.DrawingConnector)
            {
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                DeselectAllControls();
            }
            else
            {
                _clickedX = e.X;
                _clickedY = e.Y;
                _menu.Show(PointToScreen(new Point(e.X, e.Y)));
            }
        }

        public void DeselectAllControls()
        {
            foreach (var c in Controls)
            {
                var dropableControl = c as DropableControl;

                if (dropableControl != null)
                {
                    dropableControl.DeselectControl();
                }
            }


            foreach (var c in Connectors)
            {
                c.DeselectControl();
            }
        }

        public void DeselectAllControlsExcept(ISelectableControl control)
        {
            foreach (var c in Controls)
            {
                if (c != control)
                {
                    var dropableControl = c as DropableControl;

                    if (dropableControl != null)
                    {
                        dropableControl.DeselectControl();
                    }
                }
            }

            foreach (var c in Connectors)
            {
                if (c != control)
                {
                    c.DeselectControl();
                }
            }
        }
    }
}
