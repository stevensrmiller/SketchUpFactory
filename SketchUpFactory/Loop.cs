using System.Collections.Generic;

namespace ExLumina.SketchUp.Factory
{
    /// <summary>
    /// Boundary for a Face, either outer or inner.
    /// </summary>
    public class Loop
    {
        /// <summary>
        /// List of Rays defining the loop. Last implicitly connects to first.
        /// </summary>
        public IList<Ray> rays;

        /// <summary>
        /// Create a Loop with a zero-length list of Rays.
        /// </summary>
        public Loop()
        {
            rays = new List<Ray>();
        }

        public Loop(IList<Ray> rays)
        {
            this.rays = rays;
        }

        public Loop(RayList rayList)
        {
            this.rays = rayList.Rays;
        }
    }
}
