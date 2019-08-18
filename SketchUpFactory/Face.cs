using System.Collections.Generic;

namespace ExLumina.SketchUp.Factory
{
    public class Face
    {
        public Loop outerLoop;
        public IList<Loop> innerLoops;

        /// <summary>
        /// null means no Material set for this Face.
        /// </summary>
        public string materialName;

        public Face()
        {
            outerLoop = new Loop();
            innerLoops = new List<Loop>();
        }
    }
}
