using System;
using System.IO;
using System.Linq;
using System.Text;

namespace BPtD_Lab_3
{
    class Program
    {
        static void Main(string[] args)
        {
            //ComputeStringHash();

            ComputeFilesHash();

            ComputeMd5Hash();

            CollisionTest();

            Console.ReadKey();
        }

        private static void ComputeMd5Hash()
        {
            var image = File.ReadAllBytes("Files/image.jpg");
            var sourceCode = File.ReadAllBytes("Files/SourceCode.cs");
            var wordDoc = File.ReadAllBytes("Files/word_doc.docx");

            var imageHash = HashUtil.Md5Hash(image);
            var sourceHash = HashUtil.Md5Hash(sourceCode);
            var wordDocHash = HashUtil.Md5Hash(wordDoc);

            Console.WriteLine($"image -> {imageHash}");
            Console.WriteLine($"source -> {sourceHash}");
            Console.WriteLine($"doc file -> {wordDocHash}");
        }

        private static void ComputeStringHash()
        {
            while (true)
            {
                Console.WriteLine("Input text:");
                var text = Console.ReadLine();

                if(text.Trim() == "exit")
                    break;

                Console.WriteLine("Input bit length (2, 4, 8):");
                var bitLength = Console.ReadLine();

                var hash = HashUtil.GetHash(Encoding.UTF8.GetBytes(text), int.Parse(bitLength));

                Console.WriteLine($"Hash: {hash}");
            }
        }

        private static void ComputeFilesHash()
        {
            var image = File.ReadAllBytes("Files/image.jpg");
            var sourceCode = File.ReadAllBytes("Files/SourceCode.cs");
            var wordDoc = File.ReadAllBytes("Files/word_doc.docx");

            CalculateHash(image, "image");
            CalculateHash(sourceCode, "source code");
            CalculateHash(wordDoc, "word document");
        }

        private static void CalculateHash(byte[] file, string name)
        {
            var twoBit = HashUtil.GetHash(file, 2);
            var fourBit = HashUtil.GetHash(file, 4);
            var eightBit = HashUtil.GetHash(file, 8);

            Console.WriteLine(name);
            Console.WriteLine($"2-bit -> {twoBit}");
            Console.WriteLine($"4-bit -> {fourBit}");
            Console.WriteLine($"8-bit -> {eightBit}");
            Console.WriteLine();
        }

        private static void CollisionTest()
        {
            var image = File.ReadAllBytes("Files/image.jpg");

            var hash = HashUtil.GetHash(image, 4);

            HashCollisionUtil.FindSimilarImage(hash, image, 4);
        }
    }
}
