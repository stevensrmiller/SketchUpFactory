using System.Collections.Generic;
using ExLumina.SketchUp.API;

namespace ExLumina.SketchUp.Factory
{
    public class ComponentDefinition
    {
        public string definitionName;
        public string description;
        public IList<Geometry> geometries;
        public IList<ComponentInstance> componentInstances;

        internal SU.ComponentDefinitionRef componentDefinitionRef;

        public ComponentDefinition()
        {
            definitionName = "<ComponentDefinition definitionName unset>";
            description = "<ComponentDefinition description unset>";
            geometries = new List<Geometry>();
            componentInstances = new List<ComponentInstance>();
        }
    }
}
