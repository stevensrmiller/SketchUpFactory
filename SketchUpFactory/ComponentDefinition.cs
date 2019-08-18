using System.Collections.Generic;

namespace ExLumina.SketchUp.Factory
{
    public class ComponentDefinition
    {
        public string definitionName;
        public string description;
        public IList<Geometry> geometries;
        public IList<ComponentInstance> componentInstances;

        public ComponentDefinition()
        {
            definitionName = "<ComponentDefinition definitionName unset>";
            description = "<ComponentDefinition description unset>";
            geometries = new List<Geometry>();
            componentInstances = new List<ComponentInstance>();
        }
    }
}
