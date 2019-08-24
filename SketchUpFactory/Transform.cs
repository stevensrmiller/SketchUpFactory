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

        internal SU.Transformation SUTransformation
        {
            get
            {
                SU.Transformation transformationS = new SU.Transformation();

                SU.TransformationNonUniformScale(
                    ref transformationS,
                    scale.x,
                    scale.y,
                    scale.z);

                SU.Transformation transformationRz = new SU.Transformation();

                SU.TransformationRotation(
                    ref transformationRz,
                    new SU.Point3D(0, 0, 0),
                    new SU.Vector3D(0, 0, 1),
                    rotation.z * Math.PI / 180);

                SU.Transformation transformationM0 = new SU.Transformation();

                SU.TransformationMultiply(
                    ref transformationRz,
                    ref transformationS,
                    ref transformationM0);

                SU.Transformation transformationRx = new SU.Transformation();

                SU.TransformationRotation(
                    ref transformationRx,
                    new SU.Point3D(0, 0, 0),
                    new SU.Vector3D(1, 0, 0),
                    rotation.x * Math.PI / 180);

                SU.Transformation transformationM1 = new SU.Transformation();

                SU.TransformationMultiply(
                    ref transformationRx,
                    ref transformationM0,
                    ref transformationM1);

                SU.Transformation transformationRy = new SU.Transformation();

                SU.TransformationRotation(
                    ref transformationRy,
                    new SU.Point3D(0, 0, 0),
                    new SU.Vector3D(0, 1, 0),
                    rotation.y * Math.PI / 180);

                SU.Transformation transformationM2 = new SU.Transformation();

                SU.TransformationMultiply(
                    ref transformationRy,
                    ref transformationM1,
                    ref transformationM2);

                SU.Transformation transformationT = new SU.Transformation();

                SU.TransformationTranslation(
                    ref transformationT,
                    new SU.Vector3D(
                        translation.x,
                        translation.y,
                        translation.z));

                SU.Transformation transformationM3 = new SU.Transformation();

                SU.TransformationMultiply(
                    ref transformationT,
                    ref transformationM2,
                    ref transformationM3);

                return transformationM3;
            }
        }
    }
}
