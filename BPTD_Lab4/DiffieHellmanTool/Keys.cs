using System.Numerics;

namespace DiffieHellmanTool
{
    public class Keys
    {
        public Keys(int q, int a, int x, BigInteger y)
        {
            Q = q;
            A = a;
            X = x;
            Y = y;
        }

        public int Q { get; set; }

        public int A { get; set; }

        public int X { get; set; }

        public BigInteger Y { get; set; }

        public BigInteger Key { get; set; }
    }
}
