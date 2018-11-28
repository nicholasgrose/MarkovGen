using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Shared.Map
{
    public class TwoDimensionalMap : ITwoDimensionalMap
    {
        private MapPixel[,] _map;

        public TwoDimensionalMap(int width, int height)
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

        public void SetPixelAt(Vector2Int coordinate, MapPixel value)
        {
            _map[coordinate.x, coordinate.y] = value;
        }

        public MapPixel GetPixelAt(Vector2Int coordinate)
        {
            return _map[coordinate.x, coordinate.y];
        }

        public MapPixel[] GetAdjacentPixels(Vector2Int coordinate)
        {
            throw new NotImplementedException();
        }
    }
}
