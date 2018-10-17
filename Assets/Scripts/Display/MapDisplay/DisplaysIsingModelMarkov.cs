using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Display.MapToTexture;
using Assets.Scripts.Generation.MapGeneration;

namespace Assets.Scripts.Display.MapDisplay
{
    public class DisplaysIsingModelMarkov : DisplaysMapScript
    {
        public IsingAlgorithm GenerationAlgorithm;

        protected override IMapGenerator CreateMapGenerator()
        {
            switch (GenerationAlgorithm)
            {
                case IsingAlgorithm.Standard:
                    return new IsingModelMarkovMapGenerator();
                default:
                    return null;
            }
        }

        protected override IMapToTextureTranslator CreateMapToTextureTranslator()
        {
            return new BinaryMapToTextureTranslator();
        }
    }

    public enum IsingAlgorithm
    {
        Standard
    }
}
