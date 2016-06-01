using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MultiArc_Compiler
{
    /// <summary>
    /// Class representing signal connecting pins.
    /// </summary>
    public class Signal
    {
        private static int nextId = 0;

        private int id = nextId++;

        private LinkedList<string> names;

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
        public LinkedList<string> Names
        {
            get
            {
                return names;
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

        private readonly UserSystem _system;

        public LinkedList<Signal> ConnectedSignals { get; private set; } 

        /// <summary>
        /// Creates one object of 
        /// </summary>
        public Signal(UserSystem system)
        {
            _system = system;

            ConnectedSignals = new LinkedList<Signal>();
            names = new LinkedList<string>();
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
                _system.Signals.Remove(this);
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
        public void InformOtherPins(Pin pin = null)
        {
            foreach (Pin p in pins)
            {
                if (pin == null || p != pin)
                {
                    p.InformThatSignalChanged(val);
                }
            }

            foreach (var s in ConnectedSignals)
            {
                s.InformOtherPins();
            }
        }

        public void AddName(string name)
        {
            Names.AddLast(name);

            var signalsToMerge = new LinkedList<Signal>();
            foreach (var s in _system.Signals)
            {
                if (s != this && s.Names.Contains(name))
                {
                    signalsToMerge.AddLast(s);
                }
            }

            if (signalsToMerge.Count > 0)
            {
                signalsToMerge.AddLast(this);
                MergeSignals(signalsToMerge);
            }
        }

        private static void MergeSignals(LinkedList<Signal> signalsToMerge)
        {
            var signal = signalsToMerge.Last();
            var system = signal._system;
            
            var mergedSignal = new Signal(signal._system);

            foreach (var s in signalsToMerge)
            {
                foreach (var name in s.names)
                {
                    if (!mergedSignal.names.Contains(name))
                    {
                        mergedSignal.names.AddLast(name);
                    }
                }

                foreach (var line in s.lines)
                {
                    if (!mergedSignal.lines.Contains(line))
                    {
                        mergedSignal.lines.AddLast(line);
                    }
                }

                foreach (var pin in s.pins)
                {
                    if (!mergedSignal.pins.Contains(pin))
                    {
                        mergedSignal.pins.AddLast(pin);
                        pin.Signal = mergedSignal;
                    }
                }

                system.Signals.Remove(s);
            }

            system.Signals.AddLast(mergedSignal);
        }

        public void AddGenericName()
        {
            names.AddLast("Signal" + id);
        }
    }
}
