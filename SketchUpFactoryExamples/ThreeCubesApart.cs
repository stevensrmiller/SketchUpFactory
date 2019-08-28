using System.Collections.Generic;

namespace ExLumina.SketchUp.Factory.Examples
{
    class ThreeCubesApart : Example
    {
        public ThreeCubesApart(string display) : base(display)
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
                Entities entities = model.entities;


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

                    entities.Add(corners);
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

                    entities.Add(corners);
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

                    entities.Add(corners);
                }


                model.WriteSketchUpFile(path + @"\ThreeCubesApart.skp");
            }
        }
    }
}
