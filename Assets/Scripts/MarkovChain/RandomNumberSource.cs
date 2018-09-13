using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.MarkovChain
{
    internal static class RandomNumberSource
    {
        private static readonly Random random = new Random();

        public static double GetRandomNumber()
        {
            return random.NextDouble();
        }
    }
}
