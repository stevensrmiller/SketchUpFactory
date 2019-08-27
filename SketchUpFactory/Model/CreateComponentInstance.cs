using ExLumina.SketchUp.API;
using System;

namespace ExLumina.SketchUp.Factory
{
    public partial class Model
    {
        public void CreateComponentInstance(
            ComponentInstance componentInstance,
            SU.EntitiesRef entitiesRef)
        {
            ComponentDefinition componentDefinition;

            SU.ComponentDefinitionRef componentDefinitionRef;

            // If this is a forward reference, create a blank
            // component definition.

            if (componentDefinitionsLib.ContainsKey(
                    componentInstance.definitionName))
            {
                componentDefinition =
                    componentDefinitionsLib[componentInstance.definitionName];

                componentDefinitionRef =
                    componentDefinition.componentDefinitionRef;
            }
            else
            {
                componentDefinition =
                    new ComponentDefinition();

                componentDefinitionRef = new SU.ComponentDefinitionRef();
                SU.ComponentDefinitionCreate(componentDefinitionRef);

                componentDefinition.componentDefinitionRef = componentDefinitionRef;

                componentDefinitionsLib.Add(
                    componentInstance.definitionName,
                    componentDefinition);
            }

            SU.ComponentInstanceRef componentInstanceRef =
                new SU.ComponentInstanceRef();

            SU.ComponentDefinitionCreateInstance(
                componentDefinitionRef,
                componentInstanceRef);

            SU.ComponentInstanceSetName(
                componentInstanceRef,
                componentInstance.instanceName);

            SU.ComponentInstanceSetTransform(
                componentInstanceRef,
                componentInstance.transform.SUTransformation);

            SU.EntitiesAddInstance(
                entitiesRef,
                componentInstanceRef,
                null);
        }
    }
}
