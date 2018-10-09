namespace Assets.ProceduralGeneration
{
    public interface IMapGenerator
    {
        MapPixel[,] GenerateMap(int mapWidth, int mapHeight);
    }
}
