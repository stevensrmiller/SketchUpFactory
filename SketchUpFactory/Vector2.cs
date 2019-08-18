namespace ExLumina.SketchUp.Factory
{
    /// <summary>
    /// Store a planar value. Use for any two doubles mapped to axes.
    /// </summary>
    public class Vector2
    {
        public double x;
        public double y;

        /// <summary>
        /// Create a point in a plane with initial values.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Vector2(
            double x,
            double y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Create a point in a plane at the origin.
        /// </summary>
        public Vector2() : this(0, 0)
        {

        }
    }
}
