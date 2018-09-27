using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.MarkovChain;

namespace Assets.Scripts.ProceduralGeneration.MarkovMapGenerator.PureMarkov
{
    public class LeftCornerMarkovMapGenerator : IMapGenerator
    {
        private readonly double _landWaterConnectionWeight;
        private readonly double _waterLandConnectionWeight;

        public LeftCornerMarkovMapGenerator(double landWaterConnectionWeight, double waterLandConnectionWeight)
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

                if (x == 0)
                {
                    for (var y = 0; y < mapHeight; y++)
                    {
                        map[x, y] = columnChain.NextValue();
                    }
                }
                else
                {
                    for (var y = 0; y < mapHeight; y++)
                    {
                        if (y == 0)
                        {
                            map[x, y] = columnChain.NextValue();
                        }
                        else
                        {
                            map[x, y] = columnChain.NextValue(ModeToTopLeftOfPoint(x, y, map));
                        }
                    }
                }
            }

            return map;
        }

        private static MapPixel ModeToTopLeftOfPoint(int x, int y, MapPixel[,] map)
        {
            var mapPixelValues = ((MapPixel[]) Enum.GetValues(typeof(MapPixel))).ToList();
            var pixelCount = new List<int>();

            foreach (var pixel in mapPixelValues)
            {
                pixelCount.Add(0);
            }

            pixelCount[mapPixelValues.IndexOf(map[x - 1, y])] += 1;
            pixelCount[mapPixelValues.IndexOf(map[x - 1, y - 1])] += 1;
            pixelCount[mapPixelValues.IndexOf(map[x, y - 1])] += 1;

            return mapPixelValues[pixelCount.IndexOf(pixelCount.Max())];
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
