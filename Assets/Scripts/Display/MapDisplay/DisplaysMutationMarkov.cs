using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Display.MapToTexture;
using Assets.Scripts.Generation.MapGeneration;

namespace Assets.Scripts.Display.MapDisplay
{
    public class DisplaysMutationMarkov : DisplaysMapScript
    {
        public MutationAlgorithm GenerationAlgorithm;

        protected override IMapGenerator CreateMapGenerator()
        {
            switch (GenerationAlgorithm)
            {
                case MutationAlgorithm.Basic:
                    return new MutationMarkovMapGenerator();
                default:
                    return null;
            }
        }

        protected override IMapToTextureTranslator CreateMapToTextureTranslator()
        {
            return new BinaryMapToTextureTranslator();
        }
    }

    public enum MutationAlgorithm
    {
        Basic
    }
}
