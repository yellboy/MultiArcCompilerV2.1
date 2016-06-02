using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiArc_Compiler
{
    public class Bus : Connector
    {
        public LinkedList<Signal> Signals { get; private set; }

        public Bus(UserSystem system) : base(system)
        {
            Signals = new LinkedList<Signal>();
            Names = new LinkedList<string>();
        }

        public override void AddName(string name)
        {
        }
    }
}
