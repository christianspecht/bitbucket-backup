using System;
using System.Security.Cryptography;
using System.Text;

namespace BitbucketBackup
{
    /// <summary>
    /// Encrypts and decrypts strings.
    /// </summary>
    internal static class StringEncryptor
    {
        /// <summary>
        /// entropy for password encryption
        /// </summary>
        private readonly static byte[] entropy = { 1, 2, 3, 4 };

        /// <summary>
        /// Encrypts the given string.
        /// </summary>
        /// <param name="input">The string to encrypt</param>
        /// <returns>The encrypted string</returns>
        public static string Encrypt(string input)
        {
            byte[] unencrypted = Encoding.Unicode.GetBytes(input);
            byte[] encrypted = ProtectedData.Protect(unencrypted, entropy, DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encrypted);
        }

        /// <summary>
        /// Decrypts the given encrypted string.
        /// </summary>
        /// <param name="input">The encrypted string to decrypt</param>
        /// <returns>The decrypted string</returns>
        public static string Decrypt(string input)
        {
            byte[] encrypted = Convert.FromBase64String(input);
            byte[] decrypted = ProtectedData.Unprotect(encrypted, entropy, DataProtectionScope.CurrentUser);
            return Encoding.Unicode.GetString(decrypted);
        }
    }
}
