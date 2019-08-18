using System.Collections.Generic;

namespace ExLumina.SketchUp.Factory
{
    public class Model
    {
        public IList<Material> materials;
        public IList<Geometry> geometries;
        public IList<ComponentDefinition> componentDefinitions;
        public IList<ComponentInstance> componentInstances;

        public Model()
        {
            materials = new List<Material>();
            geometries = new List<Geometry>();
            componentDefinitions = new List<ComponentDefinition>();
            componentInstances = new List<ComponentInstance>();
        }
    }
}
