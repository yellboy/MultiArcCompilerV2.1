/*
 * File: Port.cs
 * Author: Bojan Jelaca
 * Date: October 2014
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MultiArc_Compiler
{

    /// <summary>
    /// Possible positions of the port.
    /// </summary>
    public enum Position { LEFT, RIGHT, UP, DOWN };

    /// <summary>
    /// Possible types of the port.
    /// </summary>
    public enum Kind { IN, OUT, INOUT };

    /// <summary>
    /// Represents one port of the component.
    /// </summary>
    public class Port : ICloneable
    {

        private Pin[] pins;

        private Position portPosition;

        /// <summary>
        /// Gets or sets positon of the port.
        /// </summary>
        public Position PortPosition
        {
            get
            {
                return portPosition;
            }
            set
            {
                portPosition = value;
            }
        }

        private Kind portType;

        /// <summary>
        /// Gets or sets type of the port.
        /// </summary>
        public Kind PortType
        {
            get
            {
                return portType;
            }
            set
            {
                portType = value;
            }
        }

        private int size;

        /// <summary>
        /// Gets or sets size of the port in bits/pins.
        /// </summary>
        public int Size
        {
            get
            {
                return size;
            }
            set
            {
                pins = new Pin[value];
                size = value;
            }
        }

        private string name;

        /// <summary>
        /// Gets or sets name of the port.
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


        /// <summary>
        /// Gets or sets value of the port.
        /// </summary>
        public int Val
        {
            get
            {
                int ret = 0;
                for (int i = 0; i < size; i++)
                {
                    if (pins[i].Val == PinValue.TRUE)
                    {
                        ret |= 1 << i;
                    }
                }
                Console.WriteLine("Port {0} returning value {1}.", name, ret);
                return ret;
            }
            set
            {
                for (int i = 0; i < size; i++)
                {
                    if ((value & (1 << i)) != 0)
                    {
                        pins[i].Val = PinValue.TRUE;
                    }
                    else
                    {
                        pins[i].Val = PinValue.FALSE;
                    }
                }
                Console.WriteLine("Port {0} setting value {1}.", name, value);
            }
        }

        /// <summary>
        /// Gets or set specific pin of the port.
        /// </summary>
        /// <param name="index">
        /// Index of the pin.
        /// </param>
        /// <returns>
        /// True if wanted pin is 1 or false if it is 0.
        /// </returns>
        public Pin this [int index] 
        {
            get
            {
                if (index >= 0 && index < size)
                {
                    return pins[index];
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
            set
            {
                if (index >= 0 && index < size)
                {
                    pins[index] = value;
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        protected SystemComponent component;

        /// <summary>
        /// Gets or sets component that contains the port.
        /// </summary>
        public SystemComponent Component
        {
            get
            {
                return component;
            }
            set
            {
                component = value;
            }
        }

        public bool Initializing { get; private set; }

        /// <summary>
        /// Creates one object of Port class.
        /// </summary>
        /// <param name="name">
        /// Name of the port.
        /// </param>
        /// <param name="size">
        /// Size of the port in bits.
        /// </param>
        /// <param name="val">
        /// Initial value of the port.
        /// </param>
        public Port(string name, SystemComponent component, int size = 0, int val = 0)
        {
            this.name = name;
            this.size = size;
            this.Val = val;
            this.component = component;
            Initializing = false;
        }

        /// <summary>
        /// Gets value of specific group of pins of this port.
        /// </summary>
        /// <param name="start">
        /// Start pin of the group.
        /// </param>
        /// <param name="end">
        /// End pin of the group.
        /// </param>
        /// <returns>
        /// Value of the wanted pins.
        /// </returns>
        public int GetPins(int start, int end)
        {
            if (start < 0 || start >= size || end < 0 || end  >= size || end < start)
            {
                throw new IndexOutOfRangeException();
            }
            int ret = 0;
            for (int i = start; i < end; i++)
            {
                if (pins[i].Val == PinValue.TRUE)
                {
                    ret |= 1 << i;
                }
            }
            return ret;
        }


        public object Clone()
        {
            Port ret = (Port)this.MemberwiseClone();
            ret.Size = size;
            ret.Val = Val;
            for (int i = 0; i < size; i++)
            {
                ret[i] = new Pin(ret, i);
            }
            return ret;
        }

        /// <summary>
        /// Initializes all pins of the port.
        /// </summary>
        public void InitializePins()
        {
            pins = new Pin[size];
            for (int i = 0; i < size; i++)
            {
                pins[i] = new Pin(this, i);
            }
        }

        public void ResetToDefault()
        {
            Initializing = true;
            Val = 0;
            Initializing = false;
        }

        public ICollection<Pin> GetAllPins()
        {
            return pins;
        }
    }
}
