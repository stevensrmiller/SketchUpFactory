using ExLumina.SketchUp.Factory;

namespace ExLumina.Examples.SketchUp.Factory
{
    // Create a single textured quad that uses a material
    // creted at run-time.

    class TexturedQuadSynth : Example
    {
        const string materialName = "Synthetic Material";

        public TexturedQuadSynth(string display) : base(display)
        {

        }

        public override void Run(string path)
        {
            Model model = new Model();

            // Define a quad with EdgePoints. These accept spatial coordinates,
            // a "soft edge" flag for shading, and UV coordinates in texture 
            // space.

            EdgePoint[] edgePoints =
            {
                new EdgePoint(-1, 0, -1, false, 0, 0),
                new EdgePoint(1, 0, -1, false, 1, 0),
                new EdgePoint(1, 0, 1, false, 1, 1),
                new EdgePoint(-1, 0, 1, false, 0, 1)
            };

            // Add the quad to the model, with a name we will use for our material.

            model.Add(edgePoints, materialName);

            // Create an ImageBuffer and fill it with pixels.

            ImageBuffer img = new ImageBuffer(256, 256, 32);

            for (int x = 0; x < 256; ++x)
            {
                for (int y = 0; y < 256; ++y)
                {
                    // Create a green, transparent gradient in two directions.

                    img[x, y] = new Pixel { Green = (byte)x, Alpha = (byte)y };
                }
            }

            // Create a texture from the image, and material from that texture,
            // and add the material to the model.

            model.Add(new Material(materialName, new Texture(img)));

            model.WriteSketchUpFile(path + @"\TexturedQuadSynth.skp");
        }
    }
}
