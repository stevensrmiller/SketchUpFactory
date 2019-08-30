using ExLumina.SketchUp.API;
using System;

namespace ExLumina.SketchUp.Factory
{
    public class ComponentDefinition : IHasEntities
    {
        public string name;
        public string description;

        public Entities Entities { get; set; }

        // Used when packing ComponentInstances.

        internal SU.ComponentDefinitionRef suComponentDefinitionRef;

        public ComponentDefinition(
            Model model,
            string name = "<component definition name unset>",
            string description = "<component definition description unset>")
        {
            model.componentDefinitions.Add(name, this);
            this.name = name;
            this.description = description;
            Entities = new Entities(model);
        }

        public ComponentDefinition(
            Model model,
            SU.ComponentDefinitionRef suComponentDefinitionRef)
        {
            // Get the name.

            SU.StringRef suStringRef = new SU.StringRef();
            SU.StringCreate(suStringRef);

            SU.ComponentDefinitionGetName(suComponentDefinitionRef, suStringRef);

            name = Convert.ToStringAndRelease(suStringRef);

            // Get the description.

            suStringRef = new SU.StringRef();
            SU.StringCreate(suStringRef);

            SU.ComponentDefinitionGetDescription(suComponentDefinitionRef, suStringRef);

            description = Convert.ToStringAndRelease(suStringRef);
            
            // Get the entities.

            SU.EntitiesRef suEntitiesRef = new SU.EntitiesRef();
            
            SU.ComponentDefinitionGetEntities(suComponentDefinitionRef, suEntitiesRef);

            Entities = new Entities(model, suEntitiesRef);
        }

        public void Pack(SU.ModelRef suModelRef)
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

            componentDefinitionsArray[0] = suComponentDefinitionRef;

            SU.ModelAddComponentDefinitions(
                suModelRef,
                1,
                componentDefinitionsArray);

            SU.ComponentDefinitionSetName(
                suComponentDefinitionRef,
                name);

            SU.ComponentDefinitionSetDescription(
                suComponentDefinitionRef,
                description);

            SU.EntitiesRef suMyEntitiesRef = new SU.EntitiesRef();

            SU.ComponentDefinitionGetEntities(suComponentDefinitionRef, suMyEntitiesRef);

            Entities.Pack(suMyEntitiesRef);
        }

        internal void GuaranteeReference()
        {
            if (suComponentDefinitionRef == null)
            {
                suComponentDefinitionRef =
                    new SU.ComponentDefinitionRef();

                SU.ComponentDefinitionCreate(suComponentDefinitionRef);
            }
        }
    }
}
