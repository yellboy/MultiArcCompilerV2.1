/*
 * File: Pin.cs
 * Author: Bojan Jelaca
 * Date: November 2014
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MultiArc_Compiler
{

    /// <summary>
    /// Possible values of the pin.
    /// </summary>
    public enum PinValue { TRUE, FALSE, HIGHZ };

    /// <summary>
    /// Class representing pin of the port.
    /// </summary>
    public class Pin : Control
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
                    //lock (this)
                    //{
                        signal.Val = val;
                        if (!parentPort.Initializing)
                        { 
                            signal.InformOtherPins(this); 
                        }
                    //}
                }
            }
        }

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

        /// <summary>
        /// Creates one object of Pin class.
        /// </summary>
        /// <param name="parentPort">
        /// Parent port containing pin.
        /// </param>
        public Pin(Port parentPort, int index)
        {
            this.parentPort = parentPort;
            this.Name = parentPort.Name + index;
            if (parentPort.PortPosition == Position.DOWN || parentPort.PortPosition == Position.UP)
            {
                this.Height = 15;
                this.Width = 1;
            }
            else
            {
                this.Height = 1;
                this.Width = 15;
            }
            base.MouseClick += this.mouseClick;
            base.Paint += this.redraw;
            base.MouseEnter += this.mouseEnter;
        }

        public void InformThatSignalChanged(PinValue signalValue)
        {
            lock (this.parentPort)
            {
                OldVal = val;
                val = signalValue;
                Monitor.PulseAll(Form1.LockObject);
            }
        }

        protected void mouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.UpArrow;
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

        protected void mouseClick(object sender, MouseEventArgs e)
        {
            ((Clipboard)((Pin)sender).Parent.Parent.Parent).PinClicked(this);
        }

    }
}
