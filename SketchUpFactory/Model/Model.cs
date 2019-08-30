using System;
using System.Collections.Generic;
using ExLumina.SketchUp.API;

namespace ExLumina.SketchUp.Factory
{
    public partial class Model : IHasEntities
    {
        public string name;
        public string description;
        public Entities Entities { get; set; }

        internal IDictionary<string, ComponentDefinition> componentDefinitions;
        internal IDictionary<string, Material> materials;

        public Model() : this("<model name unset>", "<model description unset>")
        {

        }

        public Model(string name, string description)
        {
            this.name = name;
            this.description = description;
            Entities = new Entities(this);
            materials = new Dictionary<string, Material>();
            componentDefinitions = new Dictionary<string, ComponentDefinition>();
        }

        /// <summary>
        /// Construct a Model from a SketchUp file.
        /// </summary>
        /// <param name="path">SketchUp file path.</param>
        public Model(string path)
        {
            SU.Initialize();

            // Creating this as a local variable insures it won't survive
            // beyond this method.

            SU.ModelRef suModelRef = new SU.ModelRef();

            SU.ModelCreateFromFile(suModelRef, path);

            materials = UnPackMaterials(suModelRef);

            componentDefinitions = UnPackComponentDefinitions(this, suModelRef);

            SU.EntitiesRef suEntitiesRef = new SU.EntitiesRef();

            SU.ModelGetEntities(suModelRef, suEntitiesRef);

            Entities = new Entities(this, suEntitiesRef);

            SU.Terminate();
        }

        public void Add(Material material)
        {
            materials.Add(material.name, material);
        }
    }
}
