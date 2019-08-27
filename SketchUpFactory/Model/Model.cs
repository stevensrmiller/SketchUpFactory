using System;
using System.Collections.Generic;

namespace ExLumina.SketchUp.Factory
{
    public partial class Model
    {
        public string name;
        public string description;

        public IList<Material> materials;
        public IList<ComponentDefinition> componentDefinitions;
        public Entities entities;

        internal IDictionary<string, Material> materialsLib;
        internal IDictionary<string, ComponentDefinition> componentDefinitionsLib;

        public Model() : this("<model name unset>", "<model description unset>")
        {

        }

        public Model(string name, string description)
        {
            this.name = name;
            this.description = description;
            materials = new List<Material>();
            componentDefinitions = new List<ComponentDefinition>();
            entities = new Entities();
            materialsLib = new Dictionary<string, Material>();
            componentDefinitionsLib = new Dictionary<string, ComponentDefinition>();
        }

        public Model(string path) : this()
        {
            ReadSketchUpFile(path);
        }
    }
}
