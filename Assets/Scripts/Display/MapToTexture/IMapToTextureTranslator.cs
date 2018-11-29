using Assets.Scripts.Shared;
using Assets.Scripts.Shared.Map;
using UnityEngine;

namespace Assets.Scripts.Display.MapToTexture
{
    public interface IMapToTextureTranslator
    {
        Texture2D TranslateMapToTexture(ITwoDimensionalMap map);
    }
}
