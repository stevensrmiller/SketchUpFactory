using ExLumina.SketchUp.API;
using System;

namespace ExLumina.SketchUp.Factory
{
    public static partial class Factory
    {
        static void CreateComponentInstance(
            Model model,
            ComponentInstance componentInstance,
            SU.EntitiesRef entitiesRef)
        {
            ComponentDefinition componentDefinition;

            SU.ComponentDefinitionRef componentDefinitionRef;

            // If this is a forward reference, create a blank
            // component definition.

            if (model.componentDefinitionsLib.ContainsKey(
                    componentInstance.definitionName))
            {
                componentDefinition =
                    model.componentDefinitionsLib[componentInstance.definitionName];

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

                model.componentDefinitionsLib.Add(
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
