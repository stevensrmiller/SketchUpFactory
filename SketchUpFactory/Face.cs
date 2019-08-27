using ExLumina.SketchUp.API;
using System.Collections.Generic;

namespace ExLumina.SketchUp.Factory
{
    public class Face
    {
        public Loop outerLoop;
        public IList<Loop> innerLoops;

        /// <summary>
        /// null means no Material set for this Face.
        /// </summary>
        public string materialName;

        public Face()
        {
            outerLoop = new Loop();
            innerLoops = new List<Loop>();
        }

        public Face(params Vector3[] points) : this()
        {
            AddPoints(points);
        }

        public Face(IList<Vector3> points) : this()
        {
            AddPoints(points);
        }

        public Face(params Ray[] rays) : this()
        {
            AddRays(rays);
        }

        public Face(IList<Ray> rays) : this()
        {
            AddRays(rays);
        }

        /// <summary>
        /// Construct a Face equivalent to a SketchUp face.
        /// </summary>
        /// <param name="faceRef"></param>
        public Face(SU.FaceRef faceRef) : this()
        {
            // Get its UVHelper for texture-mapping coordinates.

            UVHelper uvh = new UVHelper(faceRef);

            // Get the outer edge descriptions.

            RayList rayList = new RayList(faceRef);

            uvh.Assign(rayList);

            outerLoop = new Loop(rayList.rays);
        }

        void AddPoints(IEnumerable<Vector3> points)
        {
            foreach (Vector3 point in points)
            {
                outerLoop.rays.Add(new Ray(point));
            }
        }

        void AddRays(IEnumerable<Ray> rays)
        {
            foreach (Ray ray in rays)
            {
                outerLoop.rays.Add(ray.Clone());
            }
        }
    }
}
