using ExLumina.SketchUp.API;

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

        public Transform transform;

        /// <summary>
        /// Create an instance with no rotation or translation, scale of one.
        /// </summary>
        public ComponentInstance()
        {
            instanceName = "<ComponentInstance instanceName unset>";
            definitionName = "<ComponentInstance definitionName unset>";

            transform = new Transform();
        }

        public void SULoad(Model model, SU.EntitiesRef entitiesRef)
        {
            ComponentDefinition componentDefinition = 
                model.componentDefinitions[definitionName];

            SU.ComponentDefinitionRef componentDefinitionRef =
                componentDefinition.SUComponentDefinitionRef;

            SU.ComponentInstanceRef componentInstanceRef =
                new SU.ComponentInstanceRef();

            SU.ComponentDefinitionCreateInstance(
                componentDefinitionRef,
                componentInstanceRef);

            SU.EntitiesAddInstance(
                entitiesRef,
                componentInstanceRef,
                null);

            SU.ComponentInstanceSetName(
                componentInstanceRef,
                instanceName);

            SU.ComponentInstanceSetTransform(
                componentInstanceRef,
                transform.SUTransformation);
        }
    }
}
