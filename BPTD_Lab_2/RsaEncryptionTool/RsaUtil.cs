using System;
using System.Security.Cryptography;
using System.Text;

namespace RsaEncryptionTool
{
    public static class RsaUtil
    {
        public const int KeySize = 2048;

        public static RsaKey GenerateRsaKey()
        {
            using (var rsaProvider = new RSACryptoServiceProvider(KeySize))
            {
                var publicKey = rsaProvider.ToXmlString(false);
                var privateKey = rsaProvider.ToXmlString(true);

                var rsaKey = new RsaKey(publicKey, privateKey);

                return rsaKey;
            }
        }

        public static string Encrypt(string text, string publicKey)
        {
            using (var rsaProvider = new RSACryptoServiceProvider(KeySize))
            {
                rsaProvider.FromXmlString(publicKey);

                var textInBytes = Encoding.UTF8.GetBytes(text);
                var encrypted = rsaProvider.Encrypt(textInBytes, false);

                var encryptedString = Convert.ToBase64String(encrypted);

                return encryptedString;
            }
        }

        public static string Decrypt(string text, string privateKey)
        {
            using (var rsaProvider = new RSACryptoServiceProvider(KeySize))
            {
                rsaProvider.FromXmlString(privateKey);

                var textInBytes = Convert.FromBase64String(text);
                var decrypted = rsaProvider.Decrypt(textInBytes, false);

                var decryptedString = Encoding.UTF8.GetString(decrypted);

                return decryptedString;
            }
        }
    }
}
