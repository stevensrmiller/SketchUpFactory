using System.Collections.Generic;

namespace ExLumina.SketchUp.Factory
{
    /// <summary>
    /// Boundary for a Face, either outer or inner.
    /// </summary>
    internal class Loop
    {
        /// <summary>
        /// List of Rays defining the loop. Last implicitly connects to first.
        /// </summary>
        public IList<EdgePoint> edgePoints;

        /// <summary>
        /// Create a Loop with a zero-length list of Rays.
        /// </summary>
        public Loop()
        {
            edgePoints = new List<EdgePoint>();
        }

        public Loop(IList<EdgePoint> edgePoints)
        {
            this.edgePoints = edgePoints;
        }

        public Loop(EdgePointList edgePointList)
        {
            this.edgePoints = edgePointList.EdgePoints;
        }
    }
}
