﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace CodeGenerator.Lexer
{
    internal static class Hash
    {
        /// <summary>
        /// This is how VB Anonymous Types combine hash values for fields.
        /// </summary>
        /// <param name="newKey"></param>
        /// <param name="currentKey"></param>
        internal static int Combine(int newKey, int currentKey)
        {
            return unchecked(currentKey * (int)0xA5555529 + newKey);
        }

        internal static int Combine(bool newKeyPart, int currentKey)
        {
            return Combine(currentKey, newKeyPart ? 1 : 0);
        }

        /// <summary>
        /// This is how VB Anonymous Types combine hash values for fields.
        /// PERF: Do not use with enum types because that involves multiple
        /// unnecessary boxing operations.  Unfortunately, we can't constrain
        /// T to "non-enum", so we'll use a more restrictive constraint.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="newKeyPart"></param>
        /// <param name="currentKey"></param>
        internal static int Combine<T>(T newKeyPart, int currentKey) where T : class
        {
            int hash = unchecked(currentKey * (int)0xA5555529);
            if (newKeyPart != null)
            {
                return unchecked(hash + newKeyPart.GetHashCode());
            }

            return hash;
        }

        internal static int CombineValues<T>(IEnumerable<T> values, int maxItemsToHash = int.MaxValue)
        {
            if (values == null)
            {
                return 0;
            }

            var hashCode = 0;
            var count = 0;
            foreach (var value in values)
            {
                if (count++ >= maxItemsToHash)
                {
                    break;
                }

                // Should end up with a constrained virtual call to object.GetHashCode (i.e. avoid boxing where possible).
                if (value != null)
                {
                    hashCode = Combine(value.GetHashCode(), hashCode);
                }
            }

            return hashCode;
        }

        internal static int CombineValues<T>(T[] values, int maxItemsToHash = int.MaxValue)
        {
            if (values == null)
            {
                return 0;
            }

            var maxSize = Math.Min(maxItemsToHash, values.Length);
            var hashCode = 0;
            for (int i = 0; i < maxSize; i++)
            {
                T value = values[i];

                // Should end up with a constrained virtual call to object.GetHashCode (i.e. avoid boxing where possible).
                if (value != null)
                {
                    hashCode = Combine(value.GetHashCode(), hashCode);
                }
            }

            return hashCode;
        }

        internal static int CombineValues<T>(ImmutableArray<T> values, int maxItemsToHash = int.MaxValue)
        {
            if (values.IsDefaultOrEmpty)
            {
                return 0;
            }

            var hashCode = 0;
            var count = 0;
            foreach (var value in values)
            {
                if (count++ >= maxItemsToHash)
                {
                    break;
                }

                // Should end up with a constrained virtual call to object.GetHashCode (i.e. avoid boxing where possible).
                if (value != null)
                {
                    hashCode = Combine(value.GetHashCode(), hashCode);
                }
            }

            return hashCode;
        }

        internal static int CombineValues(IEnumerable<string> values, StringComparer stringComparer, int maxItemsToHash = int.MaxValue)
        {
            if (values == null)
            {
                return 0;
            }

            var hashCode = 0;
            var count = 0;
            foreach (var value in values)
            {
                if (count++ >= maxItemsToHash)
                {
                    break;
                }

                if (value != null)
                {
                    hashCode = Combine(stringComparer.GetHashCode(value), hashCode);
                }
            }

            return hashCode;
        }

        /// <summary>
        /// The offset bias value used in the FNV-1a algorithm
        /// See http://en.wikipedia.org/wiki/Fowler%E2%80%93Noll%E2%80%93Vo_hash_function
        /// </summary>
        internal const int FnvOffsetBias = unchecked((int)2166136261);
        /// <summary>
        /// The generative factor used in the FNV-1a algorithm
        /// See http://en.wikipedia.org/wiki/Fowler%E2%80%93Noll%E2%80%93Vo_hash_function
        /// </summary>
        internal const int FnvPrime = 16777619;

        /// <summary>
        /// Compute the FNV-1a hash of a sequence of bytes
        /// See http://en.wikipedia.org/wiki/Fowler%E2%80%93Noll%E2%80%93Vo_hash_function
        /// </summary>
        /// <param name="data">The sequence of bytes</param>
        /// <returns>The FNV-1a hash of <paramref name="data"/></returns>
        internal static int GetFNVHashCode(byte[] data)
        {
            int hashCode = FnvOffsetBias;
            for (int i = 0; i < data.Length; i++)
            {
                hashCode = unchecked((hashCode ^ data[i]) * FnvPrime);
            }

            return hashCode;
        }

        /// <summary>
        /// Compute the FNV-1a hash of a sequence of bytes and determines if the byte
        /// sequence is valid ASCII and hence the hash code matches a char sequence
        /// encoding the same text.
        /// See http://en.wikipedia.org/wiki/Fowler%E2%80%93Noll%E2%80%93Vo_hash_function
        /// </summary>
        /// <param name="data">The sequence of bytes that are likely to be ASCII text.</param>
        /// <param name="length">The length of the sequence.</param>
        /// <param name="isAscii">True if the sequence contains only characters in the ASCII range.</param>
        /// <returns>The FNV-1a hash of <paramref name="data"/></returns>
        internal static unsafe int GetFNVHashCode(byte* data, int length, out bool isAscii)
        {
            int hashCode = FnvOffsetBias;
            byte asciiMask = 0;
            for (int i = 0; i < length; i++)
            {
                byte b = data[i];
                asciiMask |= b;
                hashCode = unchecked((hashCode ^ b) * FnvPrime);
            }

            isAscii = (asciiMask & 0x80) == 0;
            return hashCode;
        }

        /// <summary>
        /// Compute the FNV-1a hash of a sequence of bytes
        /// See http://en.wikipedia.org/wiki/Fowler%E2%80%93Noll%E2%80%93Vo_hash_function
        /// </summary>
        /// <param name="data">The sequence of bytes</param>
        /// <returns>The FNV-1a hash of <paramref name="data"/></returns>
        internal static int GetFNVHashCode(ImmutableArray<byte> data)
        {
            int hashCode = FnvOffsetBias;
            for (int i = 0; i < data.Length; i++)
            {
                hashCode = unchecked((hashCode ^ data[i]) * FnvPrime);
            }

            return hashCode;
        }

        /// <summary>
        /// Compute the hashcode of a sub-string using FNV-1a
        /// See http://en.wikipedia.org/wiki/Fowler%E2%80%93Noll%E2%80%93Vo_hash_function
        /// Note: FNV-1a was developed and tuned for 8-bit sequences. We're using it here
        /// for 16-bit Unicode chars on the understanding that the majority of chars will
        /// fit into 8-bits and, therefore, the algorithm will retain its desirable traits
        /// for generating hash codes.
        /// </summary>
        /// <param name="text">The input string</param>
        /// <param name="start">The start index of the first character to hash</param>
        /// <param name="length">The number of characters, beginning with <paramref name="start"/> to hash</param>
        /// <returns>The FNV-1a hash code of the substring beginning at <paramref name="start"/> and ending after <paramref name="length"/> characters.</returns>
        internal static int GetFNVHashCode(string text, int start, int length)
        {
            int hashCode = FnvOffsetBias;
            int end = start + length;
            for (int i = start; i < end; i++)
            {
                hashCode = unchecked((hashCode ^ text[i]) * FnvPrime);
            }

            return hashCode;
        }


        /// <summary>
        /// Compute the hashcode of a sub-string using FNV-1a
        /// See http://en.wikipedia.org/wiki/Fowler%E2%80%93Noll%E2%80%93Vo_hash_function
        /// </summary>
        /// <param name="text">The input string</param>
        /// <param name="start">The start index of the first character to hash</param>
        /// <returns>The FNV-1a hash code of the substring beginning at <paramref name="start"/> and ending at the end of the string.</returns>
        internal static int GetFNVHashCode(string text, int start)
        {
            return GetFNVHashCode(text, start, text.Length - start);
        }

        /// <summary>
        /// Compute the hashcode of a string using FNV-1a
        /// See http://en.wikipedia.org/wiki/Fowler%E2%80%93Noll%E2%80%93Vo_hash_function
        /// </summary>
        /// <param name="text">The input string</param>
        /// <returns>The FNV-1a hash code of <paramref name="text"/></returns>
        internal static int GetFNVHashCode(string text)
        {
            return CombineFNVHash(FnvOffsetBias, text);
        }

        /// <summary>
        /// Compute the hashcode of a string using FNV-1a
        /// See http://en.wikipedia.org/wiki/Fowler%E2%80%93Noll%E2%80%93Vo_hash_function
        /// </summary>
        /// <param name="text">The input string</param>
        /// <returns>The FNV-1a hash code of <paramref name="text"/></returns>
        internal static int GetFNVHashCode(StringBuilder text)
        {
            int hashCode = FnvOffsetBias;
            int end = text.Length;
            for (int i = 0; i < end; i++)
            {
                hashCode = unchecked((hashCode ^ text[i]) * FnvPrime);
            }

            return hashCode;
        }

        /// <summary>
        /// Compute the hashcode of a sub string using FNV-1a
        /// See http://en.wikipedia.org/wiki/Fowler%E2%80%93Noll%E2%80%93Vo_hash_function
        /// </summary>
        /// <param name="text">The input string as a char array</param>
        /// <param name="start">The start index of the first character to hash</param>
        /// <param name="length">The number of characters, beginning with <paramref name="start"/> to hash</param>
        /// <returns>The FNV-1a hash code of the substring beginning at <paramref name="start"/> and ending after <paramref name="length"/> characters.</returns>
        internal static int GetFNVHashCode(char[] text, int start, int length)
        {
            int hashCode = FnvOffsetBias;
            int end = start + length;
            for (int i = start; i < end; i++)
            {
                hashCode = unchecked((hashCode ^ text[i]) * FnvPrime);
            }

            return hashCode;
        }

        /// <summary>
        /// Compute the hashcode of a single character using the FNV-1a algorithm
        /// See http://en.wikipedia.org/wiki/Fowler%E2%80%93Noll%E2%80%93Vo_hash_function
        /// Note: In general, this isn't any more useful than "char.GetHashCode". However,
        /// it may be needed if you need to generate the same hash code as a string or
        /// substring with just a single character.
        /// </summary>
        /// <param name="ch">The character to hash</param>
        /// <returns>The FNV-1a hash code of the character.</returns>
        internal static int GetFNVHashCode(char ch)
        {
            return CombineFNVHash(FnvOffsetBias, ch);
        }

        /// <summary>
        /// Combine a string with an existing FNV-1a hash code
        /// See http://en.wikipedia.org/wiki/Fowler%E2%80%93Noll%E2%80%93Vo_hash_function
        /// </summary>
        /// <param name="hashCode">The accumulated hash code</param>
        /// <param name="text">The string to combine</param>
        /// <returns>The result of combining <paramref name="hashCode"/> with <paramref name="text"/> using the FNV-1a algorithm</returns>
        internal static int CombineFNVHash(int hashCode, string text)
        {
            foreach (char ch in text)
            {
                hashCode = unchecked((hashCode ^ ch) * FnvPrime);
            }

            return hashCode;
        }

        /// <summary>
        /// Combine a char with an existing FNV-1a hash code
        /// See http://en.wikipedia.org/wiki/Fowler%E2%80%93Noll%E2%80%93Vo_hash_function
        /// </summary>
        /// <param name="hashCode">The accumulated hash code</param>
        /// <param name="ch">The new character to combine</param>
        /// <returns>The result of combining <paramref name="hashCode"/> with <paramref name="ch"/> using the FNV-1a algorithm</returns>
        internal static int CombineFNVHash(int hashCode, char ch)
        {
            return unchecked((hashCode ^ ch) * FnvPrime);
        }
    }
}
