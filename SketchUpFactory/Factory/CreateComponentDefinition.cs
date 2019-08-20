using ExLumina.SketchUp.API;
using System;

namespace ExLumina.SketchUp.Factory
{
    public static partial class Factory
    {
        static void CreateComponentDefinition(
            ComponentDefinition componentDefinition,
            SU.ModelRef modelRef)
        {
            SU.ComponentDefinitionRef componentDefinitionRef;

            // Did a forward reference already create a blank
            // component definition?

            if (componentDefinitionsLib.ContainsKey(
                    componentDefinition.definitionName))
            {
                componentDefinitionRef =
                    componentDefinitionsLib[componentDefinition.definitionName]
                        .componentDefinitionRef;

                componentDefinitionsLib[componentDefinition.definitionName] =
                    componentDefinition;
            }
            else
            {
                componentDefinitionRef = new SU.ComponentDefinitionRef();
                SU.ComponentDefinitionCreate(componentDefinitionRef);

                componentDefinitionsLib.Add(
                    componentDefinition.definitionName,
                    componentDefinition);
            }

            componentDefinition.componentDefinitionRef = componentDefinitionRef;

            // SketchUp thinks the model is dirty if a Component has its
            // Entities filled before it is added to the model, so we
            // add it now and do all the actual defining after that.

            SU.ComponentDefinitionRef[] componentDefinitionsArray =
                new SU.ComponentDefinitionRef[1];

            componentDefinitionsArray[0] = componentDefinitionRef;

            SU.ModelAddComponentDefinitions(
                modelRef,
                1,
                componentDefinitionsArray);

            SU.ComponentDefinitionSetName(
                componentDefinitionRef,
                componentDefinition.definitionName);

            SU.ComponentDefinitionSetDescription(
                componentDefinitionRef,
                componentDefinition.description);

            SU.EntitiesRef entitiesRef = new SU.EntitiesRef();
            SU.ComponentDefinitionGetEntities(
                componentDefinitionRef,
                entitiesRef);

            foreach (Geometry geometry in componentDefinition.geometries)
            {
                CreateGeometry(geometry, entitiesRef);
            }

            // Create instances.

            foreach (ComponentInstance componentInstance
                in componentDefinition.componentInstances)
            {
                CreateComponentInstance(componentInstance, entitiesRef);
            }
        }
    }
}
