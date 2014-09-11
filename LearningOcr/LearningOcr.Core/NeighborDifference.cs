using System;
using System.CodeDom;

namespace LearningOcr.Core
{
    [Serializable]
    public class NeighborDifference
    {
        public float Up { get; set; }
        public float Down { get; set; }
        public float Left { get; set; }
        public float Right { get; set; }

        public bool HasUp { get; set; }
        public bool HasDown { get; set; }
        public bool HasLeft { get; set; }
        public bool HasRight { get; set; }

        public bool IsPartOfChar { get; set; }

        public NeighborDifference()
        {
            HasUp = true;
            HasDown = true;
            HasLeft = true;
            HasRight = true;
        }
    }
}