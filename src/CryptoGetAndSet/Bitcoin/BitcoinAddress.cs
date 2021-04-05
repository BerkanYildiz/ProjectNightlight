namespace CryptoGetAndSet.Bitcoin
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Threading.Tasks;

    using CryptoGetAndSet.Helpers;

    internal static class BitcoinAddress
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
            // 
            // Check if the length of the address is between 26 and 35 characters.
            // 

            if (Address.Length < 26 || Address.Length > 35)
            {
                return false;
            }

            // 
            // Decode the bitcoin address.
            // 

            var DecodedAddress = (byte[]) null;

            try
            {
                DecodedAddress = DecodeBase58(Address);
            }
            catch (Exception)
            {
                return false;
            }

            // 
            // Hash the 2 last parts of the address.
            // 

            var Hash1 = (byte[]) null;
            var Hash2 = (byte[]) null;

            using (var Hasher = new SHA256Managed())
            {
                Hash1 = Hasher.ComputeHash(DecodedAddress.SubArray(0, 21));
                Hash2 = Hasher.ComputeHash(Hash1);
            }

            // 
            // Check if the checksum (last part of the address) matches the hash of the 2nd part of the address.
            // 

            if (!DecodedAddress.SubArray(21, 4).SequenceEqual(Hash2.SubArray(0, 4)))
            {
                return false;
            }

            return true;
        }
        
        /// <summary>
        /// Returns true if the passed bitcoin address is valid.
        /// </summary>
        /// <param name="Address">The bitcoin address.</param>
        /// <returns>
        ///   <c>true</c> if the specified address is valid; otherwise, <c>false</c>.
        /// </returns>
        internal static async Task<bool> IsValidAsync(string Address)
        {
            // 
            // Check if the length of the address is between 26 and 35 characters.
            // 

            if (Address.Length < 26 || Address.Length > 35)
            {
                return false;
            }

            // 
            // Decode the bitcoin address.
            // 

            var DecodedAddress = (byte[]) null;

            try
            {
                DecodedAddress = DecodeBase58(Address);
            }
            catch (Exception)
            {
                return false;
            }

            // 
            // Hash the 2 last parts of the address.
            // 

            var Hash1 = (byte[]) null;
            var Hash2 = (byte[]) null;

            using (var Hasher = new SHA256Managed())
            {
                using (var Stream = new MemoryStream(DecodedAddress.SubArray(0, 21)))
                {
                    Hash1 = await Hasher.ComputeHashAsync(Stream);
                }

                using (var Stream = new MemoryStream(Hash1))
                {
                    Hash2 = await Hasher.ComputeHashAsync(Stream);
                }
            }

            // 
            // Check if the checksum (last part of the address) matches the hash of the 2nd part of the address.
            // 

            if (!DecodedAddress.SubArray(21, 4).SequenceEqual(Hash2.SubArray(0, 4)))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Decodes a bitcoin address using the Base58 algorithm.
        /// </summary>
        /// <param name="Address">The bitcoin address.</param>
        private static byte[] DecodeBase58(string Address)
        {
            // 
            // The valid characters a bitcoin address can contain.
            // 

            const string Alphabet = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";
            const int Size = 25;

            // 
            // Decode the bitcoin address.
            // 

            var output = new byte[Size];

            foreach (var t in Address)
            {
                var p = Alphabet.IndexOf(t);

                if (p == -1)
                {
                    throw new Exception("The address contains invalid characters");
                }

                var j = Size;

                while (--j > 0)
                {
                    p += 58 * output[j];
                    output[j] = (byte)(p % 256);
                    p /= 256;
                }

                if (p != 0)
                {
                    throw new Exception("The address is too long");
                }
            }

            return output;
        }
    }
}