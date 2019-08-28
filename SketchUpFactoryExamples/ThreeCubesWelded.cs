using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ExLumina.SketchUp.Factory.Examples
{
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

            using (Model model = new Model())
            {
                for (int f = 0; f < coords.Length / (perFace * 3); ++f)
                {
                    IList<Vector3> corners = new List<Vector3>();

                    for (int c = 0; c < perFace; ++c)
                    {
                        int offset = (f * perFace + c) * 3;

                        corners.Add(new Vector3(coords[offset] + 2,
                                                coords[offset + 1],
                                                coords[offset + 2]));
                    }

                    model.entities.Add(corners);
                }

                for (int f = 0; f < coords.Length / (perFace * 3); ++f)
                {
                    IList<Vector3> corners = new List<Vector3>();

                    for (int c = 0; c < perFace; ++c)
                    {
                        int offset = (f * perFace + c) * 3;

                        corners.Add(new Vector3(coords[offset],
                                                coords[offset + 1] + 2,
                                                coords[offset + 2]));
                    }

                    model.entities.Add(corners);
                }

                for (int f = 0; f < coords.Length / (perFace * 3); ++f)
                {
                    IList<Vector3> corners = new List<Vector3>();

                    for (int c = 0; c < perFace; ++c)
                    {
                        int offset = (f * perFace + c) * 3;

                        corners.Add(new Vector3(coords[offset],
                                                coords[offset + 1],
                                                coords[offset + 2] + 2));
                    }

                    model.entities.Add(corners);
                }

                model.WriteSketchUpFile(path + @"\ThreeCubesWelded.skp");
            }
        }
    }
}
