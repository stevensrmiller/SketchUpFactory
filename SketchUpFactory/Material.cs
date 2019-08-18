using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Material()
        {
            name = "<Material name unset>";
            color = new Color();
        }
    }
}
