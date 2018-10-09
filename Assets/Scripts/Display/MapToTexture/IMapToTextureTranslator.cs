using Assets.Scripts.Shared;
using UnityEngine;

namespace Assets.Scripts.Display.MapToTexture
{
    public interface IMapToTextureTranslator
    {
        Texture2D TranslateMapToTexture(IMap map);
    }
}
