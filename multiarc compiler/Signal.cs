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
    public class Signal : Connector
    {
        private static int nextId = 0;

        private int id = nextId++;

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

        public Bus Bus { get; set; }

        public LinkedList<Signal> ConnectedSignals { get; private set; } 

        /// <summary>
        /// Creates one object of 
        /// </summary>
        public Signal(UserSystem system) : base(system)
        {
            ConnectedSignals = new LinkedList<Signal>();
        }

        public void Remove()
        {
            foreach (Pin p in pins)
            {
                p.Signal = null;
                System.Signals.Remove(this);
            }
            foreach (Line l in Lines)
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

        public override void AddName(string name)
        {
            Names.AddLast(name);

            var signalsToMerge = new LinkedList<Signal>();
            foreach (var s in System.Signals)
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
            var system = signal.System;
            
            var mergedSignal = new Signal(signal.System);

            foreach (var s in signalsToMerge)
            {
                foreach (var name in s.Names)
                {
                    if (!mergedSignal.Names.Contains(name))
                    {
                        mergedSignal.Names.AddLast(name);
                    }
                }

                foreach (var line in s.Lines)
                {
                    if (!mergedSignal.Lines.Contains(line))
                    {
                        mergedSignal.Lines.AddLast(line);
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

                if (s.Bus != null)
                {
                    var node = s.Bus.Signals.Find(s);
                    s.Bus.Signals.AddBefore(node, mergedSignal);
                    s.Bus.Signals.Remove(s);
                }

                system.Signals.Remove(s);
            }

            system.Signals.AddLast(mergedSignal);
        }
    }
}
