using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Shared
{
    public static class RandomNumberSource
    {
        private static readonly Random NumberGenerator = new Random();

        public static double GetRandomNumber()
        {
            return NumberGenerator.NextDouble();
        }
    }
}
