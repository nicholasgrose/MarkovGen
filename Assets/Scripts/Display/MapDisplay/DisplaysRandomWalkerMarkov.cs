using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Display.MapToTexture;
using Assets.Scripts.Generation.MapGeneration;

namespace Assets.Scripts.Display.MapDisplay
{
    public class DisplaysRandomWalkerMarkov : DisplaysMapScript
    {
        public RandomWalkerAlgorithm GenerationMethod;

        protected override IMapGenerator CreateMapGenerator()
        {
            switch (GenerationMethod)
            {
                case RandomWalkerAlgorithm.CrossRow:
                    return new CrossRowRandomWalkerMarkovMapGenerator();
                case RandomWalkerAlgorithm.Independent:
                    return new IndependentRandomWalkerMarkovMapGenerator();
                case RandomWalkerAlgorithm.LeftCorner:
                    return new LeftCornerRandomWalkerMarkovMapGenerator();
                default:
                    return null;
            }
        }

        protected override IMapToTextureTranslator CreateMapToTextureTranslator()
        {
            return new BinaryMapToTextureTranslator();
        }
    }

    public enum RandomWalkerAlgorithm
    {
        CrossRow,
        Independent,
        LeftCorner
    }
}
