using ExLumina.SketchUp.API;
using System;

namespace ExLumina.SketchUp.Factory
{
    public class ComponentDefinition
    {
        public string name;
        public string description;

        public Entities entities;

        internal SU.ComponentDefinitionRef componentDefinitionRef;

        public ComponentDefinition() : this(null)
        {

        }

        public ComponentDefinition(Model parent, string name, string description)
        {
            parent?.componentDefinitions.Add(this);

            this.name = name;
            this.description = description;
            entities = new Entities();
        }

        public ComponentDefinition(Model parent)
            : this(parent, "<Group name unset>", "<Group description unset>")
        {

        }
    }
}
