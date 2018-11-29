using System.Collections.Generic;
using Assets.Scripts.Shared.Coordinate;
using UnityEngine;

namespace Assets.Scripts.Shared.Map
{
    public interface ITwoDimensional
    {
        int Width();
        int Height();
    }

    public interface IMap : ITwoDimensional
    {
        MapPixel[,] GetArrayRepresentation();
        MapPixel GetPixelAt(IThreeDimensionalCoordinate coordinate);
        void SetPixelAt(IThreeDimensionalCoordinate coordinate, MapPixel value);
        ICollection<MapPixel> GetAdjacentPixels(IThreeDimensionalCoordinate coordinate);
    }
}
