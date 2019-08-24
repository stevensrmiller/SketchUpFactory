using System;
using ExLumina.SketchUp.API;

namespace ExLumina.SketchUp.Factory
{
    /// <summary>
    /// Store a spatial value. Use for any three doubles mapped to axes.
    /// </summary>
    public class Vector3
    {
        /// <summary>
        /// Equivalent to new Vector3(0, 0, 0).
        /// </summary>
        public static readonly Vector3 Zero = new Vector3(0, 0, 0);

        /// <summary>
        /// Equivalent to new Vector3(1, 1, 1).
        /// </summary>
        /// <remarks>
        /// Note that this Vector3's lenght is the square root of three
        /// (about 1.732), not one.
        /// </remarks>
        public static readonly Vector3 One = new Vector3(1, 1, 1);

        public double x;
        public double y;
        public double z;

        internal SU.Point3D SUPoint3D
        {
            get => new SU.Point3D { x = x, y = y, z = z };
        }

        /// <summary>
        /// Create a point in space with initial values.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Vector3(
            double x,
            double y,
            double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// Create a point in space at the origin.
        /// </summary>
        public Vector3() : this(0, 0, 0)
        {

        }

        /// <summary>
        /// Creates a new Vector3 object identical to itself.
        /// </summary>
        /// <returns></returns>
        public Vector3 Clone()
        {
            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Formats the values into a string, with labels.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("Vector3 = x:{0} y:{1} z:{2}", x, y, z);
        }
    }
}
