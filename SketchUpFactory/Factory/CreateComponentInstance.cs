using ExLumina.SketchUp.API;
using System;

namespace ExLumina.SketchUp.Factory
{
    public static partial class Factory
    {
        static void CreateComponentInstance(
            ComponentInstance componentInstance,
            SU.EntitiesRef entitiesRef)
        {
            ComponentDefinition componentDefinition;

            SU.ComponentDefinitionRef componentDefinitionRef;

            // If this is a forward reference, create a blank
            // component definition.

            if (componentDefinitionsLib.ContainsKey(
                    componentInstance.definitionName))
            {
                componentDefinition =
                    componentDefinitionsLib[componentInstance.definitionName];

                componentDefinitionRef =
                    componentDefinition.componentDefinitionRef;
            }
            else
            {
                componentDefinition =
                    new ComponentDefinition();

                componentDefinitionRef = new SU.ComponentDefinitionRef();
                SU.ComponentDefinitionCreate(componentDefinitionRef);

                componentDefinition.componentDefinitionRef = componentDefinitionRef;

                componentDefinitionsLib.Add(
                    componentInstance.definitionName,
                    componentDefinition);
            }

            SU.ComponentInstanceRef componentInstanceRef =
                new SU.ComponentInstanceRef();

            SU.ComponentDefinitionCreateInstance(
                componentDefinitionRef,
                componentInstanceRef);

            SU.ComponentInstanceSetName(
                componentInstanceRef,
                componentInstance.instanceName);

            SU.Transformation transformationS = new SU.Transformation();

            SU.TransformationNonUniformScale(
                ref transformationS,
                componentInstance.scale.x,
                componentInstance.scale.y,
                componentInstance.scale.z);

            SU.Transformation transformationRz = new SU.Transformation();

            SU.TransformationRotation(
                ref transformationRz,
                new SU.Point3D(0, 0, 0),
                new SU.Vector3D(0, 0, 1),
                componentInstance.rotation.z * Math.PI / 180);

            SU.Transformation transformationM0 = new SU.Transformation();

            SU.TransformationMultiply(
                ref transformationRz,
                ref transformationS,
                ref transformationM0);

            SU.Transformation transformationRx = new SU.Transformation();

            SU.TransformationRotation(
                ref transformationRx,
                new SU.Point3D(0, 0, 0),
                new SU.Vector3D(1, 0, 0),
                componentInstance.rotation.x * Math.PI / 180);

            SU.Transformation transformationM1 = new SU.Transformation();

            SU.TransformationMultiply(
                ref transformationRx,
                ref transformationM0,
                ref transformationM1);

            SU.Transformation transformationRy = new SU.Transformation();

            SU.TransformationRotation(
                ref transformationRy,
                new SU.Point3D(0, 0, 0),
                new SU.Vector3D(0, 1, 0),
                componentInstance.rotation.y * Math.PI / 180);

            SU.Transformation transformationM2 = new SU.Transformation();

            SU.TransformationMultiply(
                ref transformationRy,
                ref transformationM1,
                ref transformationM2);

            SU.Transformation transformationT = new SU.Transformation();

            SU.TransformationTranslation(
                ref transformationT,
                new SU.Vector3D(
                    componentInstance.translation.x,
                    componentInstance.translation.y,
                    componentInstance.translation.z));

            SU.Transformation transformationM3 = new SU.Transformation();

            SU.TransformationMultiply(
                ref transformationT,
                ref transformationM2,
                ref transformationM3);

            SU.ComponentInstanceSetTransform(
                componentInstanceRef,
                transformationM3);

            SU.EntitiesAddInstance(
                entitiesRef,
                componentInstanceRef,
                null);
        }
    }
}
