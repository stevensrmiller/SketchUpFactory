using System;
using Newtonsoft.Json;

namespace ExLumina.SketchUp.Factory.Examples
{
    class ThreePlyTree : Example
    {
        public ThreePlyTree(string display) : base(display)
        {

        }

        public override void Run(string path)
        {
            ComponentInstance ci;

            using (Model model = new Model())
            {
                // Define the root level.

                ComponentDefinition def = new ComponentDefinition(
                    model,
                    "Flat",
                    "A Square");

                def.entities.Add(
                    new Vector3(-1, 0, -1),
                    new Vector3(1, 0, -1),
                    new Vector3(1, 0, 1),
                    new Vector3(-1, 0, 1));

                // Two branches.

                ci = new ComponentInstance
                {
                    definitionName = "Pointy",
                    instanceName = "On the right."
                };

                ci.transform.translation.x = 3;
                ci.transform.translation.z = 3;

                def.entities.Add(ci);

                ci = new ComponentInstance
                {
                    definitionName = "Pointy",
                    instanceName = "On the left."
                };

                ci.transform.translation.x = -3;
                ci.transform.translation.z = 3;

                def.entities.Add(ci);

                ci = new ComponentInstance
                {
                    definitionName = "Flat",
                    instanceName = "At the origin."
                };

                model.entities.Add(ci);

                ComponentDefinition pointy = new ComponentDefinition(
                    model,
                    "Pointy",
                    "A Triangle");

                pointy.entities.Add(
                    new Vector3(-1, 0, -1),
                    new Vector3(1, 0, -1),
                    new Vector3(0, 0, 1));

                // Two branches of its own.

                ci = new ComponentInstance
                {
                    definitionName = "Skinny",
                    instanceName = "On the far upper right."
                };

                ci.transform.translation.x = 3;
                ci.transform.translation.z = 3;

                pointy.entities.Add(ci);

                ci = new ComponentInstance
                {
                    definitionName = "Skinny",
                    instanceName = "On the far upper left."
                };

                ci.transform.translation.x = -3;
                ci.transform.translation.z = 3;

                pointy.entities.Add(ci);

                //Console.WriteLine(JsonConvert.SerializeObject(model,
                //                    Newtonsoft.Json.Formatting.Indented));

                ComponentDefinition skinny = new ComponentDefinition(
                    model,
                    "Skinny",
                    "A Slim Quad");

                skinny.entities.Add(
                    new Vector3(-.2, 0, -1),
                    new Vector3(.2, 0, -1),
                    new Vector3(.2, 0, 1),
                    new Vector3(-.2, 0, 1));

                model.WriteSketchUpFile(path + @"\ThreePlyTree.skp");
            }
        }
    }
}
