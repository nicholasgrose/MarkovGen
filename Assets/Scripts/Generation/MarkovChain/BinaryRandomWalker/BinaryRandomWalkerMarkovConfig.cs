using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Shared;

namespace Assets.Scripts.Generation.MarkovChain.BinaryRandomWalker
{
    public class BinaryRandomWalkerMarkovConfig
    {
        public double LandToLandWeight { get; set; }
        public double WaterToWaterWeight { get; set; }
        public MapPixel StartValue { get; set; }

        public BinaryRandomWalkerMarkovConfig()
        {
            LandToLandWeight = 0;
            WaterToWaterWeight = 0;
            StartValue = GetRandomPixel();
        }

        public BinaryRandomWalkerMarkovConfig(double landToLandWeight, double waterToWaterWeight)
        {
            LandToLandWeight = landToLandWeight;
            WaterToWaterWeight = waterToWaterWeight;
            StartValue = GetRandomPixel();
        }

        private MapPixel GetRandomPixel()
        {
            var randomValue = RandomNumberSource.GetRandomNumber();

            return randomValue < 0.5 ? MapPixel.Land : MapPixel.Water;
        }
    }
}
