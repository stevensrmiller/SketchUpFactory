using ExLumina.SketchUp.API;

namespace ExLumina.SketchUp.Factory
{
    public partial class Model
    {
        public void WriteSketchUpFile(string path)
        {
            SU.Initialize();

            SU.ModelRef suModelRef = new SU.ModelRef();
            SU.ModelCreate(suModelRef);

            // Load the SketchUp structures for our materials.

            foreach (Material material in materials.Values)
            {
                material.Pack(suModelRef);
            }

            // Load the SketchUp structures for our component definitions.

            foreach (ComponentDefinition componentDefinition in componentDefinitions.Values)
            {
                componentDefinition.Pack(suModelRef);
            }

            // Load the SketchUp structures for our entities.

            SU.EntitiesRef suEntitiesRef = new SU.EntitiesRef();

            SU.ModelGetEntities(suModelRef, suEntitiesRef);

            Entities.Pack(suEntitiesRef);

            // Set style and camera.

            SU.StylesRef stylesRef = new SU.StylesRef();
            SU.ModelGetStyles(suModelRef, stylesRef);
            SU.StylesAddStyle(stylesRef, "base.style", true);

            SU.CameraRef cameraRef = new SU.CameraRef();

            SU.ModelGetCamera(suModelRef, cameraRef);

            SU.CameraSetOrientation(
                cameraRef,
                new SU.Point3D(
                     10,// * SU.MetersToInches,
                    -10,// * SU.MetersToInches,
                     10),// * SU.MetersToInches),
                new SU.Point3D(0, 0, 0),
                new SU.Vector3D(0, 0, 1));

            SU.ModelSaveToFileWithVersion(suModelRef, path, SU.ModelVersion_SU2017);

            SU.ModelRelease(suModelRef);

            SU.Terminate();

            // Mop up left-over references.

            foreach (Material material in materials.Values)
            {
                material.suMaterialRef = null;
            }

            foreach (ComponentDefinition componentDefinition in componentDefinitions.Values)
            {
                componentDefinition.suComponentDefinitionRef = null;
            }
        }
    }
}
