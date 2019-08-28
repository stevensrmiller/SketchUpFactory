using ExLumina.SketchUp.API;

namespace ExLumina.SketchUp.Factory
{
    public class Material
    {
        private static readonly Color blank = new Color();
        public string name;
        public Color color;

        /// <summary>
        /// null means no Texture set for this Material.
        /// </summary>
        public Texture texture;

        SU.MaterialRef suMaterialRef;

        public SU.MaterialRef SUMaterialRef
        {
            get
            {
                if (suMaterialRef == null)
                {
                    suMaterialRef = new SU.MaterialRef();
                    SU.MaterialCreate(suMaterialRef);
                }

                return suMaterialRef;
            }
        }

        Model parentModel;

        // Optional parameters would help avoid all these constructors, but
        // one of the parameters calls for the creation of a Color, which
        // isn't a value type, so can't be a default.

        public Material()
            : this("<material name unset>", new Color(), null)
        {

        }

        public Material(string name)
            : this(name, new Color(), null)
        {

        }
        
        public Material(string name, Color color)
            : this (name, color, null)
        {

        }

        public Material(string name, string textureFileName)
            : this (name, new Color(), textureFileName)
        {

        }

        public Material(string name, Color color, string textureFileName)
        {
            this.name = name;
            this.color = color;

            if (textureFileName != null)
            {
                texture = new Texture(textureFileName);
            }
        }

        public void SULoad(Model model)
        {
            SU.MaterialSetName(SUMaterialRef, name);

            SU.MaterialSetColor(SUMaterialRef, color.SUColor);

            if (texture != null)
            {
                SU.TextureRef suTextureRef = new SU.TextureRef();

                SU.TextureCreateFromFile(
                    suTextureRef,
                    texture.filename,
                    SU.MetersToInches,
                    SU.MetersToInches);

                SU.MaterialSetTexture(SUMaterialRef, suTextureRef);
            }

            SU.MaterialRef[] suMaterialRefs = new SU.MaterialRef[1];

            suMaterialRefs[0] = SUMaterialRef;

            SU.ModelAddMaterials(model.SUModelRef, 1, suMaterialRefs);
        }
    }
}
