namespace ExLumina.SketchUp.Factory
{
    /// <summary>
    /// Instance created from a ComponentDefinition.
    /// </summary>
    public class ComponentInstance
    {
        /// <summary>
        /// The name of the definition use by this instance.
        /// </summary>
        public string definitionName;

        /// <summary>
        /// The instance name to appear in the hierarchy.
        /// </summary>
        public string instanceName;

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

        /// <summary>
        /// Create an instance with no rotation or translation, scale of one.
        /// </summary>
        public ComponentInstance()
        {
            instanceName = "<ComponentInstance instanceName unset>";
            definitionName = "<ComponentInstance definitionName unset>";

            scale = new Vector3(1, 1, 1);
            rotation = new Vector3();
            translation = new Vector3();
        }
    }
}
