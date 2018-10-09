using Assets.Scripts.Shared;

namespace Assets.Scripts.Generation.MapGeneration
{
    public interface IMapGenerator
    {
        IMap GenerateMap(int height, int width);
    }
}
