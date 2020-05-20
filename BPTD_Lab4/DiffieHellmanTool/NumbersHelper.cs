using System;
using System.Collections.Generic;

namespace DiffieHellmanTool
{
    public static class NumbersHelper
    {
        private static readonly Random Random = new Random();

        public static int GetPrime()
        {
            var primes = GetPrimes();
            var index = GetRandom(0, primes.Length);

            return primes[index];
        }

        public static int GetPrimitiveRoot(int n)
        {
            for (int i = 0; i < n; i++)
            {
                if (IsPrimitiveRoot(i, n))
                {
                    return i;
                }
            }

            return 0;
        }

        private static bool IsPrimitiveRoot(int num, int n)
        {
            var temp = 1;
            var stack = new Stack<int>();

            for (int i = 0; i < n-1; i++)
            {
                temp = (temp * num) % n;

                if (stack.Contains(temp))
                {
                    return false;
                }

                stack.Push(temp);
            }

            return true;
        }

        public static int GetRandom(int min, int max) => Random.Next(min, max);

        private static int[] GetPrimes() =>
            new[]
            {
                43, 101, 103, 107, 109, 137, 139, 347, 349 
            };
    }
}
