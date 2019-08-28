using System;
using Newtonsoft.Json;

namespace ExLumina.SketchUp.Factory.Examples
{
    class TexturedQuad : Example
    {
        public TexturedQuad(string display) : base(display)
        {

        }

        public override void Run(string path)
        {
            using (Model model = new Model())
            {
                Face face = new Face(
                    new Ray(-1, 0, -1, false, 0, 0),
                    new Ray(1, 0, -1, false, 1, 0),
                    new Ray(1, 0, 1, false, 1, 1),
                    new Ray(-1, 0, 1, false, 0, 1));

                face.materialName = "Place Holder";

                model.entities.Add(face);


                model.Add(new Material("Place Holder", "PlaceHolderRGBY.png"));

                //Console.WriteLine(JsonConvert.SerializeObject(model,
                //                    Newtonsoft.Json.Formatting.Indented));

                model.WriteSketchUpFile(path + @"\TexturedQuad.skp");
            }
        }
    }
}
