using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExLumina.SketchUp.API;

namespace ExLumina.SketchUp.Factory
{
    public class RayList
    {
        IList<Ray> rays;

        public IList<Ray> Rays { get => rays; }

        /// <summary>
        /// Extract the Rays for the outer loop.
        /// </summary>
        /// <param name="faceRef"></param>
        public RayList(SU.FaceRef faceRef)
        {
            SU.LoopRef loopRef = new SU.LoopRef();

            SU.FaceGetOuterLoop(faceRef, loopRef);

            RaysFromLoop(loopRef);
        }

        public RayList(SU.LoopRef loopRef)
        {
            RaysFromLoop(loopRef);
        }

        void RaysFromLoop(SU.LoopRef loopRef)
        {
            long count;

            SU.LoopGetNumVertices(loopRef, out count);

            long len = count;

            SU.EdgeRef[] edgeRefs = new SU.EdgeRef[len];

            SU.LoopGetEdges(loopRef, len, edgeRefs, out count);

            SU.VertexRef[] vertexRefs = new SU.VertexRef[len];

            SU.LoopGetVertices(loopRef, len, vertexRefs, out count);

            rays = new List<Ray>();

            // The dread pirate Parallel Arrays.

            for (int i = 0; i < len; ++i)
            {
                rays.Add(new Ray(edgeRefs[i], vertexRefs[i]));
            }
        }
    }
}
