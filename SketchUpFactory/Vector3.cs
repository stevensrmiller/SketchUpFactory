using System;
using ExLumina.SketchUp.API;

namespace ExLumina.SketchUp.Factory
{
    /// <summary>
    /// Store a spatial value. Use for any three doubles mapped to axes.
    /// </summary>
    public class Vector3
    {
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

        public override string ToString()
        {
            return String.Format("Vector3 = x:{0} y:{1} z:{2}", x, y, z);
        }
    }
}
