﻿using System;
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

        private List<NonPinDropableControl> _copiedControls = new List<NonPinDropableControl>();

        private Point _copiedControlsClickLocation;

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
            else if (e.KeyCode == Keys.Delete)
            {
                DoTheRemove();
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
                return;
            }

            var designer = Parent as Designer;
            
            if (designer != null)
            {
                designer.RemoveControls(selectedControls);
            }
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

            foreach (var c in _copiedControls)
            {
                var newControl = (NonPinDropableControl)c.Clone();
                Controls.Add(newControl);
                newControl.Location = new Point(c.Location.X + x - _copiedControlsClickLocation.X, c.Location.Y + y - _copiedControlsClickLocation.Y);
                controlsToAdd.Add(newControl);
            }

            var clipboard = Parent as Clipboard;

            if (clipboard != null)
            {
                clipboard.DoThePaste(controlsToAdd);
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
