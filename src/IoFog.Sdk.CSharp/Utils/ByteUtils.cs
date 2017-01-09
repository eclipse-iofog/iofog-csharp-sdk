using System;
using System.IO;
using System.Text;

using IoFog.Sdk.CSharp.Extensions;

namespace IoFog.Sdk.CSharp.Utils
{
    /// <summary>
    /// Utils class for convenient byte transformations.
    /// </summary>
    internal static class ByteUtils
    {
        public static byte[] CopyOfRange(byte[] src, int from, int to)
        {
            byte[] result = new byte[to - from];
            Array.Copy(src, from, result, 0, to - from);

            return result;
        }

        public static byte[] LongToBytes(long x)
        {
            byte[] b = new byte[8];
            for (int i = 0; i < 8; ++i)
            {
                b[i] = (byte)(x >> (8 - i - 1 << 3));
            }
            return b;
        }

        public static long BytesToLong(byte[] bytes)
        {
            long result = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                result = (result << 8) + (bytes[i] & 0xff);
            }
            return result;
        }

        public static byte[] IntegerToBytes(int x)
        {
            byte[] b = new byte[4];
            for (int i = 0; i < 4; ++i)
            {
                b[i] = (byte)(x >> (4 - i - 1 << 3));
            }

            return b;
        }

        public static int BytesToInteger(byte[] bytes)
        {
            int result = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                result = (result << 8) + (bytes[i] & 0xff);
            }
            return result;
        }

        public static byte[] ShortToBytes(short x)
        {
            byte[] b = new byte[2];
            for (int i = 0; i < 2; ++i)
            {
                b[i] = (byte)(x >> (2 - i - 1 << 3));
            }
            return b;
        }

        public static short BytesToShort(byte[] bytes)
        {
            short result = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                result = (short)((result << 8) + (bytes[i] & 0xff));
            }
            return result;
        }

        public static byte[] DecimalToBytes(double x)
        {
            return BitConverter.GetBytes(x);
        }
    }
}
