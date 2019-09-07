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
        
        // Note that a ComponentInstance can upcast into a DrawingElement.
        // As such, it can have an instance-wide Material set for it that
        // SketchUp will use on any Faces that use the defalt Material.
        // But you cannot set the ComponentInstance's material; you must
        // upcast first.

        /// <summary>
        /// Create an instance with no rotation or translation, scale of one.
        /// </summary>
        public ComponentInstance()
        {
            instanceName = "<ComponentInstance instanceName unset>";
            definitionName = "<ComponentInstance definitionName unset>";

            transform = new Transform();
        }

        public ComponentInstance(SU.ComponentInstanceRef suInstanceRef)
        {
            // Get the transform.

            SU.Transformation suTransformation = new SU.Transformation();

            SU.ComponentInstanceGetTransform(suInstanceRef, out suTransformation);

            transform = new Transform(suTransformation);

            // Get the instance name.

            SU.StringRef suStringRef = new SU.StringRef();
            SU.StringCreate(suStringRef);

            SU.ComponentInstanceGetName(suInstanceRef, suStringRef);

            instanceName = Convert.ToStringAndRelease(suStringRef);

            // Get the definition name.

            SU.ComponentDefinitionRef suComponentDefinitionRef = new SU.ComponentDefinitionRef();

            SU.ComponentInstanceGetDefinition(suInstanceRef, suComponentDefinitionRef);

            suStringRef = new SU.StringRef();
            SU.StringCreate(suStringRef);

            SU.ComponentDefinitionGetName(suComponentDefinitionRef, suStringRef);

            definitionName = Convert.ToStringAndRelease(suStringRef);
        }

        public void Pack(Model model, SU.EntitiesRef entitiesRef)
        {
            ComponentDefinition componentDefinition =
                model.componentDefinitions[definitionName];

            // We might be making a forward reference, so guarantee
            // that the ComponentDefinition has a SketchUp pointer.

            componentDefinition.GuaranteeReference();

            SU.ComponentDefinitionRef componentDefinitionRef =
                componentDefinition.suComponentDefinitionRef;

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
