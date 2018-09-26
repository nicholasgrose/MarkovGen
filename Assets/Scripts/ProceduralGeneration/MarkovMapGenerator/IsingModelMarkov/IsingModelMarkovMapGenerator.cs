using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ProceduralGeneration.MarkovMapGenerator.IsingModelMarkov
{
    public class IsingModelMarkovMapGenerator : MapGenerator
    {
        private int _iterations;
        private int _temperature;

        public IsingModelMarkovMapGenerator(int iterations, int temperature)
        {
            _iterations = iterations;
            _temperature = temperature;
        }

        public MapPixel[,] GenerateMap(int mapWidth, int mapHeight)
        {
            MapPixel[,] map = new MapPixel[mapWidth, mapHeight];

            throw new NotImplementedException();
        }
    }
}
