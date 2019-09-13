using ExLumina.SketchUp.Factory;

namespace ExLumina.Examples.SketchUp.Factory
{
    // Create a model with material, and a quad textured with
    // that material. The material is created from a file that
    // must be in the start-up directory.

    class TexturedQuad : Example
    {
        public TexturedQuad(string display) : base(display)
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

            Face face = new Face(facePoints);

            face.MaterialName = "Place Holder";

            model.Add(face);

            model.Add(new Material("Place Holder", new Texture("PlaceHolderRGBY.png")));

            model.WriteSketchUpFile(path + @"\TexturedQuad.skp");
        }
    }
}
