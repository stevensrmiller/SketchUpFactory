using ExLumina.SketchUp.API;
using System.Collections.Generic;

namespace ExLumina.SketchUp.Factory
{
    /// <summary>
    /// Defines a reusable component.
    /// </summary>
    public class CompDef : Entities
    {
        /// <summary>
        /// Name visible in SketchUp.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Descriptoin visible in SketchUp.
        /// </summary>
        public string Description { get; set; }

        // Used when packing ComponentInstances.

        internal SU.ComponentDefinitionRef componentDefinitionRef;

        /// <summary>
        /// Create an empty definition.
        /// </summary>
        /// <param name="name">Name visibile in SketchUp (optional).</param>
        /// <param name="description">Description visible in SketchUp (optional).</param>
        public CompDef(
            string name = "<component definition name unset>",
            string description = "<component definition description unset>")
        {
            Name = name;
            Description = description;
        }

        internal CompDef(
            SU.ComponentDefinitionRef componentDefinitionRef)
        {
            // Get the name.

            SU.StringRef stringRef = new SU.StringRef();
            SU.StringCreate(stringRef);

            SU.ComponentDefinitionGetName(componentDefinitionRef, stringRef);

            Name = Convert.ToStringAndRelease(stringRef);

            // Get the description.

            stringRef = new SU.StringRef();
            SU.StringCreate(stringRef);

            SU.ComponentDefinitionGetDescription(componentDefinitionRef, stringRef);

            Description = Convert.ToStringAndRelease(stringRef);
            
            // Get the entities.

            SU.EntitiesRef entitiesRef = new SU.EntitiesRef();
            
            SU.ComponentDefinitionGetEntities(componentDefinitionRef, entitiesRef);

            //entities = new Entities(entitiesRef);
            UnpackEntities(entitiesRef);
        }

        internal void Pack(Model model, SU.ModelRef modelRef)
        {
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
            //
            // To prevent all this from happening, we add the reference first.

            GuaranteeReference();

            SU.ComponentDefinitionRef[] componentDefinitionsArray =
                new SU.ComponentDefinitionRef[1];

            componentDefinitionsArray[0] = componentDefinitionRef;

            SU.ModelAddComponentDefinitions(
                modelRef,
                1,
                componentDefinitionsArray);

            SU.ComponentDefinitionSetName(
                componentDefinitionRef,
                Name);

            SU.ComponentDefinitionSetDescription(
                componentDefinitionRef,
                Description);

            SU.EntitiesRef myEntitiesRef = new SU.EntitiesRef();

            SU.ComponentDefinitionGetEntities(componentDefinitionRef, myEntitiesRef);

            //entities.Pack(model, myEntitiesRef);
            Pack(model, myEntitiesRef);
        }

        // Handles forward-references.

        internal void GuaranteeReference()
        {
            if (componentDefinitionRef == null)
            {
                componentDefinitionRef =
                    new SU.ComponentDefinitionRef();

                SU.ComponentDefinitionCreate(componentDefinitionRef);
            }
        }
    }
}
