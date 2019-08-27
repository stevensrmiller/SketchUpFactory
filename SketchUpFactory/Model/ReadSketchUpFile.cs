using ExLumina.SketchUp.API;


namespace ExLumina.SketchUp.Factory
{
    public partial class Model
    {
        public void ReadSketchUpFile(string fileName)
        {
            SU.Initialize();

            // Load the model from a file.

            SU.ModelRef modelRef = new SU.ModelRef();
            SU.ModelCreateFromFile(modelRef, fileName);

            // Get the model's entity container.

            SU.EntitiesRef entitiesRef = new SU.EntitiesRef();
            SU.ModelGetEntities(modelRef, entitiesRef);

            // Get the materials.

            //ReadMaterials(model, modelRef);

            // Get the faces.

            ReadFaces(entitiesRef);

            //ReadComponentDefinitions(model, modelRef);

            // Get the component instances.

            //ReadComponentInstances(model, entitiesRef);

            // Get the groups.

            //ReadGroups(model, entitiesRef);

            //long numDefinitions;
            //SU.ModelGetNumComponentDefinitions(modelRef, out numDefinitions);

            //Console.WriteLine("NUMBER OF COMPONENT DEFINITIONS = {0}.", numDefinitions);

            //SU.ComponentDefinitionRef[] definitionRefs = new SU.ComponentDefinitionRef[numDefinitions];
            //long definitionsRetrieved;
            //SU.ModelGetComponentDefinitions(
            //    modelRef,
            //    numDefinitions,
            //    definitionRefs,
            //    out definitionsRetrieved);

            //Console.WriteLine("DEFINITIONS RETRIEVED = {0}", definitionsRetrieved);

            WriteSketchUpFile(@"C:\users\smiller\Factory Output\dupe.skp");
            SU.ModelRelease(modelRef);
            SU.Terminate();
        }
    }
}
