using ExLumina.SketchUp.API;
using System;

namespace ExLumina.SketchUp.Factory
{
    public partial class Model : Entities
    {
        public void CreateComponentDefinition(
            ComponentDefinition componentDefinition,
            SU.ModelRef modelRef)
        {
            SU.ComponentDefinitionRef componentDefinitionRef;

            // Did a forward reference already create a blank
            // component definition?

            if (componentDefinitionsLib.ContainsKey(
                    componentDefinition.name))
            {
                componentDefinitionRef =
                    componentDefinitionsLib[componentDefinition.name]
                        .componentDefinitionRef;

                componentDefinitionsLib[componentDefinition.name] =
                    componentDefinition;
            }
            else
            {
                componentDefinitionRef = new SU.ComponentDefinitionRef();
                SU.ComponentDefinitionCreate(componentDefinitionRef);

                componentDefinitionsLib.Add(
                    componentDefinition.name,
                    componentDefinition);
            }

            componentDefinition.componentDefinitionRef = componentDefinitionRef;

            // The SketchUp API appears to add a "persistent ID" to vertices,
            // edges, and faces, as they are added to definitions and groups.
            // But it also appears not to do this unless the definition or
            // group reference has already been added to the model. If you
            // add those references to the model after creating their contents,
            // SketchUp will detect the missing IDs on load and "fix" them,
            // causing SketchUp to ask if you want to save the changes when
            // you close (even though you won't think you've made any).
            //
            // You can see this in the "Edit / Undo Check Validity" menu item
            // when it happens. Undo it, and then use the "Window / Model info"
            // dialog, under "Statistics" by pressing the "Fix Problems"
            // button. You'll get a report on the missing persistent IDs.

            SU.ComponentDefinitionRef[] componentDefinitionsArray =
                new SU.ComponentDefinitionRef[1];

            componentDefinitionsArray[0] = componentDefinitionRef;

            SU.ModelAddComponentDefinitions(
                modelRef,
                1,
                componentDefinitionsArray);

            SU.ComponentDefinitionSetName(
                componentDefinitionRef,
                componentDefinition.name);

            SU.ComponentDefinitionSetDescription(
                componentDefinitionRef,
                componentDefinition.description);

            SU.EntitiesRef entitiesRef = new SU.EntitiesRef();
            SU.ComponentDefinitionGetEntities(
                componentDefinitionRef,
                entitiesRef);

            // Create the definition's geometry.

            foreach (Geometry geometry in componentDefinition.geometries)
            {
                CreateGeometry(geometry, entitiesRef);
            }

            // Create the defintion's instances.

            foreach (ComponentInstance componentInstance
                in componentDefinition.componentInstances)
            {
                CreateComponentInstance(componentInstance, entitiesRef);
            }

            // Create the definition's groups.

            foreach (Group group in componentDefinition.groups)
            {
                CreateGroup(group, entitiesRef);
            }
        }
    }
}
