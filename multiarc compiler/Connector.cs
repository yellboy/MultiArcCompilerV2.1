using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using MoreLinq;

namespace MultiArc_Compiler
{
    public abstract class Connector : ISelectableControl
    {
        private static int _nextId = 0;

        protected int _id = ++_nextId;

        public bool Selected
        {
            get;
            set;
        }

        public bool SelectingDisabled
        {
            get;
            set;
        }

        public virtual LinkedList<string> Names { get; protected set; }

        public virtual LinkedList<Line> Lines { get; protected set; }

        protected UserSystem System;

        private DragAndDropPanel _parentPanel;

        protected DragAndDropPanel ParentPanel
        {
            get
            {
                if (_parentPanel == null)
                {
                    var parent = Lines.First().Parent;
                    while (!(parent is DragAndDropPanel))
                    {
                        parent = parent.Parent;
                    }

                    _parentPanel = (DragAndDropPanel)parent;
                }

                return _parentPanel;
            }
        }

        private Color _previousColor;

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
                    _previousColor = line.ForeColor;
                    line.ForeColor = color;
                    line.Refresh();
                }
            }
        }

        private delegate void setColor(Color color);

        public abstract void AddName(string name);

        public void SelectControl()
        {
            if (!SelectingDisabled && !Selected)
            {
                Selected = true;
                SetColor(Color.Lime);
                Refresh();
            }
        }

        public void DeselectControl()
        {
            if (Selected)
            {
                Selected = false;
                SetColor(_previousColor);
                Refresh();
            }
        }

        private void Refresh()
        {
            Lines.ForEach(l => l.Refresh());
        }

        public void DeselectOthers()
        {
            ParentPanel.DeselectAllControlsExcept(this);    
        }

        public bool IsCompletelySelected(Rectangle rectangle)
        {
            foreach (var l in Lines)
            {
                if (!l.IsCompletelySelected(rectangle))
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsPartialySelected(Rectangle rectangle)
        {
            foreach (var l in Lines)
            {
                if (l.IsPartialySelected(rectangle))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
