using ExLumina.SketchUp.Factory;

namespace ExLumina.Examples.SketchUp.Factory
{
    // Create a single plain quad.

    class PlainQuad : Example
    {
        public PlainQuad(string display) : base(display)
        {

        }

        public override void Run(string path)
        {
            Model model = new Model();

            // Create an array of 3D points defining a quad

            Point3[] quadPoints =
            {
                new Point3(-1, 0, -1),
                new Point3(1, 0, -1),
                new Point3(1, 0, 1),
                new Point3(-1, 0, 1)
            };

            // Add the quad to the model.

            model.Add(quadPoints);

            model.WriteSketchUpFile(path + @"\PlainQuad.skp");
        }
    }
}
