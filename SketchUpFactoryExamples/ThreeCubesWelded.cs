using ExLumina.SketchUp.Factory;
using System.Collections.Generic;

namespace ExLumina.Examples.SketchUp.Factory
{
    // Create three cubes of six quads each, and explicitly
    // ask SketchUp to weld their common vertexes.

    class ThreeCubesWelded : Example
    {
        public ThreeCubesWelded(string display) : base(display)
        {

        }

        public override void Run(string path)
        {
            const int perFace = 4;

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

            for (int f = 0; f < coords.Length / (perFace * 3); ++f)
            {
                IList<Point3> corners = new List<Point3>();

                for (int c = 0; c < perFace; ++c)
                {
                    int offset = (f * perFace + c) * 3;

                    corners.Add(new Point3(coords[offset] + 2,
                                            coords[offset + 1],
                                            coords[offset + 2]));
                }

                model.Add(corners);
            }

            for (int f = 0; f < coords.Length / (perFace * 3); ++f)
            {
                IList<Point3> corners = new List<Point3>();

                for (int c = 0; c < perFace; ++c)
                {
                    int offset = (f * perFace + c) * 3;

                    corners.Add(new Point3(coords[offset],
                                            coords[offset + 1] + 2,
                                            coords[offset + 2]));
                }

                model.Add(corners);
            }

            for (int f = 0; f < coords.Length / (perFace * 3); ++f)
            {
                IList<Point3> corners = new List<Point3>();

                for (int c = 0; c < perFace; ++c)
                {
                    int offset = (f * perFace + c) * 3;

                    corners.Add(new Point3(coords[offset],
                                            coords[offset + 1],
                                            coords[offset + 2] + 2));
                }

                model.Add(corners);
            }

            model.WriteSketchUpFile(path + @"\ThreeCubesWelded.skp");
        }
    }
}
