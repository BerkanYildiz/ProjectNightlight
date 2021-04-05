namespace CryptoGetAndSet.Crypto
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Threading.Tasks;

    using CryptoGetAndSet.Helpers;

    using Org.BouncyCastle.Crypto.Digests;

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
            // Check if the address only contains alpha-numeric characters.
            // 

            if (!Address.All("123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz".Contains))
            {
                return false;
            }

            // 
            // Decode the address using Base58.
            // 

            var DecodedAddress = Base58.ToByteArray(Address);

            if (DecodedAddress == null ||
                DecodedAddress.Length < 4)
            {
                return false;
            }

            // 
            // Parse the address without its ending checksum.
            // 

            var LengthWithoutChecksum = DecodedAddress.Length - 4;
            var BytesWithoutChecksum = new byte[LengthWithoutChecksum];
            Array.Copy(DecodedAddress, BytesWithoutChecksum, LengthWithoutChecksum);

            // 
            // Calculate the checksum ourselves.
            // 

            var ChecksumCalculator = new Sha256Digest();
            ChecksumCalculator.BlockUpdate(BytesWithoutChecksum, 0, BytesWithoutChecksum.Length);
            var Checksum = new byte[32];
            ChecksumCalculator.DoFinal(Checksum, 0);
            ChecksumCalculator.BlockUpdate(Checksum, 0, Checksum.Length);
            ChecksumCalculator.DoFinal(Checksum, 0);

            // 
            // Compare the checksum we've calculated ourselves with the one
            // we parsed at the end of the address we've been given.
            // 

            if (Checksum[0] != DecodedAddress[LengthWithoutChecksum] ||
                Checksum[1] != DecodedAddress[LengthWithoutChecksum + 1] ||
                Checksum[2] != DecodedAddress[LengthWithoutChecksum + 2] ||
                Checksum[3] != DecodedAddress[LengthWithoutChecksum + 3])
            {
                return false;
            }

            return true;
        }
    }
}