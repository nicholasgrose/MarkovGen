﻿using System.Collections.Generic;
using Assets.Scripts_Deprecated.MarkovChain;

namespace Assets.Scripts_Deprecated.ProceduralGeneration.MarkovMapGenerator.PureMarkov
{
    public class CrossRowMarkovMapGenerator : IMapGenerator
    {
        private readonly double _landWaterConnectionWeight;
        private readonly double _waterLandConnectionWeight;

        public CrossRowMarkovMapGenerator(double landWaterConnectionWeight, double waterLandConnectionWeight)
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
                for (var y = 0; y < mapHeight; y++)
                {
                    map[x, y] = markovChain.NextValue();
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
    }
}
