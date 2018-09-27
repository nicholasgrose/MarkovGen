using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.ProceduralGeneration;

namespace Assets.Scripts.MiscUtils
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
