/*
 * File: Data.cs
 * Author: Bojan Jelaca
 * Date: April 2014
 */


using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace MultiArc_Compiler
{
    /// <summary>
    /// Class whose instance represents one data type.
    /// </summary>
    public class Data
    {

        /// <summary>
        /// Data type size in bits.
        /// </summary>
        private int size;

        public int Size
        {
            get
            {
                return size;
            }
        }

        /// <summary>
        /// Name of data type.
        /// </summary>
        private String name;

        public String Name
        {
            get
            {
                return name;
            }
        }


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="size">
        /// Data size in bits. 
        /// </param>
        /// <param name="name">
        /// Data name.
        /// </param>
        public Data(int size, String name)
        {
            this.size = size;
            this.name = name;
        }

        /// Maybe it will be necessarry to write methods for aritmetical and logical operations.

    }
}
