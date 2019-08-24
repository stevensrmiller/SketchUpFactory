using System;
using Newtonsoft.Json;

namespace ExLumina.SketchUp.Factory.Examples
{
    class PlainQuad : Example
    {
        public PlainQuad(string display) : base(display)
        {

        }

        public override void Run(string path)
        {
            Model model = new Model();

            Face face = new Face(
                new Vector3(-1, 0, -1),
                new Vector3(1, 0, -1),
                new Vector3(1, 0, 1),
                new Vector3(-1, 0, 1));

            Geometry geometry = new Geometry(face);
            model.Add(geometry);

            //Console.WriteLine(JsonConvert.SerializeObject(model,
            //                    Newtonsoft.Json.Formatting.Indented));

            model.MakeSketchUpFile(path + @"\PlainQuad.skp");
        }
    }
}
