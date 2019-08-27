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

            ComponentDefinition flat = new ComponentDefinition(
                model, 
                "Flat",
                "A Square");

            using (Entities entities = flat.entities)
            {
                entities.Add(
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

                entities.Add(ci2);
            }

            ComponentInstance ci = new ComponentInstance
            {
                definitionName = "Flat",
                instanceName = "Quad the First"
            };

            model.entities.Add(ci);

            ComponentDefinition pointy = new ComponentDefinition(
                model, 
                "Pointy",
                "A Triangle");

            using (Entities entities = pointy.entities)
            {
                entities.Add(
                    new Vector3(-1, 0, -1),
                    new Vector3(1, 0, -1),
                    new Vector3(0, 0, 1));
            }

            //Console.WriteLine(JsonConvert.SerializeObject(model,
            //                    Newtonsoft.Json.Formatting.Indented));

            model.WriteSketchUpFile(path + @"\QuadComponent.skp");
        }
    }
}
