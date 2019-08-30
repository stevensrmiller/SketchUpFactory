using ExLumina.SketchUp.API;
using System.Collections.Generic;

namespace ExLumina.SketchUp.Factory
{
    public partial class Face
    {
        public Loop outerLoop;
        public IList<Loop> innerLoops;

        /// <summary>
        /// null means no Material set for this Face.
        /// </summary>
        public string materialName;

        public Face(IHasEntities owner = null)
        {
            outerLoop = new Loop();
            innerLoops = new List<Loop>();
            owner?.Entities.Add(this);
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
        /// <param name="suFaceRef"></param>
        public Face(SU.FaceRef suFaceRef) : this()
        {
            // Get its UVHelper for texture-mapping coordinates.

            UVHelper uvh = new UVHelper(suFaceRef);

            // Get the outer edge descriptions.

            RayList rayList = new RayList(suFaceRef);

            uvh.Assign(rayList);

            outerLoop = new Loop(rayList.rays);

            SU.MaterialRef suMaterialRef = new SU.MaterialRef();

            SU.FaceGetFrontMaterial(suFaceRef, suMaterialRef);

            SU.StringRef suStringRef = new SU.StringRef();
            SU.StringCreate(suStringRef);

            SU.MaterialGetNameLegacyBehavior(suMaterialRef, suStringRef);

            materialName = Convert.ToStringAndRelease(suStringRef);
        }

        public static void Pack(Model model, SU.EntitiesRef SUEntitiesRef, IList<Face> faces)
        {
            SU.GeometryInputRef geometryInputRef = new SU.GeometryInputRef();
            SU.GeometryInputCreate(geometryInputRef);

            int vertexIndex = 0;

            foreach (Face face in faces)
            {
                face.Pack(model, geometryInputRef, ref vertexIndex);
            }

            if (faces.Count > 0)
            {
                SU.EntitiesFill(SUEntitiesRef, geometryInputRef, true);
            }

            SU.GeometryInputRelease(geometryInputRef);
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
