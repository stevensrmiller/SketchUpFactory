using ExLumina.SketchUp.API;
using System;

namespace ExLumina.SketchUp.Factory
{
    /// <summary>
    /// Instance created from a CompDef.
    /// </summary>
    public class CompInst
    {
        /// <summary>
        /// The name of the component used by this instance.
        /// </summary>
        public string ComponentName { get; set; }

        /// <summary>
        /// The Instance name to appear in the hierarchy.
        /// </summary>
        public string InstanceName { get; set; }

        /// <summary>
        /// Sets the scale, Euler angles, and translations relative to the parent.
        /// </summary>
        public Transform Transform { get; set; }

        /// <summary>
        /// Override material for faces using default material in the definition.
        /// </summary>
        public string MaterialName { get; set; }
        
        /// <summary>
        /// Create an instance with no rotation or translation, scale of one.
        /// </summary>
        public CompInst()
        {
            InstanceName = "<ComponentInstance instanceName unset>";
            ComponentName = "<ComponentInstance definitionName unset>";

            Transform = new Transform();
        }

        internal CompInst(SU.ComponentInstanceRef instanceRef)
        {
            // Get the transform.

            SU.Transformation transformation = new SU.Transformation();

            SU.ComponentInstanceGetTransform(instanceRef, out transformation);

            Transform = new Transform(transformation);

            // Get the instance name.

            SU.StringRef stringRef = new SU.StringRef();
            SU.StringCreate(stringRef);

            SU.ComponentInstanceGetName(instanceRef, stringRef);

            InstanceName = Convert.ToStringAndRelease(stringRef);

            // Get the definition name.

            SU.ComponentDefinitionRef componentDefinitionRef = new SU.ComponentDefinitionRef();

            SU.ComponentInstanceGetDefinition(instanceRef, componentDefinitionRef);

            stringRef = new SU.StringRef();
            SU.StringCreate(stringRef);

            SU.ComponentDefinitionGetName(componentDefinitionRef, stringRef);

            ComponentName = Convert.ToStringAndRelease(stringRef);

            // Note that a ComponentInstance can upcast into a DrawingElement.
            // As such, it can have an instance-wide Material set for it that
            // SketchUp will use on any Faces that use the defalt Material.
            // But you cannot set the ComponentInstance's material; you must
            // upcast first.

            // Upcast to a DrawingElement and get the material name.

            SU.DrawingElementRef drawingElementRef =
                SU.ComponentInstanceToDrawingElement(instanceRef);

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

        }

        internal void Pack(Model model, SU.EntitiesRef entitiesRef)
        {
            CompDef componentDefinition =
                model.components[ComponentName];

            // We might be making a forward reference, so guarantee
            // that the ComponentDefinition has a SketchUp pointer.

            componentDefinition.GuaranteeReference();

            SU.ComponentDefinitionRef componentDefinitionRef =
                componentDefinition.componentDefinitionRef;

            SU.ComponentInstanceRef componentInstanceRef =
                new SU.ComponentInstanceRef();

            SU.ComponentDefinitionCreateInstance(
                componentDefinitionRef,
                componentInstanceRef);

            SU.EntitiesAddInstance(
                entitiesRef,
                componentInstanceRef,
                null);

            SU.ComponentInstanceSetName(
                componentInstanceRef,
                InstanceName);

            SU.ComponentInstanceSetTransform(
                componentInstanceRef,
                Transform.SUTransformation);

            if (MaterialName != null)
            {
                Material material = null;

                try
                {
                    material = model.materials[MaterialName];
                }
                catch (Exception e)
                {
                    string msg = "\nCould not find a material named " + MaterialName;
                    throw new Exception(e.Message + msg);
                }

                SU.DrawingElementRef drawingElementRef =
                    SU.ComponentInstanceToDrawingElement(componentInstanceRef);

                SU.DrawingElementSetMaterial(
                    drawingElementRef,
                    material.suMaterialRef);
            }
        }
    }
}
