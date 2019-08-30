using ExLumina.SketchUp.API;
using System;

namespace ExLumina.SketchUp.Factory
{
    public class Material
    {
        public string name;
        public Color color;

        /// <summary>
        /// null means no Texture set for this Material.
        /// </summary>
        public Texture texture;

        // Used when packing Faces.

        internal SU.MaterialRef suMaterialRef;

        int suMaterialType;
        int suMaterialColorizeType;

        public Material(string name)
        {
            this.name = name;
            this.color = new Color();
            this.texture = null;

            suMaterialType = SU.MaterialType_Colored;
            suMaterialColorizeType = SU.MaterialColorizeType_Shift;
        }

        public Material(string name, Texture texture) : this(name)
        {
            this.texture = texture;
            suMaterialType = SU.MaterialType_Textured;
        }

        public Material(string name, Color color) : this(name)
        {
            this.color = color;
        }

        public Material(SU.MaterialRef suMaterialRef)
        {
            // Get the name.

            SU.StringRef suStringRef = new SU.StringRef();
            SU.StringCreate(suStringRef);

            SU.MaterialGetNameLegacyBehavior(suMaterialRef, suStringRef);

            name = Convert.ToStringAndRelease(suStringRef);

            // Get the types.

            SU.MaterialGetType(suMaterialRef, out suMaterialType);
            SU.MaterialGetColorizeType(suMaterialRef, out suMaterialColorizeType);

            // Get the color and/or texture.

            SU.Color suColor;
            SU.TextureRef suTextureRef;

            switch (suMaterialType)
            {
                case SU.MaterialType_Colored:

                    SU.MaterialGetColor(suMaterialRef, out suColor);

                    color = new Color(suColor);

                    break;

                case SU.MaterialType_Textured:

                    suTextureRef = new SU.TextureRef();

                    SU.MaterialGetTexture(suMaterialRef, suTextureRef);

                    texture = new Texture(suTextureRef);

                    break;

                case SU.MaterialType_ColorizedTexture:

                    SU.MaterialGetColor(suMaterialRef, out suColor);

                    color = new Color(suColor);

                    suTextureRef = new SU.TextureRef();

                    SU.MaterialGetTexture(suMaterialRef, suTextureRef);

                    texture = new Texture(suTextureRef);

                    break;

                default:
                    throw new Exception($"Unknown material type = {suMaterialType}");
            }
        }

        public void Pack(SU.ModelRef suModelRef)
        {

            suMaterialRef = new SU.MaterialRef();
            SU.MaterialCreate(suMaterialRef);

            SU.MaterialSetName(suMaterialRef, name);

            switch (suMaterialType)
            {
                case SU.MaterialType_Colored:

                    SU.MaterialSetColor(suMaterialRef, color.SUColor);

                    break;

                case SU.MaterialType_Textured:

                    texture.Pack();
                    SU.MaterialSetTexture(suMaterialRef, texture.suTextureRef);

                    break;

                case SU.MaterialType_ColorizedTexture:

                    SU.MaterialSetColor(suMaterialRef, color.SUColor);
                    SU.MaterialSetColorizeType(suMaterialRef, suMaterialColorizeType);

                    texture.Pack();
                    SU.MaterialSetTexture(suMaterialRef, texture.suTextureRef);

                    break;

                default:
                    throw new Exception($"Unknown material type = {suMaterialType}");
            }

            SU.MaterialRef[] suMaterialRefs = new SU.MaterialRef[1];

            suMaterialRefs[0] = suMaterialRef;

            SU.ModelAddMaterials(suModelRef, 1, suMaterialRefs);
        }
    }
}
