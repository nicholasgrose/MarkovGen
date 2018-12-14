using System;
using Assets.Scripts.Shared;

namespace Assets.Scripts.Generation.MarkovChain.BinaryRandomWalker
{
    public class BinaryRandomWalkerMarkovChain : IMarkovChain<MapPixel>
    {
        public BinaryRandomWalkerMarkovConfig Config { get; set; }
        private MapPixel _currentValue;

        public BinaryRandomWalkerMarkovChain(BinaryRandomWalkerMarkovConfig config)
        {
            Config = config;
            _currentValue = config.StartValue;
        }

        public MapPixel NextValue()
        {
            var randomNumber = RandomNumberSource.GetRandomNumber();

            if (_currentValue == MapPixel.Land)
            {
                if (randomNumber >= Config.LandToLandWeight)
                {
                    _currentValue = MapPixel.Water;
                }
            }
            else
            {
                if (randomNumber >= Config.WaterToWaterWeight)
                {
                    _currentValue = MapPixel.Land;
                }
            }

            return _currentValue;
        }

        public void RestartChain()
        {
            _currentValue = Config.StartValue;
        }
    }
}
