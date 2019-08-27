using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ExLumina.SketchUp.Factory.Examples
{
    class PlainCube : Example
    {
        public PlainCube(string display) : base(display)
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

            using (Entities entities = model.entities)
            {
                Geometry geometry = new Geometry();

                for (int f = 0; f < coords.Length / (perFace * 3); ++f)
                {
                    IList<Vector3> corners = new List<Vector3>();

                    for (int c = 0; c < perFace; ++c)
                    {
                        int offset = (f * perFace + c) * 3;

                        corners.Add(new Vector3(coords[offset],
                                                coords[offset + 1],
                                                coords[offset + 2]));
                    }

                    Face face = new Face(corners);

                    geometry.Add(face);
                }

                entities.Add(geometry);
            }

            model.WriteSketchUpFile(path + @"\PlainCube.skp");
        }
    }
}
