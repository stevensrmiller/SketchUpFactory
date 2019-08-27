using System;
using System.Collections.Generic;

namespace ExLumina.SketchUp.Factory
{
    public class Geometry
    {
        public IList<Face> faces;
        public bool isDirty;

        public Geometry()
        {
            faces = new List<Face>();
        }

        public Geometry(params Face[] faces) : this()
        {
            Add(faces);
        }

        public void Add(params Face[] faces)
        {
            IList<Face> faceList = new List<Face>();

            foreach (Face face in faces)
            {
                faceList.Add(face);
            }

            Add(faceList);
        }

        public void Add(IList<Face> faces)
        {
            foreach (Face face in faces)
            {
                this.faces.Add(face);
            }

            isDirty = true;
        }
    }
}
