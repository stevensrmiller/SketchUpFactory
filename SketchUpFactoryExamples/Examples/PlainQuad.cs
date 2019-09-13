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

            Point3[] quadPoints =
            {
                new Point3(-1, 0, -1),
                new Point3(1, 0, -1),
                new Point3(1, 0, 1),
                new Point3(-1, 0, 1)
            };

            model.Add(quadPoints);

            model.WriteSketchUpFile(path + @"\PlainQuad.skp");
        }
    }
}
