namespace Assets.Scripts_Deprecated.ProceduralGeneration
{
    public interface IMapGenerator
    {
        MapPixel[,] GenerateMap(int mapWidth, int mapHeight);
    }
}
