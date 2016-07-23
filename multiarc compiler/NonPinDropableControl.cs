using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiArc_Compiler
{
    public abstract class NonPinDropableControl : DropableControl
    {
        public abstract object Clone();
    }
}
