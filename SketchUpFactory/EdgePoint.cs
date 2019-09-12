using ExLumina.SketchUp.API;

namespace ExLumina.SketchUp.Factory
{
    /// <summary>
    /// Defines an edge in a Loop.
    /// </summary>
    public class EdgePoint
    {
        /// <summary>
        /// Starting point for the edge.
        /// </summary>
        public Point3 Vertex { get; set; }

        /// <summary>
        /// Marks the edge for smooth (curved) rendering.
        /// </summary>
        public bool IsSmooth { get; set; }

        /// <summary>
        /// Index into a Material Texture, or null.
        /// </summary>
        public Point2 UVCoords { get; set; }

        public EdgePoint()
        {
            Vertex = new Point3();
        }

        public EdgePoint(double x, double y, double z) : this()
        {
            Vertex.X = x;
            Vertex.Y = y;
            Vertex.Z = z;
        }

        public EdgePoint(double x, double y, double z, bool isSmooth) : this(x, y, z)
        {
            this.IsSmooth = isSmooth;
        }

        public EdgePoint(
            double x,
            double y,
            double z,
            bool softEdge,
            double u,
            double v) : this(x, y, z, softEdge)
        {
            UVCoords = new Point2(u, v);
        }

        /// <summary>
        /// Construct a Ray from a point, making a defensive copy.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public EdgePoint(Point3 vector) : this(vector.X, vector.Y, vector.Z)
        {

        }

        internal EdgePoint(SU.EdgeRef edgeRef, SU.VertexRef vertexRef)
        {
            SU.EdgeGetSmooth(edgeRef, out bool isSmooth);

            IsSmooth = IsSmooth;

            SU.Point3D point = new SU.Point3D();

            SU.VertexGetPosition(vertexRef, ref point);

            Vertex = new Point3(point);
        }

        public EdgePoint Clone()
        {
            EdgePoint ray = new EdgePoint(Vertex.X, Vertex.Y, Vertex.Z);

            ray.IsSmooth = IsSmooth;

            if (UVCoords != null)
            {
                ray.UVCoords = new Point2(UVCoords.X, UVCoords.Y);
            }

            return ray;
        }
    }
}
