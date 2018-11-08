using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Shared
{
    public interface IMap
    {
        MapPixel[,] GetMap();

        int Width();
        int Height();

        void SetPixelAt(Vector2Int coordinate, MapPixel value);
        MapPixel GetPixelAt(Vector2Int coordinate);
        MapPixel[] GetAdjacentPixels(Vector2Int coordinate);
    }
}
