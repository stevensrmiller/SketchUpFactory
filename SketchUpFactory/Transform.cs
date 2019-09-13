using System;
using ExLumina.SketchUp.API;

namespace ExLumina.SketchUp.Factory
{
    /// <summary>
    /// Allow scaling, rotation, and translation of Groups and CompInsts.
    /// </summary>
    public class Transform
    {
        /// <summary>
        /// Scale in world or parent coordinates
        /// </summary>
        /// <remarks>
        /// For Unity, x is to the right, y is up, and z is away.
        /// For SketchUp, x is to the right, y is away, and z is up.
        /// </remarks>
        public Point3 Scale { get; set; }

        /// <summary>
        /// Rotation individually in world or parent coordinates.
        /// </summary>
        /// <remarks>
        /// For Unity, x is to the right, y is up, and z is away.
        /// For SketchUp, x is to the right, y is away, and z is up.
        /// </remarks>
        public Point3 Rotation { get; set; }

        /// <summary>
        /// Translation along world or parent coordinates.
        /// </summary>
        /// <remarks>
        /// For Unity, x is to the right, y is up, and z is away.
        /// For SketchUp, x is to the right, y is away, and z is up.
        /// </remarks>
        public Point3 Translation { get; set; }

        /// <summary>
        /// Create an identity Transform
        /// </summary>
        public Transform()
        {
            Scale = new Point3(1, 1, 1);
            Rotation = new Point3(0, 0, 0);
            Translation = new Point3(0, 0, 0);
        }

        /// <summary>
        /// Create a new Transform with specified scale, Euler angles, and translation.
        /// </summary>
        /// <param name="scale"></param>
        /// <param name="rotation"></param>
        /// <param name="translation"></param>
        public Transform(
            Point3 scale,
            Point3 rotation,
            Point3 translation)
        {
            this.Scale       = scale.Clone();
            this.Rotation    = rotation.Clone();
            this.Translation = translation.Clone();
        }

        /// <summary>
        /// Use an existing SketchUp transformation to initialize this Transform.
        /// </summary>
        /// <param name="t"></param>
        internal Transform(SU.Transformation t) :this()
        {
            // Translation is a literal copy of column 3.

            Translation.X = t[0, 3];
            Translation.Y = t[1, 3];
            Translation.Z = t[2, 3];

            // Squared scales are sums of squares of left three columns.

            Scale.X = Math.Sqrt(
                t[0, 0] * t[0, 0] +
                t[1, 0] * t[1, 0] +
                t[2, 0] * t[2, 0]);

            Scale.Y = Math.Sqrt(
                t[0, 1] * t[0, 1] +
                t[1, 1] * t[1, 1] +
                t[2, 1] * t[2, 1]);

            Scale.Z = Math.Sqrt(
                t[0, 2] * t[0, 2] +
                t[1, 2] * t[1, 2] +
                t[2, 2] * t[2, 2]);

            // Extract and normalize the rotation matrix.

            double[,] r = new double[3, 3];

            for (int row = 0; row < 3; ++row)
            {
                r[row, 0] = t[row, 0] / Scale.X;
                r[row, 1] = t[row, 1] / Scale.Y;
                r[row, 2] = t[row, 2] / Scale.Z;
            }

            // Negate the scales and the matrix if its determinant is negative.

            if (Determinant(r) < 0)
            {
                Scale.X = -Scale.X;
                Scale.Y = -Scale.Y;
                Scale.Z = -Scale.Z;

                for (int row = 0; row < 3; ++row)
                {
                    for (int col = 0; col < 3; ++col)
                    {
                        r[row, col] = -r[row, col];
                    }
                }
            }

            // Use inverse trig to get the angles.

            Rotation.Y = Math.Asin(-r[2, 0]) * 180 / Math.PI;

            // Edge case where y rotation is 90 degrees.

            if (1 - r[2, 0] * r[2, 0] == 0)
            {
                Rotation.X = Math.Atan2(-r[1, 2], r[1, 1]) * 180 / Math.PI;
                Rotation.Z = 0;
            }
            else
            {
                Rotation.X = Math.Atan2(r[2, 1], r[2, 2]) * 180 / Math.PI;
                Rotation.Z = Math.Atan2(r[1, 0], r[0, 0]) * 180 / Math.PI;
            }
        }

        internal SU.Transformation SUTransformation
        {
            get
            {
                // Note that we must premultiply the running matrix with each
                // successive transformation, so each will be in world space,
                // not body space.

                // Apply all three scale transformations. => S

                SU.Transformation transformationS = new SU.Transformation();

                SU.TransformationNonUniformScale(
                    ref transformationS,
                    Scale.X,
                    Scale.Y,
                    Scale.Z);

                // Apply rotation in the YZ plane (around X/red axis). X * S => SX

                SU.Transformation transformationX = new SU.Transformation();

                SU.TransformationRotation(
                    ref transformationX,
                    new SU.Point3D(0, 0, 0),
                    new SU.Vector3D(1, 0, 0),
                    Rotation.X * Math.PI / 180);

                SU.Transformation transformationSX = new SU.Transformation();

                SU.TransformationMultiply(
                    ref transformationX,
                    ref transformationS,
                    ref transformationSX);

                // Apply rotation in the XZ plane (around Y/green axis). Y * SX => SXY

                SU.Transformation transformationY = new SU.Transformation();

                SU.TransformationRotation(
                    ref transformationY,
                    new SU.Point3D(0, 0, 0),
                    new SU.Vector3D(0, 1, 0),
                    Rotation.Y * Math.PI / 180);

                SU.Transformation transformationSXY = new SU.Transformation();

                SU.TransformationMultiply(
                    ref transformationY,
                    ref transformationSX,
                    ref transformationSXY);

                // Apply rotation in the XY plane (around Z/blue axis). Z * SXY => SXYZ

                SU.Transformation transformationZ = new SU.Transformation();

                SU.TransformationRotation(
                    ref transformationZ,
                    new SU.Point3D(0, 0, 0),
                    new SU.Vector3D(0, 0, 1),
                    Rotation.Z * Math.PI / 180);

                SU.Transformation transformationSXYZ = new SU.Transformation();

                SU.TransformationMultiply(
                    ref transformationZ,
                    ref transformationSXY,
                    ref transformationSXYZ);

                // Apply all three translation transformations. T * SXYZ => SXYZT

                SU.Transformation transformationT = new SU.Transformation();

                SU.TransformationTranslation(
                    ref transformationT,
                    new SU.Vector3D(
                        Translation.X,
                        Translation.Y,
                        Translation.Z));

                SU.Transformation transformationSXYZT = new SU.Transformation();

                SU.TransformationMultiply(
                    ref transformationT,
                    ref transformationSXYZ,
                    ref transformationSXYZT);

                return transformationSXYZT;
            }
        }

        double Determinant(double[,] r)
        {
            return r[0, 0] * (r[1, 1] * r[2, 2] - r[1, 2] * r[2, 1]) -
                   r[0, 1] * (r[1, 0] * r[2, 2] - r[1, 2] * r[2, 0]) +
                   r[0, 2] * (r[1, 0] * r[2, 1] - r[1, 1] * r[2, 0]);
        }
    }
}
