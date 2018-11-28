using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Display.MapToTexture;
using Assets.Scripts.Generation.MapGeneration;
using Assets.Scripts.Shared;
using Assets.Scripts.Shared.Map;
using UnityEngine;

namespace Assets.Scripts.Display.MapDisplay
{
    public abstract class DisplaysMapScript : MonoBehaviour
    {
        public GameObject DisplaySurface;
        public int MapHeight;
        public int MapWidth;

        private void Start()
        {
            GenerateNewMap();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                GenerateNewMap();
            }
        }

        private void GenerateNewMap()
        {
            var mapGenerator = CreateMapGenerator();
            var map = mapGenerator.GenerateMap(MapHeight, MapWidth);
            DisplayMap(map);
        }

        private void DisplayMap(ITwoDimensionalMap twoDimensionalMap)
        {
            var mapToTextureTranslator = CreateMapToTextureTranslator();
            DisplaySurface.GetComponent<MeshRenderer>().material.mainTexture =
                mapToTextureTranslator.TranslateMapToTexture(twoDimensionalMap);
        }

        protected abstract IMapGenerator CreateMapGenerator();

        protected abstract IMapToTextureTranslator CreateMapToTextureTranslator();
    }
}
