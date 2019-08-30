using System;
using Newtonsoft.Json;

namespace ExLumina.SketchUp.Factory.Examples
{
    class TexturedQuadSynth : Example
    {
        public TexturedQuadSynth(string display) : base(display)
        {

        }

        public override void Run(string path)
        {
            Model model = new Model();

            Face face = new Face(
                new Ray(-1, 0, -1, false, 0, 0),
                new Ray(1, 0, -1, false, 1, 0),
                new Ray(1, 0, 1, false, 1, 1),
                new Ray(-1, 0, 1, false, 0, 1));

            face.materialName = "Synthetic Material";

            model.Entities.Add(face);

            ImageBuffer img = new ImageBuffer(256, 256, 32);

            for (int x = 0; x < 256; ++x)
            {
                for (int y = 0; y < 256; ++y)
                {
                    img[x, y] = new Pixel { Green = (byte)x, Alpha = (byte)y };
                }
            }

            Texture tex = new Texture(img);

            model.Add(new Material("Synthetic Material", tex));

            //Console.WriteLine(JsonConvert.SerializeObject(model,
            //                    Newtonsoft.Json.Formatting.Indented));

            model.WriteSketchUpFile(path + @"\TexturedQuadSynth.skp");
        }
    }
}
