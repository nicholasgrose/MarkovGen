using Assets.Scripts.Shared;
using Assets.Scripts.Shared.Map;

namespace Assets.Scripts.Generation.MapGeneration
{
    public interface IMapGenerator
    {
        ITwoDimensionalMap GenerateMap(int height, int width);
    }
}
