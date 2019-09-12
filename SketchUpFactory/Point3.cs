using System;
using ExLumina.SketchUp.API;

namespace ExLumina.SketchUp.Factory
{
    /// <summary>
    /// Store a spatial value. Use for any three doubles mapped to axes.
    /// </summary>
    public class Point3
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
        /// Z coordinate.
        /// </summary>
        public double Z { get; set; }

        internal SU.Point3D SUPoint3D
        {
            get => new SU.Point3D { x = X, y = Y, z = Z };
        }

        /// <summary>
        /// Create a point in space with initial values.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Point3(
            double x,
            double y,
            double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary>
        /// Create a point in space at the origin.
        /// </summary>
        public Point3() : this(0, 0, 0)
        {

        }

        /// <summary>
        /// Create a Vector3 from a SketchUp point.
        /// </summary>
        /// <param name="p"></param>
        public Point3(SU.Point3D p) : this (p.x, p.y, p.z)
        {

        }

        /// <summary>
        /// Creates a new Vector3 object identical to itself.
        /// </summary>
        /// <returns></returns>
        public Point3 Clone()
        {
            return new Point3(X, Y, Z);
        }

        /// <summary>
        /// Formats the values into a string, with labels.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("Point3 = x:{0} y:{1} z:{2}", X, Y, Z);
        }
    }
}
