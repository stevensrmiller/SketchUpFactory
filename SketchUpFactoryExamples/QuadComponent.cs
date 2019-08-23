using System;
using Newtonsoft.Json;

namespace ExLumina.SketchUp.Factory.Examples
{
    class QuadComponent : Example
    {
        public QuadComponent(string display) : base(display)
        {

        }

        public override void Run(string path)
        {
            Model model = new Model();

            using (ComponentDefinition def = new ComponentDefinition(
                model, "Flat", "A Square"))
            {
                def.Add(
                    new Vector3(-1, 0, -1),
                    new Vector3(1, 0, -1),
                    new Vector3(1, 0, 1),
                    new Vector3(-1, 0, 1));

                ComponentInstance ci2 = new ComponentInstance
                {
                    definitionName = "Pointy",
                    instanceName = "Tri the Angle"
                };

                ci2.transform.translation.y = 1;
                ci2.transform.rotation.z = -20;

                def.Add(ci2);
            }

            ComponentInstance ci = new ComponentInstance
            {
                definitionName = "Flat",
                instanceName = "Quad the First"
            };

            model.Add(ci);

            using (ComponentDefinition def = new ComponentDefinition(
                model, "Pointy", "A Triangle"))
            {
                def.Add(
                    new Vector3(-1, 0, -1),
                    new Vector3(1, 0, -1),
                    new Vector3(0, 0, 1));
            }

            //Console.WriteLine(JsonConvert.SerializeObject(model,
            //                    Newtonsoft.Json.Formatting.Indented));

            Factory.MakeSketchUpFile(model, path + @"\QuadComponent.skp");
        }
    }
}
