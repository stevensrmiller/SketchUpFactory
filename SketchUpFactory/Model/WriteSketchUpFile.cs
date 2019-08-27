using System;
using ExLumina.SketchUp.API;
using System.Collections.Generic;

namespace ExLumina.SketchUp.Factory
{
    public partial class Model
    {
        public void WriteSketchUpFile(string path)
        {
            // Init and open a model structure.

            SU.Initialize();
            SU.ModelRef modelRef = new SU.ModelRef();
            SU.ModelCreate(modelRef);
            SU.EntitiesRef entitiesRef = new SU.EntitiesRef();
            SU.ModelGetEntities(modelRef, entitiesRef);

            // Create the materials.

            SU.MaterialRef[] materialRefs = new SU.MaterialRef[materials.Count];

            for (int i = 0; i < materials.Count; ++i)
            {
                materialRefs[i] = materials[i].SUmaterialRef;
            }

            SU.ModelAddMaterials(modelRef, materials.Count, materialRefs);

            // Create the geometries.

            foreach (Geometry geometry in entities.geometries)
            {
                CreateGeometry(geometry, entitiesRef);
            }

            // Create the component definitions.

            foreach (ComponentDefinition componentDefinition in componentDefinitions)
            {
                CreateComponentDefinition(componentDefinition, modelRef);
            }

            // Create instances.

            foreach (ComponentInstance componentInstance in entities.componentInstances)
            {
                CreateComponentInstance(componentInstance, entitiesRef);
            }

            // Create groups.

            foreach (Group group in entities.groups)
            {
                CreateGroup(group, entitiesRef);
            }

            // Set style and camera, write the file, terminate.

            SU.StylesRef stylesRef = new SU.StylesRef();
            SU.ModelGetStyles(modelRef, stylesRef);
            SU.StylesAddStyle(stylesRef, "base.style", true);

            SU.CameraRef cameraRef = new SU.CameraRef();

            SU.ModelGetCamera(modelRef, cameraRef);

            SU.CameraSetOrientation(
                cameraRef,
                new SU.Point3D(
                     10,// * SU.MetersToInches,
                    -10,// * SU.MetersToInches,
                     10),// * SU.MetersToInches),
                new SU.Point3D(0, 0, 0),
                new SU.Vector3D(0, 0, 1));
            
            SU.ModelSaveToFileWithVersion(modelRef, path, SU.ModelVersion_SU2017);

            SU.ModelRelease(modelRef);

            SU.Terminate();
        }
    }
}
