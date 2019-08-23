using ExLumina.SketchUp.API;
using System;

namespace ExLumina.SketchUp.Factory
{
    public class ComponentDefinition : Entities, IDisposable
    {
        internal SU.ComponentDefinitionRef componentDefinitionRef;

        public ComponentDefinition() : this(null)
        {

        }

        public ComponentDefinition(Model parent, string name, string description)
            : base(name, description)
        {
            parent?.componentDefinitions.Add(this);
        }

        public ComponentDefinition(Model parent)
            : this(parent, "<Group name unset>", "<Group description unset>")
        {

        }

        public void Dispose()
        {
            Separate();
        }
    }
}
