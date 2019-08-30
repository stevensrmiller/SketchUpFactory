using System;
using Newtonsoft.Json;

namespace ExLumina.SketchUp.Factory.Examples
{
    class GroupQuad : Example
    {
        public GroupQuad(string display) : base(display)
        {

        }

        public override void Run(string path)
        {
            Model model = new Model();

            Group group = new Group(model, "I am a group name", model.Entities);

            group.Entities.Add(
                new Vector3(-1, 0, -1),
                new Vector3(1, 0, -1),
                new Vector3(1, 0, 1),
                new Vector3(-1, 0, 1));

            //Console.WriteLine(JsonConvert.SerializeObject(model,
            //                    Newtonsoft.Json.Formatting.Indented));

            model.WriteSketchUpFile(path + @"\GroupQuad.skp");
        }
    }
}
