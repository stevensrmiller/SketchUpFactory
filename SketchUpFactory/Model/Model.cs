using System.Collections.Generic;

namespace ExLumina.SketchUp.Factory
{
    public partial class Model : Entities
    {
        public IList<Material> materials;
        public IList<ComponentDefinition> componentDefinitions;

        internal IDictionary<string, Material> materialsLib;
        internal IDictionary<string, ComponentDefinition> componentDefinitionsLib;

        public Model():this("<model name unset>", "<model description unset>")
        {

        }

        public Model(string name, string description) : base(name, description)
        {
            materials = new List<Material>();
            componentDefinitions = new List<ComponentDefinition>();
            materialsLib = new Dictionary<string, Material>();
            componentDefinitionsLib = new Dictionary<string, ComponentDefinition>();
        }
    }
}
