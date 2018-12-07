using Assets.Scripts.Shared;
using Assets.Scripts.Shared.Map;

namespace Assets.Scripts.Generation.MapGeneration
{
    public interface IMapGenerator
    {
        IMap GenerateMap(int height, int width);
    }
}
