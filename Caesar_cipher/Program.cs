using System;
namespace Caesar_cipher
{
    class Program
    {
        static void Main(string[] args)
        {
            var key = GetKey();
            var text = GetText();

            Console.WriteLine("---Encryption---");

            var encryptedText = CaesarCipher.Encrypt(text, key);
            Console.WriteLine(encryptedText);

            Console.WriteLine("---Decryption---");

            var decryptedText = CaesarCipher.Decrypt(encryptedText, key);
            Console.WriteLine(decryptedText);

            Console.WriteLine("---Hacking---");

            var attempts = CaesarCipher.TryHack(encryptedText);
            var output = string.Join("\n", attempts);
            Console.WriteLine(output);

            Console.ReadKey();
        }

        private static int GetKey()
        {
            while (true)
            {
                Console.WriteLine("Input key in range 1 to 33:");

                var key = Console.ReadLine();

                if (int.TryParse(key, out var result))
                {
                    if (result > 0 && result < 33)
                    {
                        return result;
                    }
                }
            }
        }

        public static string GetText()
        {
            while (true)
            {
                Console.WriteLine("Input text:");

                var text = Console.ReadLine();

                if (text.Length > 0)
                {
                    return text;
                }
            }
        }
    }
}
