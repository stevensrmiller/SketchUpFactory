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
    }
}
