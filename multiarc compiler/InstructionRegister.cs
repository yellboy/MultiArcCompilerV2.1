/*
 * File: InstructionRegister.cs
 * Author: Bojan Jelaca
 * Date: July 2014
 */ 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiArc_Compiler
{
    /// <summary>
    /// Class representing binary code of the instruction.
    /// </summary>
    public class InstructionRegister
    {
        private byte[] binary;

        /// <summary>
        /// Binary code of the instruction.
        /// </summary>
        public byte[] Binary
        {
            get
            {
                return binary;
            }
            set
            {
                binary = value;
            }
        }

        /// <summary>
        /// Creates new instruction register.
        /// </summary>
        /// <param name="binary">
        /// Binary code for concrete instruction.
        /// </param>
        public InstructionRegister(byte[] binary)
        {
            this.binary = binary;
        }

        /// <summary>
        /// Gets specific bits of the instruction code.
        /// </summary>
        /// <param name="start">
        /// Start bit.
        /// </param>
        /// <param name="end">
        /// End bit.
        /// </param>
        /// <returns>
        /// Wanted bits as int.
        /// </returns>
        public int GetBits(int start, int end)
        {
            int result = 0;
            int size = 1;
            for (int k = start; k >= end; k--)
            {
                if (k % 8 == 0 && k != end)
                {
                    size++;
                }
            }
            int count = start - end;
            int byteCount = size - 1;
            for (int k = start; k >= end; k--)
            {
                int semiValue = (binary[binary.Length - 1 - end / 8 - byteCount] & (1 << ((count + end) % 8)));
                result |= semiValue << 8 * byteCount;
                //codeValue |= (byte)((semiValue & (1 << (codeEnds % 8 + codeCount))) >> byteCount * 8); // This might be a problem.
                if ((end + count) % 8 == 0)
                    byteCount--;
                count--;
            }
            result >>= end % 8;
            if ((result & (1 << start - end)) != 0)
            {
                result -= (1 << start - end + 1);
            }
            return result;
        }
    }
}
