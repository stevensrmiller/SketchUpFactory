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

            using (Group group = new Group(model, "I am a group name", "I am not used"))
            {
                Face face = MakeFace.From(
                new Vector3(-1, 0, -1),
                new Vector3(1, 0, -1),
                new Vector3(1, 0, 1),
                new Vector3(-1, 0, 1));

                Geometry geometry = new Geometry(face);
                group.Add(geometry);
            }

            //Console.WriteLine(JsonConvert.SerializeObject(model,
            //                    Newtonsoft.Json.Formatting.Indented));

            Factory.MakeSketchUpFile(model, path + @"\GroupQuad.skp");
        }
    }
}
