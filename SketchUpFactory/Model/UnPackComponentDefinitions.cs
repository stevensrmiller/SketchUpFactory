using ExLumina.SketchUp.API;
using System.Collections.Generic;

namespace ExLumina.SketchUp.Factory
{
    public partial class Model
    {
        static IDictionary<string, ComponentDefinition> UnPackComponentDefinitions(
            Model model, SU.ModelRef suModelRef)
        {
            //return new Dictionary<string, ComponentDefinition>();

            long count;

            SU.ModelGetNumComponentDefinitions(suModelRef, out count);

            SU.ComponentDefinitionRef[] suComponentDefinitionRefs = new SU.ComponentDefinitionRef[count];

            long len = count;

            SU.ModelGetComponentDefinitions(suModelRef, len, suComponentDefinitionRefs, out count);

            Dictionary<string, ComponentDefinition> componentDefinitions =
                new Dictionary<string, ComponentDefinition>();

            foreach (SU.ComponentDefinitionRef suComponentDefinitionRef in suComponentDefinitionRefs)
            {
                ComponentDefinition componentDefinition = 
                    new ComponentDefinition(model, suComponentDefinitionRef);

                componentDefinitions.Add(componentDefinition.name, componentDefinition);
            }

            return componentDefinitions;
        }
    }
}

