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
        private bool _selectingFromLeftToRight;
        private int _clickedX;
        private int _clickedY;

        private ContextMenuStrip _menu;

        public DragAndDropPanel()
        {
            DragDrop += DragAndDrop;
            DragEnter += OnDragEnter;
            MouseDown += OnMouseDown;
            MouseMove += OnMouseMove;
            MouseUp += OnMouseUp;
            MouseClick += OnMouseClick;

            _menu = new ContextMenuStrip();
            _menu.Items.Add("Paste      Ctrl + V");
            _menu.ItemClicked += MenuItemClicked;
        }

        private void MenuItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            DoThePaste(_clickedX, _clickedY);
        }

        protected void DoThePaste(int x, int y)
        {
            var clipboard = Parent as Clipboard;

            if (clipboard != null)
            {
                clipboard.DoThePaste(x, y);
                return;
            }

            var designer = Parent as Designer;

            if (designer != null)
            {
                designer.DoThePaste(x, y);
            }
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
                //c.Refresh();
                //c.Refresh();
            }
        }

        private void SelectAllCoveredControls()
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
            if (e.Button == MouseButtons.Left)
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
            else
            {
                _clickedX = e.X;
                _clickedY = e.Y;
                _menu.Show(PointToScreen(new Point(e.X, e.Y)));
            }
        }
    }
}
