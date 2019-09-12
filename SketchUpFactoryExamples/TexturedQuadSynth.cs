using ExLumina.SketchUp.Factory;

namespace ExLumina.Examples.SketchUp.Factory
{
    // Create a single textured quad that uses a material
    // creted at run-time.

    class TexturedQuadSynth : Example
    {
        public TexturedQuadSynth(string display) : base(display)
        {

        }

        public override void Run(string path)
        {
            Model model = new Model();

            EdgePoint[] facePoints =
            {
                new EdgePoint(-1, 0, -1, false, 0, 0),
                new EdgePoint(1, 0, -1, false, 1, 0),
                new EdgePoint(1, 0, 1, false, 1, 1),
                new EdgePoint(-1, 0, 1, false, 0, 1)
            };

            Face face = new Face(facePoints, "Synthetic Material");

            model.Add(face);

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

            model.WriteSketchUpFile(path + @"\TexturedQuadSynth.skp");
        }
    }
}
