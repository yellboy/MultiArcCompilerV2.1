using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MultiArc_Compiler
{
    /// <summary>
    /// Class representing signal connecting pins.
    /// </summary>
    public class Signal
    {
        private static int nextId = 0;

        private int id = nextId++;

        private string name;

        private PinValue val = PinValue.HIGHZ;

        public PinValue Val
        {
            get
            {
                return val;
            }
            set
            {
                val = value;
                switch (val)
                {
                    case PinValue.FALSE:
                        this.SetColor(Color.Blue);
                        break;
                    case PinValue.TRUE:
                        this.SetColor(Color.Red);
                        break;
                    case PinValue.HIGHZ:
                        this.SetColor(Color.Yellow);
                        break;
                }
            }
        }

        /// <summary>
        /// Gets or sets name of the signal.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        private LinkedList<Line> lines = new LinkedList<Line>();

        /// <summary>
        /// Linked list containing lines representing signal.
        /// </summary>
        public LinkedList<Line> Lines
        {
            get
            {
                return lines;
            }
        }

        private LinkedList<Pin> pins = new LinkedList<Pin>();

        /// <summary>
        /// Linked list containing all pins connected with the signal.
        /// </summary>
        public LinkedList<Pin> Pins
        {
            get
            {
                return pins;
            }
        }

        /// <summary>
        /// Creates one object of 
        /// </summary>
        public Signal()
        {
            name = "Signal" + id;
        }

        /// <summary>
        /// Sets color of the drawn signal.
        /// </summary>
        /// <param name="color">
        /// Color to be set.
        /// </param>
        public void SetColor(Color color)
        {
            foreach (Line line in lines)
            {
                if (line.InvokeRequired == true)
                {
                    setColor d = new setColor(SetColor);
                    line.BeginInvoke(d, new Object[] { color });
                }
                else
                {
                    line.ForeColor = color;
                    line.Refresh();
                }
            }
        }

        private delegate void setColor(Color color);

        public void Remove()
        {
            foreach (Pin p in pins)
            {
                p.Signal = null;
            }
            foreach (Line l in lines)
            {
                l.Dispose();
            }
        }

        /// <summary>
        /// Informs pins that value of the signal has changed.
        /// </summary>
        /// <param name="pin">
        /// Pin that changed value of the signal.
        /// </param>
        public void InformOtherPins(Pin pin)
        {
            foreach (Pin p in pins)
            {
                if (p != pin)
                {
                    p.InformThatSignalChanged(val);
                }
            }
        }
    }
}
