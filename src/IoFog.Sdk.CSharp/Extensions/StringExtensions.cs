/*
 *******************************************************************************
 * Copyright (c) 2018 Edgeworx, Inc.
 *
 * This program and the accompanying materials are made available under the
 * terms of the Eclipse Public License v. 2.0 which is available at
 * http://www.eclipse.org/legal/epl-2.0
 *
 * SPDX-License-Identifier: EPL-2.0
 *******************************************************************************
*/

using System.Text;

namespace IoFog.Sdk.CSharp.Extensions
{
    internal static class StringExtensions
    {
        internal static byte[] GetBytes(this string value)
        {
            return GetBytesForEncoding(Encoding.UTF8, value);
        }

        internal static byte[] GetBytes(this string value, Encoding encoding)
        {
            return GetBytesForEncoding(encoding, value);
        }

        internal static byte[] GetBytes(this string value, string encoding)
        {
            return GetBytesForEncoding(Encoding.GetEncoding(encoding), value);
        }

        private static byte[] GetBytesForEncoding(Encoding encoding, string s)
        {
            return encoding.GetBytes(s);
        }

        public static int GetLengthOrZero(this string value)
        {
            return string.IsNullOrEmpty(value) ? 0 : value.Length;
        }

        public static byte[] GetBytesOrEmptyArray(this string value)
        {
            return value == null ? new byte[0] : value.GetBytes();
        }
    }
}