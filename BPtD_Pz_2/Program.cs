using System;

namespace BPtD_Pz_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var mode = string.Empty;

            while (mode != "0")
            {
                mode = GetMode();

                if (mode == "1")
                {
                    Encrypt();
                }
                else
                {
                    Decrypt();
                }
            }

            Console.ReadKey();
        }

        private static string GetMode()
        {
            while (true)
            {
                Console.WriteLine("Input mode:");
                Console.WriteLine("1 - encrypt, 2 -decrypt, 0 -exit");
                var text = Console.ReadLine();

                if (text.Length == 1 && (text == "1" || text == "2" || text == "0"))
                {
                    return text;
                }
            }
        }

        private static void Encrypt()
        {
            var text = GetText();
            var key = GetKey();

            var pad = new CustomOnTimePad();

            var encrypt = pad.Encrypt(text, key);

            Console.WriteLine("Original: " + text);
            Console.WriteLine("Encrypted text: " + encrypt);
        }

        private static void Decrypt()
        {
            var text = GetText();
            var key = GetKey();

            var pad = new CustomOnTimePad();

            var decrypt = pad.Decrypt(text, key);

            Console.WriteLine("Original: " + text);
            Console.WriteLine("Decrypted text: " + decrypt);
        }

        private static string GetText()
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

        private static string GetKey()
        {
            while (true)
            {
                Console.WriteLine("Input key:");

                var key = Console.ReadLine();

                if (key.Length > 0)
                {
                    return key;
                }
            }
        }

    }
}