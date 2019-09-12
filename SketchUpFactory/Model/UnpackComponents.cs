using ExLumina.SketchUp.API;

namespace ExLumina.SketchUp.Factory
{
    public partial class Model : Entities
    {
        void UnpackComponents(SU.ModelRef modelRef)
        {
            SU.ModelGetNumComponentDefinitions(modelRef, out long count);

            SU.ComponentDefinitionRef[] componentDefinitionRefs = new SU.ComponentDefinitionRef[count];

            long len = count;

            SU.ModelGetComponentDefinitions(modelRef, len, componentDefinitionRefs, out count);

            foreach (SU.ComponentDefinitionRef componentDefinitionRef in componentDefinitionRefs)
            {
                CompDef component =
                    new CompDef(componentDefinitionRef);

                components.Add(component.Name, component);
            }
        }
    }
}

