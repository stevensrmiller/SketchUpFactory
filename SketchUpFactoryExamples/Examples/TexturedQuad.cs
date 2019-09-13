using ExLumina.SketchUp.Factory;

namespace ExLumina.Examples.SketchUp.Factory
{
    // Create a model with material, and a quad textured with
    // that material. The material is created from a file that
    // must be in the start-up directory.

    class TexturedQuad : Example
    {
        const string materialName = "2x2 Texture";

        public TexturedQuad(string display) : base(display)
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

            // Add the quad to the model, along with the name we will assign to
            // the material used to provide its texture.

            model.Add(edgePoints, materialName);

            // Create a texture from a file and add it to the model.

            model.Add(new Material(materialName, new Texture("PlaceHolderRGBY.png")));

            model.WriteSketchUpFile(path + @"\TexturedQuad.skp");
        }
    }
}
