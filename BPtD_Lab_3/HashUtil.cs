using System;
using System.Numerics;
using System.Security.Cryptography;

namespace BPtD_Lab_3
{
    public static class HashUtil
    {
        public static string GetHash(byte[] content, int bitLength)
        {
            var p1 = 103;
            var p2 = 101;
            BigInteger hash = 0;

            foreach (var element in content)
            {
                hash = hash * p1 + element * p2;

                var temp = p1;
                p1 = p2;
                p2 = temp;
            }

            var longHash = (long)BigInteger.ModPow(hash, 1, long.MaxValue);

            longHash = Math.Abs(longHash);
            longHash %= (long)Math.Pow(2, bitLength) - 1;

            return Convert.ToString(longHash, 2).PadLeft(bitLength, '0');
        }

        public static string Md5Hash(byte[] file)
        {
            var md5 = new MD5CryptoServiceProvider();

            var computedHash = md5.ComputeHash(file);

            return Convert.ToBase64String(computedHash);
        }
    }
}
