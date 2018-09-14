using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.MarkovChain;

namespace Assets.Scripts.ProceduralGeneration.MarkovMapGenerator.PureMarkov
{
    public class IndependentRowMarkovMapGenerator : MapGenerator
    {
        private readonly double _landWaterConnectionWeight;
        private readonly double _waterLandConnectionWeight;

        public IndependentRowMarkovMapGenerator(double landWaterConnectionWeight, double waterLandConnectionWeight)
        {
            _landWaterConnectionWeight = landWaterConnectionWeight;
            _waterLandConnectionWeight = waterLandConnectionWeight;
        }

        public MapPixel[,] GenerateMap(int mapWidth, int mapHeight)
        {
            var markovChain = GetMarkovChain();
            var map = new MapPixel[mapWidth, mapHeight];

            for (var x = 0; x < mapWidth; x++)
            {
                var columnChain = GetMarkovChain(markovChain.NextValue());

                for (var y = 0; y < mapHeight; y++)
                {
                    map[x, y] = columnChain.NextValue();
                }
            }

            return map;
        }

        private MarkovChain<MapPixel> GetMarkovChain()
        {
            var landNode = new MarkovNode<MapPixel>(MapPixel.LAND);
            var waterNode = new MarkovNode<MapPixel>(MapPixel.WATER);

            landNode.AddConnection(waterNode, _landWaterConnectionWeight);
            waterNode.AddConnection(landNode, _waterLandConnectionWeight);

            var nodeList = new List<MarkovNode<MapPixel>>
            {
                landNode,
                waterNode
            };

            return new MarkovChain<MapPixel>(nodeList);
        }

        private MarkovChain<MapPixel> GetMarkovChain(MapPixel startValue)
        {
            var landNode = new MarkovNode<MapPixel>(MapPixel.LAND);
            var waterNode = new MarkovNode<MapPixel>(MapPixel.WATER);

            landNode.AddConnection(waterNode, _landWaterConnectionWeight);
            waterNode.AddConnection(landNode, _waterLandConnectionWeight);

            var nodeList = new List<MarkovNode<MapPixel>>
            {
                landNode,
                waterNode
            };

            return new MarkovChain<MapPixel>(nodeList, nodeList.FirstOrDefault(node => node.NodeValue == startValue));
        }
    }
}
