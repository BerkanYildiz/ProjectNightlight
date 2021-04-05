namespace CryptoGetAndSet.Crypto
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Threading.Tasks;

    using CryptoGetAndSet.Helpers;

    internal static class EthereumAddress
    {
        /// <summary>
        /// Returns true if the passed bitcoin address is valid.
        /// </summary>
        /// <param name="Address">The bitcoin address.</param>
        /// <returns>
        ///   <c>true</c> if the specified address is valid; otherwise, <c>false</c>.
        /// </returns>
        internal static bool IsValid(string Address)
        {
            if (string.IsNullOrEmpty(Address))
            {
                return false;
            }

            // 
            // Check if the address starts with a '0x'.
            // 

            if (!Address.StartsWith("0x"))
            {
                return false;
            }

            // 
            // Check if the length of the address is exactly 40.
            // 

            Address = Address.Replace("0x", string.Empty);

            if (Address.Length != 40)
            {
                return false;
            }

            // 
            // Check if the address only contains hexadecimal characters.
            // 

            if (!Address.All("0123456789abcdefABCDEF".Contains))
            {
                return false;
            }

            return true;
        }
    }
}