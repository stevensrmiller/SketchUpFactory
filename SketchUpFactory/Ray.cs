namespace ExLumina.SketchUp.Factory
{
    /// <summary>
    /// Defines an edge in a Loop.
    /// </summary>
    public class Ray
    {
        /// <summary>
        /// Starting point for the edge.
        /// </summary>
        public Vector3 vertex;

        /// <summary>
        /// Marks the edge for soft (curved) rendering.
        /// </summary>
        public bool softEdge;

        /// <summary>
        /// Index into a Material Texture, or null.
        /// </summary>
        public Vector2 uvCoords;

        public Ray()
        {
            vertex = new Vector3();
        }

        public Ray(double x, double y, double z) : this()
        {
            vertex.x = x;
            vertex.y = y;
            vertex.z = z;
        }

        public Ray(double x, double y, double z, bool softEdge) : this(x, y, z)
        {
            this.softEdge = softEdge;
        }

        public Ray(
            double x,
            double y,
            double z,
            bool softEdge,
            double u,
            double v) : this(x, y, z, softEdge)
        {
            uvCoords = new Vector2(u, v);
        }

        /// <summary>
        /// Construct a Ray from a point, making a defensive copy.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public Ray(Vector3 vector) : this(vector.x, vector.y, vector.z)
        {

        }

        public Ray Clone()
        {
            Ray ray = new Ray(vertex.x, vertex.y, vertex.z);

            ray.softEdge = softEdge;

            if (uvCoords != null)
            {
                ray.uvCoords = new Vector2(uvCoords.x, uvCoords.y);
            }

            return ray;
        }
    }
}
