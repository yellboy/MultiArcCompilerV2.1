using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiArc_Compiler
{
    public interface IMemoryObserver
    {
        /// <summary>
        /// Notifies that some location was changed.
        /// </summary>
        /// <param name="address">
        /// Address of the changed location.
        /// </param>
        /// <param name="newValue">
        /// New value of the changed location.
        /// </param>
        void LocationChanged(uint address, byte[] newValue);
    }
}
