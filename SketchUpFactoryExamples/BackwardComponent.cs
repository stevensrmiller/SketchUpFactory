using System;
using Newtonsoft.Json;

namespace ExLumina.SketchUp.Factory.Examples
{
    class BackwardComponent : Example
    {
        public BackwardComponent(string display) : base(display)
        {

        }

        public override void Run(string path)
        {
            using (Model model = new Model())
            {
                ComponentDefinition pointy = new ComponentDefinition(
                    model,
                    "Pointy",
                    "A Triangle");

                pointy.entities.Add(
                    new Vector3(-1, 0, -1),
                    new Vector3(1, 0, -1),
                    new Vector3(0, 0, 1));

                ComponentDefinition flat = new ComponentDefinition(
                    model,
                    "Flat",
                    "A Square");

                flat.entities.Add(
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

                flat.entities.Add(ci2);

                ComponentInstance ci = new ComponentInstance
                {
                    definitionName = "Flat",
                    instanceName = "Quad the First"
                };

                model.entities.Add(ci);

                model.WriteSketchUpFile(path + @"\BackwardComponent.skp");
            }
        }
    }
}
