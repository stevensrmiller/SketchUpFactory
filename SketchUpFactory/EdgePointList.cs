using ExLumina.SketchUp.API;
using System.Collections.Generic;

namespace ExLumina.SketchUp.Factory
{
    internal class EdgePointList
    {
        IList<EdgePoint> edgePoints;

        public IList<EdgePoint> EdgePoints { get => edgePoints; }

        /// <summary>
        /// Extract the EdgePoints for the outer loop.
        /// </summary>
        /// <param name="faceRef"></param>
        public EdgePointList(SU.FaceRef faceRef)
        {
            SU.LoopRef loopRef = new SU.LoopRef();

            SU.FaceGetOuterLoop(faceRef, loopRef);

            EdgePointsFromLoop(loopRef);
        }

        public EdgePointList(SU.LoopRef loopRef)
        {
            EdgePointsFromLoop(loopRef);
        }

        void EdgePointsFromLoop(SU.LoopRef loopRef)
        {
            long count;

            SU.LoopGetNumVertices(loopRef, out count);

            long len = count;

            SU.EdgeRef[] edgeRefs = new SU.EdgeRef[len];

            SU.LoopGetEdges(loopRef, len, edgeRefs, out count);

            SU.VertexRef[] vertexRefs = new SU.VertexRef[len];

            SU.LoopGetVertices(loopRef, len, vertexRefs, out count);

            edgePoints = new List<EdgePoint>();

            // The dread pirate Parallel Arrays.

            for (int i = 0; i < len; ++i)
            {
                edgePoints.Add(new EdgePoint(edgeRefs[i], vertexRefs[i]));
            }
        }
    }
}
