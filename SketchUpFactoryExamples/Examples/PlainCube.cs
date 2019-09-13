using ExLumina.SketchUp.Factory;
using System.Collections.Generic;

namespace ExLumina.Examples.SketchUp.Factory
{
    // Create a plain cube made of six quads.

    class PlainCube : Example
    {
        public PlainCube(string display) : base(display)
        {

        }

        public override void Run(string path)
        {
            const int perFace = 4;

            // Define all of the coordinates needed for six faces
            // of a cube.

            int[] coords =
            {
                1, -1, 1,
                -1, -1, 1,
                -1, -1, -1,
                1, -1, -1,

                1, -1, 1,
                1, 1, 1,
                -1, 1, 1,
                -1, -1, 1,

                1, 1, -1,
                1, 1, 1,
                1, -1, 1,
                1, -1, -1,

                1, -1, -1,
                -1, -1, -1,
                -1, 1, -1,
                1, 1, -1,

                -1, 1, -1,
                -1, -1, -1,
                -1, -1, 1,
                -1, 1, 1,

                1, 1, -1,
                -1, 1, -1,
                -1, 1, 1,
                1, 1, 1
            };

            Model model = new Model();

            // Convert the coordinates to sets that each define a quad.

            for (int f = 0; f < coords.Length / (perFace * 3); ++f)
            {
                IList<Point3> corners = new List<Point3>();

                // Extract four coordinates at a time into a list.

                for (int c = 0; c < perFace; ++c)
                {
                    int offset = (f * perFace + c) * 3;

                    corners.Add(new Point3(coords[offset],
                                            coords[offset + 1],
                                            coords[offset + 2]));
                }

                // Add another quad to the model.

                model.Add(corners);
            }

            model.WriteSketchUpFile(path + @"\PlainCube.skp");
        }
    }
}
