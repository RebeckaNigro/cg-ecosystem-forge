using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Util.Const
{
    public static class RandomNumberGeneartor
    {
        private static readonly Random _random = new Random();

        // Generates a random number within a range.      
        public static int Generate(int min, int max)
        {
            return _random.Next(min, max);
        }
    }
}
