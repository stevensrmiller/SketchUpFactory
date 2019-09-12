using ExLumina.SketchUp.API;

namespace ExLumina.SketchUp.Factory
{
    public partial class Model : Entities
    {
        public void WriteSketchUpFile(string path)
        {
            SU.Initialize();

            SU.ModelRef modelRef = new SU.ModelRef();
            SU.ModelCreate(modelRef);

            // Load the SketchUp structures for our materials.

            foreach (Material material in materials.Values)
            {
                material.Pack(modelRef);
            }

            // Load the SketchUp structures for our component definitions.

            foreach (CompDef componentDefinition in components.Values)
            {
                componentDefinition.Pack(this, modelRef);
            }

            // Load the SketchUp structures for our entities.

            SU.EntitiesRef entitiesRef = new SU.EntitiesRef();

            SU.ModelGetEntities(modelRef, entitiesRef);

            //entities.Pack(this, entitiesRef);
            Pack(this, entitiesRef);
            // Set style and camera.

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

            // Mop up left-over references.

            foreach (Material material in materials.Values)
            {
                material.suMaterialRef = null;
            }

            foreach (CompDef componentDefinition in components.Values)
            {
                componentDefinition.componentDefinitionRef = null;
            }
        }
    }
}
