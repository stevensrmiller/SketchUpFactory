using System;
using ExLumina.SketchUp.API;

namespace ExLumina.SketchUp.Factory
{
    public class Group : IHasEntities
    {
        public string name;
        public Transform transform;
        public Entities Entities { get; set; }

        public Group(
            Model model,
            string name = "<group name unset>")
        {
            Entities = new Entities(model);
            this.name = name;
            transform = new Transform();
        }

        public Group(
            Model model,
            SU.GroupRef suGroupRef)
        {
            // Get the transform.

            SU.Transformation suTransformation = new SU.Transformation();

            SU.GroupGetTransform(suGroupRef, out suTransformation);

            transform = new Transform(suTransformation);

            // Get the name.

            SU.StringRef suStringRef = new SU.StringRef();
            SU.StringCreate(suStringRef);

            SU.GroupGetName(suGroupRef, suStringRef);

            name = Convert.ToStringAndRelease(suStringRef);

            // Get the entities.

            SU.EntitiesRef suEntitiesRef = new SU.EntitiesRef();

            SU.GroupGetEntities(suGroupRef, suEntitiesRef);

            Entities = new Entities(model, suEntitiesRef);
        }

        public void Pack(SU.EntitiesRef suEntitiesRef)
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

            SU.GroupRef suGroupRef = new SU.GroupRef();
            SU.GroupCreate(suGroupRef);

            SU.EntitiesAddGroup(suEntitiesRef, suGroupRef);

            SU.EntitiesRef suMyEntitiesRef = new SU.EntitiesRef();

            SU.GroupGetEntities(suGroupRef, suMyEntitiesRef);

            Entities.Pack(suMyEntitiesRef);

            // Set the group's name and transformation..

            SU.GroupSetName(
                suGroupRef,
                name);

            SU.GroupSetTransform(
                suGroupRef,
                transform.SUTransformation);
        }
    }
}
