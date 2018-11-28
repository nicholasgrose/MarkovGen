using UnityEngine;

namespace Assets.Scripts.Shared.Map
{
    public interface ITwoDimensional
    {
        int Width();
        int Height();
    }

    public interface ITwoDimensionalMap : ITwoDimensional
    {
        void SetPixelAt(Vector2Int coordinate, MapPixel value);
        MapPixel GetPixelAt(Vector2Int coordinate);
        MapPixel[] GetAdjacentPixels(Vector2Int coordinate);
    }
}
