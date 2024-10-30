// ***********************************************************************
//  Assembly         : RzR.Shared.Services.LinkShortener
//  Author           : RzR
//  Created On       : 2024-10-21 23:04
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-10-23 20:01
// ***********************************************************************
//  <copyright file="CodeGeneratorHelper.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

namespace LinkShortener.Helpers
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     A code generator helper.
    /// </summary>
    /// =================================================================================================
    internal static class CodeGeneratorHelper
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the alphabet.
        /// </summary>
        /// =================================================================================================
        private static readonly string[] Alphabet =
        {
            "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", 
            "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
            "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"
        };

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Generates an URL code.
        /// </summary>
        /// <param name="codeLength">(Optional) Length of the code.</param>
        /// <returns>
        ///     The URL code.
        /// </returns>
        /// =================================================================================================
        internal static string GenerateUrlCode(int codeLength)
        {
            var code = new string[codeLength];

            for (var i = 0; i < codeLength; i++)
            {
                var n = RandomHelper.Instance.Number(0, Alphabet.Length - 1);
                var letter = Alphabet[n];
                code[i] = letter;
            }

            return string.Join(string.Empty, code);
        }
    }
}