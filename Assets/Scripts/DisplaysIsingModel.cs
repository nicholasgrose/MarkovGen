using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.ProceduralGeneration;
using Assets.Scripts.ProceduralGeneration.MarkovMapGenerator.IsingModelMarkov;
using UnityEngine;

namespace Assets.Scripts
{
    public class DisplaysIsingModel : MonoBehaviour
    {
        [Tooltip("The resolution of the texture on the x-axis")]
        public int TextureWidth;
        [Tooltip("The resolution of the texture on the y-axis")]
        public int TextureHeight;
        [Tooltip("Selects the specific algorithm being used to generate the map")]
        public IsingModelMarkovGenAlgorithm GenerationAlgorithm;
        [Tooltip("The object the generated texture is displayed on")]
        public GameObject DisplaySurface;

        public int Iterations;
        public int Temperature;

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
                case IsingModelMarkovGenAlgorithm.StandardIsingModel:
                    return new IsingModelMarkovMapGenerator(Iterations, Temperature);
                default:
                    return null;
            }
        }

        private void DisplayMap(MapPixel[,] map)
        {
            var surfaceRenderer = DisplaySurface.GetComponent<MeshRenderer>();
            var newTexture = new Texture2D(TextureWidth, TextureHeight);
            newTexture.filterMode = FilterMode.Point;

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
