using ExLumina.SketchUp.API;
using System;

namespace ExLumina.SketchUp.Factory
{
    /// <summary>
    /// Represents a color or texture for a Face, Group, or CompInst instance.
    /// </summary>
    public class Material
    {
        /// <summary>
        /// The Material's display name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// If this Material is a color, set/get it here. Do not alter this for textures.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// null means no Texture set for this Material.
        /// </summary>
        public Texture Texture { get; set; }
        
        // Used when packing Faces.

        internal SU.MaterialRef suMaterialRef;

        int suMaterialType;
        int suMaterialColorizeType;

        /// <summary>
        /// Create a black Material with no Texture.
        /// </summary>
        /// <param name="name"></param>
        public Material(string name)
        {
            this.Name = name;
            this.Color = new Color();
            this.Texture = null;

            suMaterialType = SU.MaterialType_Colored;
            suMaterialColorizeType = SU.MaterialColorizeType_Shift;
        }

        /// <summary>
        /// Create a Material that has a Texture.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="texture"></param>
        public Material(string name, Texture texture) : this(name)
        {
            this.Texture = texture;
            suMaterialType = SU.MaterialType_Textured;
        }

        /// <summary>
        /// Create a Material with a color, but no Texture.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="color"></param>
        public Material(string name, Color color) : this(name)
        {
            this.Color = color;
        }

        internal Material(SU.MaterialRef suMaterialRef)
        {
            // Get the name.

            SU.StringRef suStringRef = new SU.StringRef();
            SU.StringCreate(suStringRef);

            SU.MaterialGetNameLegacyBehavior(suMaterialRef, suStringRef);

            Name = Convert.ToStringAndRelease(suStringRef);

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

                    Color = new Color(suColor);

                    break;

                case SU.MaterialType_Textured:

                    suTextureRef = new SU.TextureRef();

                    SU.MaterialGetTexture(suMaterialRef, suTextureRef);

                    Texture = new Texture(suTextureRef);

                    break;

                case SU.MaterialType_ColorizedTexture:

                    SU.MaterialGetColor(suMaterialRef, out suColor);

                    Color = new Color(suColor);

                    suTextureRef = new SU.TextureRef();

                    SU.MaterialGetTexture(suMaterialRef, suTextureRef);

                    Texture = new Texture(suTextureRef);

                    break;

                default:
                    throw new Exception($"Unknown material type = {suMaterialType}");
            }
        }

        internal void Pack(SU.ModelRef suModelRef)
        {

            suMaterialRef = new SU.MaterialRef();
            SU.MaterialCreate(suMaterialRef);

            SU.MaterialSetName(suMaterialRef, Name);

            switch (suMaterialType)
            {
                case SU.MaterialType_Colored:

                    SU.MaterialSetColor(suMaterialRef, Color.SUColor);

                    break;

                case SU.MaterialType_Textured:

                    Texture.Pack();
                    SU.MaterialSetTexture(suMaterialRef, Texture.textureRef);

                    break;

                case SU.MaterialType_ColorizedTexture:

                    SU.MaterialSetColor(suMaterialRef, Color.SUColor);
                    SU.MaterialSetColorizeType(suMaterialRef, suMaterialColorizeType);

                    Texture.Pack();
                    SU.MaterialSetTexture(suMaterialRef, Texture.textureRef);

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
