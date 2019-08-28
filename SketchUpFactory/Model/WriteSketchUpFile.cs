using ExLumina.SketchUp.API;

namespace ExLumina.SketchUp.Factory
{
    public partial class Model
    {
        public void WriteSketchUpFile(string path)
        {
            // Load the SketchUp structores for our materials.

            foreach (Material material in materials.Values)
            {
                material.SULoad(this);
            }

            // Load the SketchUp structures for our component definitions.

            foreach (ComponentDefinition componentDefinition in componentDefinitions.Values)
            {
                componentDefinition.SULoad(this);
            }

            // Load the SketchUp structures for our entities.

            entities.SULoad(this);

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

            SU.ModelSaveToFileWithVersion(SUModelRef, path, SU.ModelVersion_SU2017);
        }
    }
}
