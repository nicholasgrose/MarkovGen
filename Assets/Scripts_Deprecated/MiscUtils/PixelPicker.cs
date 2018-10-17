using System;
using System.Linq;
using Assets.Scripts_Deprecated.ProceduralGeneration;

namespace Assets.Scripts_Deprecated.MiscUtils
{
    public static class PixelPicker
    {
        public static MapPixel GetNewRandomPixel()
        {
            var mapPixelValues = ((MapPixel[])Enum.GetValues(typeof(MapPixel))).ToList();

            var randomPixelIndex = (int)RandomNumberSource.GetRandomNumber().Map(0, 1, 0, mapPixelValues.Count);

            return mapPixelValues[randomPixelIndex];
        }

        public static MapPixel GetNewRandomPixel(MapPixel currentPixel)
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
