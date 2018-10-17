using Assets.Scripts_Deprecated.ProceduralGeneration;
using Assets.Scripts_Deprecated.ProceduralGeneration.MarkovMapGenerator.MutationMarkov;
using UnityEngine;

namespace Assets.Scripts_Deprecated
{
    public class DisplayMutatedChain : MonoBehaviour
    {
        [Tooltip("The resolution of the texture on the x-axis")]
        public int TextureWidth;
        [Tooltip("The resolution of the texture on the y-axis")]
        public int TextureHeight;
        [Range(0, 1)]
        [Tooltip("The chance that a land pixel will turn into a water pixel in the chain")]
        public double LandToWaterWeight;
        [Range(0, 1)]
        [Tooltip("The chance that a water pixel will turn into a land pixel in the chain")]
        public double WaterToLandWeight;
        [Range(0, 1024)]
        [Tooltip("The minimum number of mutations that can occur")]
        public int MinimumMutations;
        [Range(0, 1024)]
        [Tooltip("The maximum number of mutations that can occur")]
        public int MaximumMutations;
        [Tooltip("Selects the specific algorithm being used to generate the map")]
        public MutationMarkovGenAlgorithm GenerationAlgorithm;
        [Tooltip("The object the generated texture is displayed on")]
        public GameObject DisplaySurface;

        private IMapGenerator _mapGenerator;

        // Use this for initialization
        private void Start()
        {
            _mapGenerator = GetMarkovMapGenerator();
            var map = _mapGenerator.GenerateMap(TextureWidth, TextureHeight);
            DisplayMap(map);
        }

        private IMapGenerator GetMarkovMapGenerator()
        {
            switch (GenerationAlgorithm)
            {
                case MutationMarkovGenAlgorithm.StandardMutation:
                    return new MutationMarkovMapGenerator(LandToWaterWeight, WaterToLandWeight, MinimumMutations, MaximumMutations);
                default:
                    return null;
            }
        }

        private void DisplayMap(MapPixel[,] map)
        {
            var surfaceRenderer = DisplaySurface.GetComponent<MeshRenderer>();
            var newTexture = new Texture2D(TextureWidth, TextureHeight);

            for (var x = 0; x < TextureWidth; x++)
            {
                for (var y = 0; y < TextureWidth; y++)
                {
                    newTexture.SetPixel(x, y, GetPixelColor(map[x, y]));
                }
            }
            newTexture.Apply();

            surfaceRenderer.material.mainTexture = newTexture;
        }

        private static Color GetPixelColor(MapPixel pixel)
        {
            switch (pixel)
            {
                case MapPixel.WATER:
                    return new Color(0, 0, 1);
                case MapPixel.LAND:
                    return new Color(20 / 255f, 114 / 255f, 52 / 255f);
                default:
                    return new Color(0, 0, 0);
            }
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _mapGenerator = GetMarkovMapGenerator();
                var map = _mapGenerator.GenerateMap(TextureWidth, TextureHeight);
                DisplayMap(map);
            }
        }
    }
}
