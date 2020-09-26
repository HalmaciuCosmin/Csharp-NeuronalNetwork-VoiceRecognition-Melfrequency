using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audio
{
    class RND
    {
        static Random rnd;

        public static void ini()
        {
            rnd = new Random();
        }

        public static double NextRandomRange(double minimum, double maximum)
        {
            return rnd.NextDouble() * (maximum - minimum) + minimum;
        }

        public static int Next(int n)
        {
            return rnd.Next(n);
        }
    }
}
