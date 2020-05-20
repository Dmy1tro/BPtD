using System;
using System.Numerics;

namespace DiffieHellmanTool
{
    public static class DiffieHellmanService
    {
        public static int GetQ() => NumbersHelper.GetPrime();

        public static int GetA(int q) => NumbersHelper.GetPrimitiveRoot(q);

        public static int GetX(int q) => NumbersHelper.GetRandom(1, q);

        public static BigInteger GetY(int a, int x, int q) => BigInteger.ModPow(a, x, q);

        public static BigInteger GetKey(BigInteger y, int x, int q) => BigInteger.ModPow(y, x, q);
    }
}
