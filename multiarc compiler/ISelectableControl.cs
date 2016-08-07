using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MultiArc_Compiler
{
    public interface ISelectableControl
    {
        bool Selected { get; set; }

        bool SelectingDisabled { get; set; }

        void SelectControl();

        void DeselectControl();

        void DeselectOthers();

        bool IsCompletelySelected(Rectangle rectangle);

        bool IsPartialySelected(Rectangle rectangle);
    }
}
