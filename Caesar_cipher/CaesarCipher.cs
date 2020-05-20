
using System.Collections.Generic;

namespace Caesar_cipher
{
    public class CaesarCipher
    {
        public static string Encrypt(string text, int key)
        {
            var output = string.Empty;

            foreach (var ch in text)
            {
                output += EncryptLetter(ch, key);
            }

            return output;
        }

        public static string Decrypt(string text, int key)
        {
            return Encrypt(text, 32 - key);
        }

        public static IList<string> TryHack(string text)
        {
            var attempts = new List<string>();

            for (int key = 0; key < 33; key++)
            {
                var attempt = Decrypt(text, key);

                attempts.Add(attempt);
            }

            return attempts;
        }

        private static char EncryptLetter(char ch, int key)
        {
            if (!char.IsLetter(ch))
            {
                return ch;
            }

            var a = char.IsUpper(ch) ? 'А' : 'а';

            return (char)((ch + key - a) % 32 + a);
        }
    }
}
