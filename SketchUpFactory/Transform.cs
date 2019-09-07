using System;
using ExLumina.SketchUp.API;

namespace ExLumina.SketchUp.Factory
{
    public class Transform
    {
        /// <summary>
        /// Scale in world or parent coordinates
        /// </summary>
        /// <remarks>
        /// For Unity, x is to the right, y is up, and z is away.
        /// For SketchUp, x is to the right, y is away, and z is up.
        /// </remarks>
        public Vector3 scale;

        /// <summary>
        /// Rotation individually in world or parent coordinates.
        /// </summary>
        /// <remarks>
        /// For Unity, x is to the right, y is up, and z is away.
        /// For SketchUp, x is to the right, y is away, and z is up.
        /// </remarks>
        public Vector3 rotation;

        /// <summary>
        /// Translation along world or parent coordinates.
        /// </summary>
        /// <remarks>
        /// For Unity, x is to the right, y is up, and z is away.
        /// For SketchUp, x is to the right, y is away, and z is up.
        /// </remarks>
        public Vector3 translation;

        public Transform()
        {
            scale = new Vector3(1, 1, 1);
            rotation = new Vector3(0, 0, 0);
            translation = new Vector3(0, 0, 0);
        }

        public Transform(
            Vector3 scale,
            Vector3 rotation,
            Vector3 translation)
        {
            this.scale       = scale.Clone();
            this.rotation    = rotation.Clone();
            this.translation = translation.Clone();
        }

        /// <summary>
        /// Use an existing SketchUp transformation to initialize this Transform.
        /// </summary>
        /// <param name="t"></param>
        public Transform(SU.Transformation t) :this()
        {
            // Translation is a literal copy of column 3.

            translation.x = t[0, 3];
            translation.y = t[1, 3];
            translation.z = t[2, 3];

            // Squared scales are sums of squares of left three columns.

            scale.x = Math.Sqrt(
                t[0, 0] * t[0, 0] +
                t[1, 0] * t[1, 0] +
                t[2, 0] * t[2, 0]);

            scale.y = Math.Sqrt(
                t[0, 1] * t[0, 1] +
                t[1, 1] * t[1, 1] +
                t[2, 1] * t[2, 1]);

            scale.z = Math.Sqrt(
                t[0, 2] * t[0, 2] +
                t[1, 2] * t[1, 2] +
                t[2, 2] * t[2, 2]);

            // Extract and normalize the rotation matrix.

            double[,] r = new double[3, 3];

            for (int row = 0; row < 3; ++row)
            {
                r[row, 0] = t[row, 0] / scale.x;
                r[row, 1] = t[row, 1] / scale.y;
                r[row, 2] = t[row, 2] / scale.z;
            }

            // Negate the scales and the matrix if its determinant is negative.

            if (Determinant(r) < 0)
            {
                scale.x = -scale.x;
                scale.y = -scale.y;
                scale.z = -scale.z;

                for (int row = 0; row < 3; ++row)
                {
                    for (int col = 0; col < 3; ++col)
                    {
                        r[row, col] = -r[row, col];
                    }
                }
            }

            // Use inverse trig to get the angles.

            rotation.y = Math.Asin(-r[2, 0]) * 180 / Math.PI;

            // Edge case where y rotation is 90 degrees.

            if (1 - r[2, 0] * r[2, 0] == 0)
            {
                rotation.x = Math.Atan2(-r[1, 2], r[1, 1]) * 180 / Math.PI;
                rotation.z = 0;
            }
            else
            {
                rotation.x = Math.Atan2(r[2, 1], r[2, 2]) * 180 / Math.PI;
                rotation.z = Math.Atan2(r[1, 0], r[0, 0]) * 180 / Math.PI;
            }
        }

        // TODO: make this internal
        public SU.Transformation SUTransformation
        {
            get
            {
                // Note that we use premultiply the running matrix with each
                // successive transformation, so each will be in world space,
                // not body space.

                // Apply all three scale transformations. => S

                SU.Transformation transformationS = new SU.Transformation();

                SU.TransformationNonUniformScale(
                    ref transformationS,
                    scale.x,
                    scale.y,
                    scale.z);

                // Apply rotation in the YZ plane (around X/red axis). X * S => SX

                SU.Transformation transformationX = new SU.Transformation();

                SU.TransformationRotation(
                    ref transformationX,
                    new SU.Point3D(0, 0, 0),
                    new SU.Vector3D(1, 0, 0),
                    rotation.x * Math.PI / 180);

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
                    rotation.y * Math.PI / 180);

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
                    rotation.z * Math.PI / 180);

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
                        translation.x,
                        translation.y,
                        translation.z));

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
