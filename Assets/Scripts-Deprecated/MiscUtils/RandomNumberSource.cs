using System;

namespace Assets.MiscUtils
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
