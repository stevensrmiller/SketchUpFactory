using ExLumina.SketchUp.API;

namespace ExLumina.SketchUp.Factory
{
    public partial class Model : Entities
    {
        void UnpackMaterials(SU.ModelRef modelRef)
        {
            SU.ModelGetNumMaterials(modelRef, out long count);

            SU.MaterialRef[] materialRefs = new SU.MaterialRef[count];

            long len = count;

            SU.ModelGetMaterials(modelRef, len, materialRefs, out count);

            foreach (SU.MaterialRef materialRef in materialRefs)
            {
                Material material = new Material(materialRef);

                materials.Add(material.Name, material);
            }
        }
    }
}

