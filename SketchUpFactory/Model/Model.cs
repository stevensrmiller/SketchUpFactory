using System;
using System.Collections.Generic;
using ExLumina.SketchUp.API;

namespace ExLumina.SketchUp.Factory
{
    public partial class Model : IDisposable, IEntitiesParent
    {
        private static int modelCount;

        public string name;
        public string description;
        public Entities entities;

        internal IDictionary<string, ComponentDefinition> componentDefinitions;
        internal IDictionary<string, Material> materials;

        private SU.ModelRef suModelRef;

        public SU.ModelRef SUModelRef
        {
            get
            {
                if (suModelRef == null)
                {
                    suModelRef = new SU.ModelRef();
                    SU.ModelCreate(suModelRef);
                }

                return this.suModelRef;
            }
        }

        private SU.EntitiesRef suEntitiesRef;

        public SU.EntitiesRef SUEntitiesRef
        {
            get
            {
                if (suEntitiesRef == null)
                {
                    suEntitiesRef = new SU.EntitiesRef();
                    SU.ModelGetEntities(SUModelRef, suEntitiesRef);
                }

                return suEntitiesRef;
            }
        }

        public Model() : this("<model name unset>", "<model description unset>")
        {

        }

        public Model(string name, string description)
        {
            this.name = name;
            this.description = description;
            entities = new Entities(this);
            materials = new Dictionary<string, Material>();
            componentDefinitions = new Dictionary<string, ComponentDefinition>();

            if (modelCount == 0)
            {
                SU.Initialize();

                modelCount = modelCount + 1;
            }
        }

        public Model(string path) : this()
        {
            ReadSketchUpFile(path);
        }

        public void Dispose()
        {
            modelCount = modelCount - 1;

            SU.Terminate();
        }

        public void Add(Material material)
        {
            materials.Add(material.name, material);
        }
    }
}
