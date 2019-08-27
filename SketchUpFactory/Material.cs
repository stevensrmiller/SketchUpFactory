using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private SU.MaterialRef materialRef;

        public SU.MaterialRef SUmaterialRef
        {
            get
            {
                if (materialRef == null)
                {

                    materialRef = new SU.MaterialRef();
                    SU.MaterialCreate(materialRef);
                    SU.MaterialSetName(materialRef, name);
                    SU.MaterialSetColor(materialRef, color.SUColor);

                    if (texture != null)
                    {
                        SU.TextureRef textureRef = new SU.TextureRef();
                        SU.TextureCreateFromFile(
                            textureRef,
                            texture.filename,
                            SU.MetersToInches,
                            SU.MetersToInches);
                        SU.MaterialSetTexture(materialRef, textureRef);
                    }
                }

                return materialRef;
            }
        }


        internal bool isInUse;

        // Optional parameters would help avoid all these constructors, but
        // one of the parameters calls for the creation of a Color, which
        // isn't a value type, so can't be a default.

        public Material() : this("<material name unset>", new Color(), null)
        {

        }

        public Material(string name) : this(name, new Color(), null)
        {

        }
        
        public Material(string name, Color color) : this (name, color, null)
        {

        }

        public Material(string name, string textureFileName) : this (name, new Color(), textureFileName)
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
    }
}
