using System;
using ExLumina.SketchUp.API;

namespace ExLumina.SketchUp.Factory
{
    public class Group : IEntitiesParent
    {
        public string name;
        public Transform transform;
        public Entities entities;

        SU.GroupRef suGroupRef;

        public SU.GroupRef SUGroupRef
        {
            get
            {
                if (suGroupRef == null)
                {
                    suGroupRef = new SU.GroupRef();
                    SU.GroupCreate(suGroupRef);
                }

                return suGroupRef;
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
                    SU.GroupGetEntities(SUGroupRef, suEntitiesRef);
                }

                return suEntitiesRef;
            }
        }
        public Group() : this(null)
        {

        }

        public Group(Entities parent, string name)
        {
            this.name = name;
            transform = new Transform();
            entities = new Entities(this);
            parent?.groups.Add(this);
        }

        public Group(Entities parent) 
            : this(parent, "<Group name unset>")
        {

        }

        public void SULoad(Model model, SU.EntitiesRef SUEntitiesRef)
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

            SU.EntitiesAddGroup(SUEntitiesRef, SUGroupRef);

            entities.SULoad(model);

            // Set the group's transformation, name, and description.

            SU.GroupSetTransform(
                SUGroupRef,
                transform.SUTransformation);

            SU.GroupSetName(
                SUGroupRef,
                name);
        }
    }
}
