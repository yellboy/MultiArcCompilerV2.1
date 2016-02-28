/*
 * File: Symbol.cs
 * Author: Bojan Jelaca
 * Date: September 2013
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiArc_Compiler
{
    /// <summary>
    /// One symbol in symbol table.
    /// </summary>
    class Symbol
    {

        /// <summary>
        /// Label attached to symbol.
        /// </summary>
        private String label;
        
        public string Label
        {
            get
            {
                return label;
            }
            set
            {
                label = value;
            }
        }


        /// <summary>
        /// Section of symbol.
        /// </summary>
        private int section;

        public int Section
        {
            get
            {
                return section;
            }
            set
            {
                section = value;
            }
        }

        /// <summary>
        /// Offset of symbol in its section.
        /// </summary>
        private int offset;

        public int Offset
        {
            get
            {
                return offset;
            }
            set
            {
                offset = value;
            }
        }

        /// <summary>
        /// Is section local?
        /// </summary>
        private bool local;

        public bool Local
        {
            get
            {
                return local;
            }
            set
            {
                local = value;
            }
        }

        /// <summary>
        /// Public constructor with all parameters.
        /// </summary>
        /// <param name="label">
        /// Label attached to symbol.
        /// </param>
        /// <param name="section">
        /// Section of symbol.
        /// </param>
        /// <param name="offset">
        /// Offset of symbol in its section.
        /// </param>
        /// <param name="local">
        /// Is symbol local?
        /// </param>
        public Symbol(String label, int section, int offset, bool local)
        {
            this.label = label;
            this.section = section;
            this.offset = offset;
            this.local = local;
        }
    }
}
