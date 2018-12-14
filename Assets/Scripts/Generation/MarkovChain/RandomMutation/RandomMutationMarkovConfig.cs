using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Generation.MarkovChain.BinaryRandomWalker;
using Assets.Scripts.Shared;

namespace Assets.Scripts.Generation.MarkovChain.RandomMutation
{
    public class RandomMutationMarkovConfig
    {
        public double MutationChance { get; set; }
        public MapPixel[] StartingPixels { get; set; }

        public RandomMutationMarkovConfig()
        {
            MutationChance = 0;
            StartingPixels = new MapPixel[0];
        }

        public RandomMutationMarkovConfig(double mutationChance, int startingPixelCount)
        {
            MutationChance = mutationChance;
            StartingPixels = GetRandomPixels(startingPixelCount);
        }

        public RandomMutationMarkovConfig(BinaryRandomWalkerMarkovConfig seedChainConfig, double mutationChance, int startingPixelCount)
        {
            MutationChance = mutationChance;
            StartingPixels = GetRandomPixels(startingPixelCount, seedChainConfig);
        }

        private MapPixel[] GetRandomPixels(int startingPixelCount)
        {
            var randomPixels = new MapPixel[startingPixelCount];

            for (var i = 0; i < randomPixels.Length; i++)
            {
                randomPixels[i] = GetRandomPixel();
            }

            return randomPixels;
        }

        private MapPixel GetRandomPixel()
        {
            var randomNumber = RandomNumberSource.GetRandomNumber();

            return randomNumber < 0.5 ? MapPixel.Land : MapPixel.Water;
        }

        private MapPixel[] GetRandomPixels(int startingPixelCount, BinaryRandomWalkerMarkovConfig initialPixelSeedConfig)
        {
            var randomPixels = new MapPixel[startingPixelCount];
            IMarkovChain<MapPixel> seedChain = new BinaryRandomWalkerMarkovChain(initialPixelSeedConfig);

            for (var i = 0; i < randomPixels.Length; i++)
            {
                randomPixels[i] = seedChain.NextValue();
            }

            return randomPixels;
        }
    }
}
