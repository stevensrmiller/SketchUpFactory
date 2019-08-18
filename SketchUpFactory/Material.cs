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
        public string name;
        public Color color;

        /// <summary>
        /// null means no Texture set for this Material.
        /// </summary>
        public Texture texture;

        internal SU.MaterialRef materialRef;
        internal bool isInUse;

        public Material()
        {
            name = "<Material name unset>";
            color = new Color();
        }
    }
}
