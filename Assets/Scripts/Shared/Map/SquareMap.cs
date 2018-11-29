using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Shared.Coordinate;
using UnityEngine;

namespace Assets.Scripts.Shared.Map
{
    public class SquareMap : IMap
    {
        private MapPixel[,] _map;

        public SquareMap(int width, int height)
        {
            _map = new MapPixel[width, height];
        }

        public int Width()
        {
            return _map.GetLength(0);
        }

        public int Height()
        {
            return _map.GetLength(1);
        }

        public MapPixel[,] GetArrayRepresentation()
        {
            return _map;
        }

        public MapPixel GetPixelAt(IThreeDimensionalCoordinate coordinate)
        {
            return _map[coordinate.GetX(), coordinate.GetY()];
        }

        public void SetPixelAt(IThreeDimensionalCoordinate coordinate, MapPixel value)
        {
            _map[coordinate.GetX(), coordinate.GetY()] = value;
        }

        public ICollection<MapPixel> GetAdjacentPixels(IThreeDimensionalCoordinate coordinate)
        {
            var adjacentPixels = new List<MapPixel>();

            AddAdjacentPixelsOnX(adjacentPixels, coordinate);
            AddAdjacentPixelsOnY(adjacentPixels, coordinate);

            return adjacentPixels;
        }

        private void AddAdjacentPixelsOnX(ICollection<MapPixel> adjacentPixels, IThreeDimensionalCoordinate coordinate)
        {
            var currentCoordinate = new MapCoordinate(coordinate.GetX(), coordinate.GetY());

            for (var xOff = -1; xOff <= 1; xOff+= 2)
            {
                currentCoordinate.SetX(coordinate.GetX() + xOff);

                if (CoordinateIsInBounds(coordinate))
                {
                    adjacentPixels.Add(_map[currentCoordinate.GetX(), currentCoordinate.GetY()]);
                }
            }
        }

        private void AddAdjacentPixelsOnY(ICollection<MapPixel> adjacentPixels, ITwoDimensionalCoordinate coordinate)
        {
            var currentCoordinate = new MapCoordinate(coordinate.GetX(), coordinate.GetY());

            for (var yOff = -1; yOff <= 1; yOff += 2)
            {
                currentCoordinate.SetY(coordinate.GetY() + yOff);

                if (CoordinateIsInBounds(coordinate))
                {
                    adjacentPixels.Add(_map[currentCoordinate.GetX(), currentCoordinate.GetY()]);
                }
            }
        }

        private bool CoordinateIsInBounds(ITwoDimensionalCoordinate coordinate)
        {
            var x = coordinate.GetX();
            var y = coordinate.GetY();

            return (x >= 0 && x < Width()) &&
                   (y >= 0 && y < Height());
        }
    }
}
