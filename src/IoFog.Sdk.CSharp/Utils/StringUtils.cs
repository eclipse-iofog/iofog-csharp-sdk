using System.Text;

namespace IoFog.Sdk.CSharp.Utils
{
    /// <summary>
    /// This class is provided methods to construct new strings.
    /// </summary>
    internal static class StringUtils
    {
        /// <summary>
        /// Creates a new string.
        /// </summary>
        /// <param name="bytes">The byte array containing the sequence of bytes to decode.</param>
        /// <returns>Decoded string.</returns>
        public static string NewString(byte[] bytes)
        {
            return NewString(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Creates a new string.
        /// </summary>
        /// <param name="bytes">The byte array containing the sequence of bytes to decode.</param>
        /// <param name="index">The index of the first byte to decode.</param>
        /// <param name="count">The number of bytes to decode.</param>
        /// <returns>Decoded string.</returns>
        public static string NewString(byte[] bytes, int index, int count)
        {
            return Encoding.UTF8.GetString(bytes, index, count);
        }

        /// <summary>
        /// Creates a new string.
        /// </summary>
        /// <param name="bytes">The byte array containing the sequence of bytes to decode.</param>
        /// <param name="encoding">The charactedr encoding that will be used to decode.</param>
        /// <returns>Decoded string.</returns>
        public static string NewString(byte[] bytes, Encoding encoding)
        {
            return NewString(bytes, 0, bytes.Length, encoding);
        }

        /// <summary>
        /// Creates a new string.
        /// </summary>
        /// <param name="bytes">The byte array containing the sequence of bytes to decode.</param>
        /// <param name="index">The index of the first byte to decode.</param>
        /// <param name="count">The number of bytes to decode.</param>
        /// <param name="encoding">The charactedr encoding that will be used to decode.</param>
        /// <returns>Decoded string.</returns>
        public static string NewString(byte[] bytes, int index, int count, Encoding encoding)
        {
            return encoding.GetString(bytes, index, count);
        }
    }
}
