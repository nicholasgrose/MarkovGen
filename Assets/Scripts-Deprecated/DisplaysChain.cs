using Assets.ProceduralGeneration;
using Assets.ProceduralGeneration.MarkovMapGenerator.PureMarkov;
using UnityEngine;
using PureMarkovGenAlgorithm = Assets.ProceduralGeneration.MarkovMapGenerator.PureMarkov.PureMarkovGenAlgorithm;

namespace Assets
{
    public class DisplaysChain : MonoBehaviour
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
        [Tooltip("Selects the specific algorithm being used to generate the map")]
        public PureMarkovGenAlgorithm GenerationAlgorithm;
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
                case PureMarkovGenAlgorithm.CrossRow:
                    return new CrossRowMarkovMapGenerator(LandToWaterWeight, WaterToLandWeight);
                case PureMarkovGenAlgorithm.IndependentRow:
                    return new IndependentRowMarkovMapGenerator(LandToWaterWeight, WaterToLandWeight);
                case PureMarkovGenAlgorithm.LeftCorner:
                    return new LeftCornerMarkovMapGenerator(LandToWaterWeight, WaterToLandWeight);
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
