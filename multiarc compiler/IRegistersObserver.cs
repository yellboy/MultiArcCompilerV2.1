using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiArc_Compiler
{
    public interface IRegistersObserver
    {
        /// <summary>
        /// Notifies that some register was changed.
        /// </summary>
        /// <param name="name">
        /// Name of the changed register.
        /// </param>
        /// <param name="newValue">
        /// New value of the changed register.
        /// </param>
        void RegisterChanged(string name, int newValue);
    }
}