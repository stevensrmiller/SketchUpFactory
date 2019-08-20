using System.Collections.Generic;

namespace ExLumina.SketchUp.Factory
{
    public static partial class Factory
    {
        static IDictionary<string, Material> materialsLib =
            new Dictionary<string, Material>();

        static IDictionary<string, ComponentDefinition> componentDefinitionsLib =
            new Dictionary<string, ComponentDefinition>();
    }
}
