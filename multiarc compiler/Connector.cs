using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MultiArc_Compiler
{
    public abstract class Connector
    {
        private static int _nextId = 0;

        protected int _id = ++_nextId;

        public virtual LinkedList<string> Names { get; protected set; }

        public virtual LinkedList<Line> Lines { get; protected set; }

        protected UserSystem System;

        public Connector(UserSystem system)
        {
            System = system;

            Lines = new LinkedList<Line>();
            Names = new LinkedList<string>();
        }

        public virtual void AddGenericName()
        {
            Names.AddLast(GetType().ToString().Split('.').Last() + _id);
        }
        /// <summary>
        /// Sets color of the drawn signal.
        /// </summary>
        /// <param name="color">
        /// Color to be set.
        /// </param>
        public void SetColor(Color color)
        {
            foreach (Line line in Lines)
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

        public abstract void AddName(string name);
    }
}
