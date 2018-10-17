using System;
using Assets.Scripts_Deprecated.MiscUtils;

namespace Assets.Scripts_Deprecated.ProceduralGeneration.MarkovMapGenerator.IsingModelMarkov
{
    public class IsingModelMarkovMapGenerator : IMapGenerator
    {
        private readonly int _iterations;
        private readonly int _temperature;

        public IsingModelMarkovMapGenerator(int iterations, int temperature)
        {
            _iterations = iterations;
            _temperature = temperature;
        }

        public MapPixel[,] GenerateMap(int mapWidth, int mapHeight)
        {
            var map = new MapPixel[mapWidth, mapHeight];
            map = FillMapWithWhiteNoise(map);

            for (var iteration = 0; iteration < _iterations; iteration++)
            {
                map = IsingIteration(map);
            }

            return map;
        }

        private static MapPixel[,] FillMapWithWhiteNoise(MapPixel[,] map)
        {
            for (var x = 0; x < map.GetLength(0); x++)
            {
                for (var y = 0; y < map.GetLength(1); y++)
                {
                    map[x, y] = PixelPicker.GetNewRandomPixel();
                }
            }

            return map;
        }

        private MapPixel[,] IsingIteration(MapPixel[,] map)
        {
            var x = (int)RandomNumberSource.GetRandomNumber().Map(0, 1, 0, map.GetLength(0));
            var y = (int)RandomNumberSource.GetRandomNumber().Map(0, 1, 0, map.GetLength(1));
            var valueIfPointIsChanged = PixelPicker.GetNewRandomPixel(map[x, y]);

            var currentEnergyAtLocation = EnergyAtPoint(map, x, y);
            var energyIfLocationIsChanged = EnergyAtPoint(map, x, y, valueIfPointIsChanged);

            var mapShouldChange = DetermineWhetherMapShouldChange(currentEnergyAtLocation, energyIfLocationIsChanged);

            if (mapShouldChange)
            {
                map[x, y] = valueIfPointIsChanged;
            }

            return map;
        }

        private bool DetermineWhetherMapShouldChange(double currentEnergy, double energyIfChanged)
        {
            var randomNumber = RandomNumberSource.GetRandomNumber();

            var currentEnergyMapProbability = Math.Exp(_temperature * currentEnergy);
            var changedEnergyMapProbability = Math.Exp(_temperature * energyIfChanged);

            var ratioOfChangedAndCurrentProbabilities = changedEnergyMapProbability / currentEnergyMapProbability;
            var mapModificationProbability = Math.Min(ratioOfChangedAndCurrentProbabilities, 1);

            return randomNumber < mapModificationProbability;
        }

        private static int EnergyAtPoint(MapPixel[,] map, int x, int y)
        {
            var totalEnergy = 0;
            var isingValueOfGivenPoint = IsingValueOfPixel(map[x, y]);

            if (CoordinateIsInBounds(map, x, y + 1))
            {
                totalEnergy += isingValueOfGivenPoint * IsingValueOfPixel(map[x, y + 1]);
            }
            if (CoordinateIsInBounds(map, x + 1, y))
            {
                totalEnergy += isingValueOfGivenPoint * IsingValueOfPixel(map[x + 1, y]);
            }
            if (CoordinateIsInBounds(map, x, y - 1))
            {
                totalEnergy += isingValueOfGivenPoint * IsingValueOfPixel(map[x, y - 1]);
            }
            if (CoordinateIsInBounds(map, x - 1, y))
            {
                totalEnergy += isingValueOfGivenPoint * IsingValueOfPixel(map[x - 1, y]);
            }

            return totalEnergy;
        }

        private static int EnergyAtPoint(MapPixel[,] map, int x, int y, MapPixel centerValue)
        {
            var totalEnergy = 0;
            var isingValueOfGivenPoint = IsingValueOfPixel(centerValue);

            totalEnergy += IsingValueOfEnergyBetweenCellsWithWrapping(map, isingValueOfGivenPoint, x, y + 1);
            totalEnergy += IsingValueOfEnergyBetweenCellsWithWrapping(map, isingValueOfGivenPoint, x + 1, y);
            totalEnergy += IsingValueOfEnergyBetweenCellsWithWrapping(map, isingValueOfGivenPoint, x, y - 1);
            totalEnergy += IsingValueOfEnergyBetweenCellsWithWrapping(map, isingValueOfGivenPoint, x - 1, y);

            return totalEnergy;
        }

        private static int IsingValueOfEnergyBetweenCellsWithoutWrapping(MapPixel[,] map, int firstCellIsingValue, int x, int y)
        {
            if (CoordinateIsInBounds(map, x, y))
            {
                return firstCellIsingValue * IsingValueOfPixel(map[x, y]);
            }
            else
            {
                return 0;
            }
        }

        private static int IsingValueOfEnergyBetweenCellsWithWrapping(MapPixel[,] map, int firstCellIsingValue, int x, int y)
        {
            if (CoordinateIsInBounds(map, x, y))
            {
                return firstCellIsingValue * IsingValueOfPixel(map[x, y]);
            }
            else
            {
                var xTooLow = x < 0;
                var xTooHigh = x >= map.GetLength(0);
                if (xTooLow)
                {
                    x = map.GetLength(0) - 1;
                }
                else if (xTooHigh)
                {
                    x = 0;
                }

                var yTooLow = y < 0;
                var yTooHigh = y >= map.GetLength(0);
                if (yTooLow)
                {
                    y = map.GetLength(1) - 1;
                }
                else if (yTooHigh)
                {
                    y = 0;
                }
                
                return firstCellIsingValue * IsingValueOfPixel(map[x, y]);
            }
        }

        private static int IsingValueOfPixel(MapPixel mapPixel)
        {
            switch (mapPixel)
            {
                case MapPixel.LAND:
                    return 1;
                case MapPixel.WATER:
                    return -1;
                default:
                    return 0;
            }
        }

        private static bool CoordinateIsInBounds(MapPixel[,] map, int x, int y)
        {
            var mapWidth = map.GetLength(0);
            var mapHeight = map.GetLength(1);

            var xIsInBounds = 0 <= x && x < mapWidth;
            var yIsInBounds = 0 <= y && y < mapHeight;

            return xIsInBounds && yIsInBounds;
        }
    }
}
