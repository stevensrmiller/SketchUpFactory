using ExLumina.SketchUp.API;

namespace ExLumina.SketchUp.Factory
{
    public static partial class Factory
    {
        static void CreateGroup(Model model, Group group, SU.EntitiesRef entitiesRef)
        {
            SU.GroupRef groupRef = new SU.GroupRef();
            SU.GroupCreate(groupRef);

            SU.EntitiesRef groupEntitiesRef = new SU.EntitiesRef();
            SU.GroupGetEntities(groupRef, groupEntitiesRef);

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

            SU.EntitiesAddGroup(entitiesRef, groupRef);

            // Create the group's geometries.

            foreach (Geometry geometry in group.geometries)
            {
                CreateGeometry(model, geometry, groupEntitiesRef);
            }

            // Create the group's instances.

            foreach (ComponentInstance componentInstance in group.componentInstances)
            {
                CreateComponentInstance(model, componentInstance, groupEntitiesRef);
            }

            // Create the group's groups.

            foreach (Group subGroup in group.groups)
            {
                CreateGroup(model, subGroup, groupEntitiesRef);
            }

            // Set the group's transformation, name, and description.

            SU.GroupSetTransform(
                groupRef,
                group.transform.SUTransformation);

            SU.GroupSetName(
                groupRef,
                group.name);

            SU.GroupSetDescription(
                groupRef,
                group.description);
        }
    }
}
