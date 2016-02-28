using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiArc_Compiler
{
    public static class ConversionHelper
    {
        /// <summary>
        /// Converts value from byte array to int.
        /// </summary>
        /// <param name="binaryValue">
        /// Byte arrray value to convert.
        /// </param>
        /// <returns>
        /// Byte array converted to int.
        /// </returns>
        public static int ConvertFromByteArrayToInt(byte[] binaryValue)
        {
            int intValue = 0;
            for (int i = 0; i < binaryValue.Length; i++)
            {
                intValue |= binaryValue[i] << (8 * i);
            }
            return intValue;
        }

        /// <summary>
        /// Converts value from int to byte value
        /// </summary>
        /// <param name="intValue">
        /// Value to be converted.
        /// </param>
        /// <param name="size">
        /// Size of value in bits.
        /// </param>
        /// <returns>
        /// Int value converted to byte array.
        /// </returns>
        public static byte[] ConvertFromIntToByteArray(int intValue, int size)
        {
            byte[] binaryValue = new byte[(size - 1) / 8 + 1];
            for (int i = 0; i < binaryValue.Length; i++)
            {
                binaryValue[i] = (byte)(intValue & (0xFF << i * 8));
            }
            return binaryValue;
        }
    }
}
