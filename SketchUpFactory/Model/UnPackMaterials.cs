using ExLumina.SketchUp.API;
using System;
using System.Collections.Generic;

namespace ExLumina.SketchUp.Factory
{
    public partial class Model
    {
        static IDictionary<string, Material> UnpackMaterials(SU.ModelRef suModelRef)
        {
            long count;

            SU.ModelGetNumMaterials(suModelRef, out count);

            SU.MaterialRef[] suMaterialRefs = new SU.MaterialRef[count];

            long len = count;

            SU.ModelGetMaterials(suModelRef, len, suMaterialRefs, out count);

            Dictionary<string, Material> materials = 
                new Dictionary<string, Material>();

            foreach (SU.MaterialRef suMaterialRef in suMaterialRefs)
            {
                Material material = new Material(suMaterialRef);

                materials.Add(material.name, material);
            }

            return materials;
        }
    }
}

