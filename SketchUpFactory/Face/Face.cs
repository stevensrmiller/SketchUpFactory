using ExLumina.SketchUp.API;
using System.Collections.Generic;

namespace ExLumina.SketchUp.Factory
{
    /// <summary>
    /// A polygon with one outer loop, zero to many inner loops.
    /// </summary>
    public partial class Face
    {
        /// <summary>
        /// Name of the material to use on this face.
        /// </summary>
        /// <remarks>
        /// null means no Material set for this Face (use the default material).
        /// </remarks>
        public string MaterialName { get; set; }

        //Loop outerLoop = new Loop();
        //IList<Loop> innerLoops = new List<Loop>();

        IList<EdgePoint> outerLoop = new List<EdgePoint>();
        IList<IList<EdgePoint>> innerLoops = new List<IList<EdgePoint>>();

        /// <summary>
        /// Construct a Face from an array of Point3 objects.
        /// </summary>
        /// <param name="points"></param>
        /// <param name="materialName">Optional.</param>
        public Face(Point3[] points, string materialName = null)
        {
            AddPoints(points);
            MaterialName = materialName;
        }

        /// <summary>
        /// Construct a Face from a list of Point3 objects.
        /// </summary>
        /// <param name="points"></param>
        /// <param name="materialName">Optional.</param>
        public Face(IList<Point3> points, string materialName = null)
        {
            AddPoints(points);
            MaterialName = materialName;
        }

        /// <summary>
        /// Construct a Face from an array of EdgePoints.
        /// </summary>
        /// <param name="edgePoints"></param>
        /// <param name="materialName">Optional.</param>
        public Face(EdgePoint[] edgePoints, string materialName = null)
        {
            AddEdgePoints(edgePoints);
            MaterialName = materialName;
        }

        /// <summary>
        /// Construct a Face from a list of EdgePoints.
        /// </summary>
        /// <param name="edgePoints"></param>
        /// <param name="materialName">Optional.</param>
        public Face(IList<EdgePoint> edgePoints, string materialName = null)
        {
            AddEdgePoints(edgePoints);
            MaterialName = materialName;
        }

        internal Face(SU.FaceRef suFaceRef)
        {
            // Get its UVHelper for texture-mapping coordinates.

            UVHelper uvh = new UVHelper(suFaceRef);

            // Get the outer loop.

            EdgePointList edgePointList = new EdgePointList(suFaceRef);

            uvh.Assign(edgePointList);

            outerLoop = new Loop(edgePointList).edgePoints;

            // Get any inner loops.

            SU.FaceGetNumInnerLoops(suFaceRef, out long count);

            SU.LoopRef[] loopRefs = new SU.LoopRef[count];

            long len = count;

            SU.FaceGetInnerLoops(suFaceRef, len, loopRefs, out count);

            foreach (SU.LoopRef loopRef in loopRefs)
            {
                innerLoops.Add(
                    new Loop(new EdgePointList(loopRef)).edgePoints);
            }

            SU.MaterialRef suMaterialRef = new SU.MaterialRef();

            try
            {
                SU.FaceGetFrontMaterial(suFaceRef, suMaterialRef);

                SU.StringRef suStringRef = new SU.StringRef();
                SU.StringCreate(suStringRef);

                SU.MaterialGetNameLegacyBehavior(suMaterialRef, suStringRef);

                MaterialName = Convert.ToStringAndRelease(suStringRef);
            }
            catch (SketchUpException e)
            {
                if (e.ErrorCode == SU.ErrorNoData)
                {
                    // Not an error. It just has no material.
                }
                else
                {
                    throw;
                }
            }
        }

        internal static void Pack(Model model, SU.EntitiesRef entitiesRef, IList<Face> faces)
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
                SU.EntitiesFill(entitiesRef, geometryInputRef, true);
            }

            SU.GeometryInputRelease(geometryInputRef);
        }

        void AddPoints(IEnumerable<Point3> points)
        {
            foreach (Point3 point in points)
            {
                outerLoop.Add(new EdgePoint(point));
            }
        }

        void AddEdgePoints(IEnumerable<EdgePoint> edgePoints)
        {
            foreach (EdgePoint edgePoint in edgePoints)
            {
                outerLoop.Add(edgePoint.Clone());
            }
        }
    }
}
