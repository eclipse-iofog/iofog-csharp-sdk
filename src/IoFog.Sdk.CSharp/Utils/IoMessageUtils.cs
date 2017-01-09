﻿using System;
using System.Text;

namespace IoFog.Sdk.CSharp.Utils
{
    /// <summary>
    /// Utils class for convenient encoding and decoding for byte arrays
    /// </summary>
    public class IoMessageUtils
    {
        /// <summary>
        /// Method to encode byte array to base64 format.
        /// </summary>
        /// <param name="data">Array of bytes to be encoded.</param>
        /// <returns>Byte array.</returns>
        public static byte[] EncodeBase64(byte[] data)
        {
            try
            {
                var dataString64 = Convert.ToBase64String(data);
                return Encoding.UTF8.GetBytes(dataString64);
            }
            catch (Exception)
            {
                // TODO: Log WARNING "Error encoding bytes to base64 format."
                return null;
            }
        }

        /// <summary>
        /// Method to decode byte array from base64 format.
        /// </summary>
        /// <param name="data">Array of bytes to be decoded.</param>
        /// <returns>Byte array.</returns>
        public static byte[] DecodeBase64(byte[] data)
        {
            try
            {
                var dataString64 = Convert.ToBase64String(data);
                return Convert.FromBase64String(dataString64);
            }
            catch (Exception)
            {
                // TODO: Log WARNING "Error decoding bytes from base64 format."
                return null;
            }
        }
    }
}
