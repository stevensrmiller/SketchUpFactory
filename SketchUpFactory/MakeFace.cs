using System.Collections.Generic;

namespace ExLumina.SketchUp.Factory
{
    public static class MakeFace
    {
        public static Face From(params Vector3[] points)
        {
            Face face = new Face();

            foreach(Vector3 point in points)
            {
                face.outerLoop.rays.Add(new Ray(point));
            }

            return face;
        }

        public static Face From(IList<Vector3> points)
        {
            Face face = new Face();

            foreach (Vector3 point in points)
            {
                face.outerLoop.rays.Add(new Ray(point));
            }

            return face;
        }

        public static Face From(params Ray[] rays)
        {
            Face face = new Face();

            foreach(Ray ray in rays)
            {
                face.outerLoop.rays.Add(ray.Clone());
            }

            return face;
        }

        public static Face From(IList<Ray> rays)
        {
            Face face = new Face();

            foreach (Ray ray in rays)
            {
                face.outerLoop.rays.Add(ray.Clone());
            }

            return face;
        }
    }
}
