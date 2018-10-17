using System;

namespace Assets.Scripts_Deprecated.MiscUtils
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
