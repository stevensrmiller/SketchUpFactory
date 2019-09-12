namespace ExLumina.SketchUp.Factory
{
    /// <summary>
    /// Store a planar value. Use for any two doubles mapped to axes.
    /// </summary>
    public class Point2
    {
        /// <summary>
        /// X coordinate.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Y coordinate.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Create a point in a plane with initial values.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Point2(
            double x,
            double y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Create a point in a plane at the origin.
        /// </summary>
        public Point2() : this(0, 0)
        {

        }
    }
}
