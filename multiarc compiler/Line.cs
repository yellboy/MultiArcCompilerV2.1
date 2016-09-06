﻿/*
 * File: Line.cs
 * Author: Bojan Jelaca
 * Date: November 2014
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MultiArc_Compiler
{
    
    /// <summary>
    /// Class representing line being part of the signal.
    /// </summary>
    public class Line : Control
    {

        private Signal containedBySignal;

        /// <summary>
        /// Signal containing line.
        /// </summary>
        public Signal ContainedBySignal
        {
            get
            {
                return containedBySignal;
            }
            set
            {
                containedBySignal = value;
            }
        }

        public Bus ContainedByBus
        {
            get;
            set;
        }

        public Connector ContainedByConnector
        {
            get
            {
                return ContainedByBus as Connector ?? ContainedBySignal as Connector;
            }
        }

        private int x1, x2, y1, y2;

        /// <summary>
        /// X coordinate of the first point of the line.
        /// </summary>
        public int X1
        {
            get
            {
                return x1;
            }
            set
            {
                x1 = value;
            }
        }

        /// <summary>
        /// Y coordinate of the second point of the line.
        /// </summary>
        public int Y1
        {
            get
            {
                return y1;
            }
            set
            {
                y1 = value;
            }
        }

        /// <summary>
        /// X coordinate of the second point of the line.
        /// </summary>
        public int X2
        {
            get
            {
                return x2;
            }
            set
            {
                x2 = value;
            }
        }

        /// <summary>
        /// Y coordinate of the second point.
        /// </summary>
        public int Y2
        {
            get
            {
                return y2;
            }
            set
            {
                y2 = value;
            }
        }

        private int _thicknes;

        protected ContextMenuStrip menu = new ContextMenuStrip();

        /// <summary>
        /// Creates one object of Line class.
        /// </summary>
        /// <param name="x1">
        /// X coordinate of the first point of the line.
        /// </param>
        /// <param name="y1">
        /// Y coordinate of the first point of the line.
        /// </param>
        /// <param name="x2">
        /// X coordinate of the second point of the line.
        /// </param>
        /// <param name="y2">
        /// Y coordinate of the second point of the line.
        /// </param>
        /// <param name="signal">
        /// Signal containing line.
        /// </param>
        public Line(int thicknes, int x1 = 0, int y1 = 0, int x2 = 0, int y2 = 0, Signal signal = null)
        {
            _thicknes = thicknes;
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
            this.containedBySignal = signal;
            Width = Math.Abs(x2 - x1);
            Height = Math.Abs(y2 - y1);
            if (Width == 0)
            {
                Width = _thicknes;
                Location = new Point(x1, y2 > y1 ? y1 : y2);
            }
            if (Height == 0)
            {
                Height = _thicknes;
                Location = new Point(x2 > x1 ? x1 : x2, y1);
            }
            menu.ItemClicked += menuClick;
            base.Paint += this.draw;
            base.MouseClick += this.mouseClick;
            base.MouseEnter += this.mouseEnter;
            base.MouseLeave += this.mouseLeave;
            menu.Items.Add("See pin states");
            menu.Items.Add("Manage names");
            menu.Items.Add("Remove");
        }

        private void menuClick(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "See pin states":
                    if (containedBySignal.Bus != null) 
                    { 
                        new BusPinStatesForm(containedBySignal.Bus); 
                    } 
                    else 
                    { 
                        new SignalPinStatesForm(containedBySignal); 
                    }

                    break;
                case "Manage names":
                    new ManageSignalNamesDialog(ContainedByConnector);
                    break;
                case "Remove":
                    containedBySignal.Remove();
                    break;
            }
        }

        protected void draw(object sender, EventArgs e)
        {
            Graphics graphics = CreateGraphics();
            graphics.PageUnit = GraphicsUnit.Pixel;
            Pen pen = new Pen(ForeColor, _thicknes);
            if (Width == _thicknes)
            {
                graphics.DrawLine(pen, 0, 0, 0, Height);
            }
            if (Height == _thicknes)
            {
                graphics.DrawLine(pen, 0, 0, Width, 0);
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Line
            // 
            this.ResumeLayout(false);

        }

        private void mouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                menu.Show(this, new Point(e.X, e.Y));
            }
            else
            {
                ContainedByConnector.SelectControl();
                if (!ModifierKeys.HasFlag(Keys.Control) && e.Button != MouseButtons.Right)
                {
                    ContainedByConnector.DeselectOthers();
                }
            }
        }


        private void mouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.UpArrow;
        }

        private void mouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        public bool IsCompletelySelected(Rectangle rectangle)
        {
            var topLeft = new Point(rectangle.Location.X, rectangle.Location.Y);
            var bottomRight = new Point(rectangle.Location.X + rectangle.Width - 1, rectangle.Location.Y + rectangle.Height - 1);

            return topLeft.X <= X1 && bottomRight.X >= X2 && topLeft.Y <= Y1 && bottomRight.Y >= Y2;
        }

        public bool IsPartialySelected(Rectangle rectangle)
        {
            var topLeft = new Point(rectangle.Location.X, rectangle.Location.Y);
            var bottomRight = new Point(rectangle.Location.X + rectangle.Width - 1, rectangle.Location.Y + rectangle.Height - 1);

            if (_thicknes == Width)
            {
                return (topLeft.X <= X1 && bottomRight.X >= X1 &&
                            (topLeft.Y <= Y1 && bottomRight.Y >= Y1 || 
                             topLeft.Y <= Y2 && bottomRight.Y >= Y2 ||
                             topLeft.Y >= Y1 && bottomRight.Y <= Y2));
            }
            else
            {
                return (topLeft.Y <= Y1 && bottomRight.Y >= Y1 &&
                            (topLeft.X <= X1 && bottomRight.X >= X1 || 
                             topLeft.X <= X2 && bottomRight.X >= X2 ||
                             topLeft.X >= X1 && bottomRight.X <= X2));
            }
        }
    }
}
