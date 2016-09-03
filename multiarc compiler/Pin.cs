/*
 * File: Pin.cs
 * Author: Bojan Jelaca
 * Date: November 2014
 */

using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace MultiArc_Compiler
{

    /// <summary>
    /// Possible values of the pin.
    /// </summary>
    public enum PinValue { TRUE, FALSE, HIGHZ, UNDEFINED };

    /// <summary>
    /// Class representing pin of the port.
    /// </summary>
    public class Pin : DropableControl, ICloneable
    {
        private Port parentPort;

        /// <summary>
        /// Parent port containing pin.
        /// </summary>
        public Port ParentPort
        {
            get
            {
                return parentPort;
            }
            set
            {
                parentPort = value;
            }
        }

        private Graphics parentGraphics;

        /// <summary>
        /// Graphics on which this pin should be drawn.
        /// </summary>
        public Graphics ParentGraphics
        {
            get
            {
                return parentGraphics;
            }
            set
            {
                parentGraphics = value;
            }
        }
        
        private PinValue val = PinValue.HIGHZ;

        /// <summary>
        /// Value of the pin.
        /// </summary>
        public PinValue Val
        {
            get
            {
                return val;
            }
            set
            {
                OldVal = val;
                val = value;
                if (signal != null)
                {
                    signal.SetValueFromPinIfNeeded(this);

                    if (!parentPort.Initializing)
                    {
                        signal.InformOtherPins(this);
                    }
                }

                ValueSetExternaly = false;
            }
        }

        public PinValue SetValue { get; private set; }

        public bool ValueSetExternaly { get; private set; }

        public PinValue OldVal { get; private set; }

        protected Signal signal = null;

        /// <summary>
        /// Gets or sets signal to which the pin is attached. 
        /// </summary>
        public Signal Signal
        {
            get
            {
                return signal;
            }
            set
            {
                signal = value;
            }
        }

        public new string Name { get; set; }

        public Clipboard Clipboard { get; set; }

        public Designer Designer { get; set; }

        private readonly ContextMenuStrip _menu = new ContextMenuStrip();

        /// <summary>
        /// Creates one object of Pin class.
        /// </summary>
        /// <param name="parentPort">
        /// Parent port containing pin.
        /// </param>
        public Pin(Port parentPort, int index) : this(parentPort)
        {
            this.Name = parentPort.Name + index;
            ValueSetExternaly = false;
        }

        public Pin(Port parentPort, string name) : this(parentPort)
        {
            this.Name = name;
        }

        private Pin(Port parentPort)
        {
            this.parentPort = parentPort;
            if (parentPort.PortPosition == Position.DOWN || parentPort.PortPosition == Position.UP)
            {
                this.Height = 5;
                this.Width = 1;
            }
            else
            {
                this.Height = 1;
                this.Width = 5;
            }
            base.Paint += this.redraw;
            base.MouseEnter += this.mouseEnter;
            _menu.Items.Add("Remove");
            _menu.ItemClicked += ItemClicked;
        }

        private void ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Designer.RemovePin(this);
        }

        public void InformThatSignalChanged(PinValue signalValue)
        {
            if (ValueSetExternaly || val == PinValue.UNDEFINED || (val == PinValue.HIGHZ && signalValue == SetValue))
            {
                lock (this.parentPort)
                {
                    if (val == PinValue.UNDEFINED)
                    {
                        ValueSetExternaly = true;
                    }

                    OldVal = val;
                    val = signalValue;
                    Monitor.PulseAll(Form1.LockObject);
                }
            }
        }

        public void SetHighZ()
        {
            if (!ValueSetExternaly)
            {
                SetValue = val;
            }

            val = PinValue.HIGHZ;
        }

        protected void mouseEnter(object sender, EventArgs e)
        {
            if (Clipboard != null)
            {
                this.Cursor = Cursors.UpArrow;
            }
        }

        protected void redraw(object sender, PaintEventArgs e)
        {
            Graphics graphics = this.CreateGraphics();
            switch (parentPort.PortPosition)
            {
                case Position.LEFT:
                    graphics.DrawLine(Pens.Black, 0, 0, 5, 0);
                    break;
                case Position.RIGHT:
                    graphics.DrawLine(Pens.Black, 0, 0, 5, 0);
                    break;
                case Position.UP:
                    graphics.DrawLine(Pens.Black, 0, 0, 0, 5);
                    break;
                case Position.DOWN:
                    graphics.DrawLine(Pens.Black, 0, 0, 0, 5);
                    break;
            }
        }

        public bool ContainsPoint(Point point)
        {
            if (Parent is DropableControl)
            {
                var x = Location.X + Parent.Location.X;
                var y = Location.X + Parent.Location.Y;

                if (point.X >= x && point.X <= x + Width && point.Y >= y && point.Y <= y + Height)
                {
                    return true;
                }
            }

            return false;
        }

        public override void MouseDownAction(MouseEventArgs e)
        {
            if (Clipboard != null)
            {
                if (e.Button == MouseButtons.Left)
                {
                    Clipboard.PinClicked(this);
                }
            }
            else if (Designer != null)
            {
                if (e.Button == MouseButtons.Left)
                {
                    DoDragDrop(this, DragDropEffects.Move);
                }
                else
                {
                    _menu.Show(new Point(e.X, e.Y));
                }
            }
        }

        public object Clone()
        {
            var newPin = new Pin(parentPort, Name);
            newPin.val = val;
            newPin.Location = Location;
            newPin.Clipboard = Clipboard;
            newPin.Designer = Designer;
            newPin.ClickedX = ClickedX;
            newPin.ClickedY = ClickedY;
            newPin.OldVal = OldVal;
            newPin.parentGraphics = parentGraphics;
            newPin.Size = Size;
            
            return newPin;
        }

        // TODO
        public override bool IsCompletelySelected(Rectangle rectangle)
        {
            return true;
        }

        // TODO
        public override bool IsPartialySelected(Rectangle rectange)
        {
            return true;
        }
    }
}
