using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ProceduralGeneration
{
    public interface IMapGenerator
    {
        MapPixel[,] GenerateMap(int mapWidth, int mapHeight);
    }
}
