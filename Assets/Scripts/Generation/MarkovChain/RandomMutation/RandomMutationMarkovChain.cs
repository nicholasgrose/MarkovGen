using System;
using Assets.Scripts.Shared;

namespace Assets.Scripts.Generation.MarkovChain.RandomMutation
{
    public class RandomMutationMarkovChain : IMarkovChain<MapPixel[]>
    {
        public RandomMutationMarkovConfig Config { get; set; }
        public MapPixel[] CurrentValue { get; set; }

        public RandomMutationMarkovChain(RandomMutationMarkovConfig config)
        {
            Config = config;
            CurrentValue = config.StartingPixels;
        }

        public MapPixel[] NextValue()
        {
            for (var i = 0; i < CurrentValue.Length; i++)
            {
                var randomNumber = RandomNumberSource.GetRandomNumber();

                if (randomNumber >= Config.MutationChance)
                {
                    continue;
                }

                ChangePixel(i);
            }

            return CurrentValue;
        }

        private void ChangePixel(int index)
        {
            if (CurrentValue[index] == MapPixel.Land)
            {
                CurrentValue[index] = MapPixel.Water;
            }
            else
            {
                CurrentValue[index] = MapPixel.Land;
            }
        }

        public void Restart()
        {
            CurrentValue = Config.StartingPixels;
        }
    }
}
