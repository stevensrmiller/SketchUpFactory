using ExLumina.SketchUp.API;

namespace ExLumina.SketchUp.Factory
{
    /// <summary>
    /// A collection of entities that can be part of a Model or Component.
    /// </summary>
    public class Group : Entities
    {
        const string defaultName = "<group name unset>";

        /// <summary>
        /// Display name for the Group.
        /// </summary>
        public string Name { get; set; } = defaultName;

        /// <summary>
        /// Orientation with respect to Group's parent.
        /// </summary>
        /// <remarks>
        /// If the Group has no parent, orientation is with
        /// respect to world coordinates.
        /// </remarks>
        public Transform Transform { get; set; } = new Transform();

        /// <summary>
        /// Override material for faces using default material in the Group.
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// Create an empty Group.
        /// </summary>
        /// <param name="name">Display name for the Group.</param>
        public Group(string name = defaultName)
        {
            Name = name;
        }

        internal Group(
            SU.GroupRef groupRef)
        {
            // Get the transform.

            SU.Transformation transformation = new SU.Transformation();

            SU.GroupGetTransform(groupRef, out transformation);

            Transform = new Transform(transformation);

            // Get the name.

            SU.StringRef stringRef = new SU.StringRef();
            SU.StringCreate(stringRef);

            SU.GroupGetName(groupRef, stringRef);

            Name = Convert.ToStringAndRelease(stringRef);

            // Note that a Group can upcast into a DrawingElement.
            // As such, it can have an instance-wide Material set for it that
            // SketchUp will use on any Faces that use the defalt Material.
            // But you cannot set the Group's material; you must
            // upcast first.

            // Upcast to a DrawingElement and get the material name.

            SU.DrawingElementRef drawingElementRef =
                SU.GroupToDrawingElement(groupRef);

            SU.MaterialRef materialRef = new SU.MaterialRef();

            try
            {
                SU.DrawingElementGetMaterial(drawingElementRef, materialRef);

                stringRef = new SU.StringRef();
                SU.StringCreate(stringRef);

                SU.MaterialGetNameLegacyBehavior(materialRef, stringRef);

                MaterialName = Convert.ToStringAndRelease(stringRef);
            }
            catch (SketchUpException e)
            {
                if (e.ErrorCode == SU.ErrorNoData)
                {
                    // Not an error. It just has no material.
                }
                else
                {
                    throw;
                }
            }

            // Get the entities.

            SU.EntitiesRef entitiesRef = new SU.EntitiesRef();

            SU.GroupGetEntities(groupRef, entitiesRef);

            UnpackEntities(entitiesRef);
        }

        internal new void Pack(Model model, SU.EntitiesRef entitiesRef)
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

            SU.GroupRef groupRef = new SU.GroupRef();
            SU.GroupCreate(groupRef);

            SU.EntitiesAddGroup(entitiesRef, groupRef);

            SU.EntitiesRef myEntitiesRef = new SU.EntitiesRef();

            SU.GroupGetEntities(groupRef, myEntitiesRef);

            //entities.Pack(model, myEntitiesRef);
            base.Pack(model, myEntitiesRef);

            // Set the group's name and transformation..

            SU.GroupSetName(
                groupRef,
                Name);

            SU.GroupSetTransform(
                groupRef,
                Transform.SUTransformation);
        }
    }
}
