using ExLumina.SketchUp.API;
using System;

namespace ExLumina.SketchUp.Factory
{
    public class ComponentDefinition : IEntitiesParent
    {
        public string name;
        public string description;

        public Entities entities;

        SU.ComponentDefinitionRef suComponentDefinitionRef;

        public SU.ComponentDefinitionRef SUComponentDefinitionRef
        {
            get
            {
                if (suComponentDefinitionRef == null)
                {
                    suComponentDefinitionRef = new SU.ComponentDefinitionRef();
                    SU.ComponentDefinitionCreate(suComponentDefinitionRef);
                }

                return suComponentDefinitionRef;
            }
        }

        SU.EntitiesRef suEntitiesRef;

        public SU.EntitiesRef SUEntitiesRef
        {
            get
            {
                if (suEntitiesRef == null)
                {
                    suEntitiesRef = new SU.EntitiesRef();
                    SU.ComponentDefinitionGetEntities(SUComponentDefinitionRef, suEntitiesRef);
                }

                return suEntitiesRef;
            }
        }

        public ComponentDefinition(Model model, string name, string description)
        {
            // Did a forward reference (an instance of this definition that
            // is part of some other definition that has already been loaded)
            // create a placeholder?

            //if (model.componentDefinitions.ContainsKey(name))
            //{
            //    ComponentDefinition ph = model.componentDefinitions[name];
            //    suComponentDefinitionRef = ph.SUComponentDefinitionRef;
            //    model.componentDefinitions.Remove(name);
            //}

            model.componentDefinitions.Add(name, this);

            this.name = name;
            this.description = description;
            entities = new Entities(this);
        }

        public ComponentDefinition(Model parent)
            : this(parent, "<Group name unset>", "<Group description unset>")
        {

        }

        public void SULoad(Model model)
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

            SU.ComponentDefinitionRef[] componentDefinitionsArray =
                new SU.ComponentDefinitionRef[1];

            componentDefinitionsArray[0] = SUComponentDefinitionRef;

            SU.ModelAddComponentDefinitions(
                model.SUModelRef,
                1,
                componentDefinitionsArray);

            SU.ComponentDefinitionSetName(
                SUComponentDefinitionRef,
                name);

            SU.ComponentDefinitionSetDescription(
                SUComponentDefinitionRef,
                description);

            entities.SULoad(model);
        }
    }
}
