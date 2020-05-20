using System;
using System.IO;

namespace BPtD_Lab_3
{
    public static class HashCollisionUtil
    {
        public static CollisionResult FindCollision(string hash, int bitLength)
        {
            for (long i = 0; i < long.MaxValue; i++)
            {
                var byteContent = BitConverter.GetBytes(i);
                var testHash = HashUtil.GetHash(byteContent, bitLength);

                if (testHash == hash)
                {
                    var text = Convert.ToBase64String(byteContent);

                    return new CollisionResult(text, i + 1);
                }
            }

            return new CollisionResult();
        }

        public static bool FindSimilarImage(string hash, byte[] file, int bitLength)
        {
            var count = 0;

            for (int i = file.Length / 4; i < file.Length; i++)
            {
                file[i] += 1;
                var testedHash = HashUtil.GetHash(file, bitLength);

                if (testedHash == hash && count >= 500)
                {
                    Console.WriteLine("Collision founded");

                    using var fileInfo =
                        File.Open("../BPTD_Lab_3/image_collision.jpg",
                            FileMode.OpenOrCreate);

                    fileInfo.Write(file);

                    return true;
                }

                count++;
            }

            return false;
        }
    }
}
