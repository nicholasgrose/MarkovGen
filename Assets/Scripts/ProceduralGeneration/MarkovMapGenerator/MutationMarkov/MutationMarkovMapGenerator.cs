using Assets.Scripts.MarkovChain;
using Assets.Scripts.MiscUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ProceduralGeneration.MarkovMapGenerator.MutationMarkov
{
    public class MutationMarkovMapGenerator : MapGenerator
    {
        private readonly double _landWaterConnectionWeight;
        private readonly double _waterLandConnectionWeight;
        private readonly int _minimumMutations;
        private readonly int _maximumMutations;

        public MutationMarkovMapGenerator(double landWaterConnectionWeight, double waterLandConnectionWeight, int minimumMutations, int maximumMutations)
        {
            _landWaterConnectionWeight = landWaterConnectionWeight;
            _waterLandConnectionWeight = waterLandConnectionWeight;
            _minimumMutations = minimumMutations;
            _maximumMutations = maximumMutations;
        }

        public MapPixel[,] GenerateMap(int mapWidth, int mapHeight)
        {
            var markovChain = GetMarkovChain();
            var map = new MapPixel[mapWidth, mapHeight];

            var mutationSequence = GetRandomSequence(mapWidth, markovChain);

            for (var y = 0; y < mapHeight; y++)
            {
                for (var x = 0; x < mapWidth; x++)
                {
                    map[x, y] = mutationSequence[x];
                }
                
                var mutationCount = (int)Math.Round(RandomNumberSource.GetRandomNumber().Map(0, 1, _minimumMutations, _maximumMutations));
                mutationSequence = MutateSequence(mutationSequence, mutationCount);
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

        private static MapPixel[] GetRandomSequence(int sequenceLength, MarkovChain<MapPixel> sequenceSource)
        {
            var sequence = new MapPixel[sequenceLength];
            for (var index = 0; index < sequence.Length; index++)
            {
                sequence[index] = sequenceSource.NextValue();
            }
            return sequence;
        }
        
        private MapPixel[] MutateSequence(MapPixel[] mutationSequence, int mutationCount)
        {
            var indecesForChanging = new List<int>(mutationCount);

            if (mutationCount >= mutationSequence.Length)
            {
                for (var index = 0; index < mutationSequence.Length; index++)
                {
                    indecesForChanging.Add(index);
                }
            }
            else
            {
                for (var mutation = 0; mutation < mutationCount; mutation++)
                {
                    int randomIndex;
                    do
                    {
                        randomIndex = (int)RandomNumberSource.GetRandomNumber().Map(0, 1, 0, mutationSequence.Length);
                    } while (indecesForChanging.Contains(randomIndex));

                    indecesForChanging.Add(randomIndex);
                }
            }

            foreach (var index in indecesForChanging)
            {
                mutationSequence[index] = GetNewRandomPixel(mutationSequence[index]);
            }

            return mutationSequence;
        }

        private MapPixel GetNewRandomPixel(MapPixel currentPixel)
        {
            var mapPixelValues = ((MapPixel[])Enum.GetValues(typeof(MapPixel))).ToList();

            int randomPixelIndex;
            do
            {
                randomPixelIndex = (int)RandomNumberSource.GetRandomNumber().Map(0, 1, 0, mapPixelValues.Count);
            } while (mapPixelValues[randomPixelIndex] == currentPixel);

            return mapPixelValues[randomPixelIndex];
        }
    }
}
