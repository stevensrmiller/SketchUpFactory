using ExLumina.SketchUp.API;

namespace ExLumina.SketchUp.Factory
{
    public static partial class Factory
    {
        static void CreateMaterial(Model model, Material material, SU.ModelRef modelRef)
        {
            SU.MaterialRef materialRef = new SU.MaterialRef();
            SU.MaterialCreate(materialRef);
            SU.MaterialSetName(materialRef, material.name);
            SU.MaterialSetColor(materialRef, material.color.SUColor);

            if (material.texture != null)
            {
                SU.TextureRef textureRef = new SU.TextureRef();
                SU.TextureCreateFromFile(
                    textureRef,
                    material.texture.filename,
                    SU.MetersToInches,
                    SU.MetersToInches);
                SU.MaterialSetTexture(materialRef, textureRef);
            }

            SU.MaterialRef[] mats = new SU.MaterialRef[1];
            mats[0] = materialRef;
            SU.ModelAddMaterials(modelRef, 1, mats);

            material.materialRef = materialRef;
            model.materialsLib.Add(material.name, material);
            material.isInUse = true;
        }
    }
}
