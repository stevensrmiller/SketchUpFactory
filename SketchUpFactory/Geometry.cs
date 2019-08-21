using System;
using System.Collections.Generic;

namespace ExLumina.SketchUp.Factory
{
    public class Geometry
    {
        public IList<Face> faces;

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
            foreach (Face face in faces)
            {
                this.faces.Add(face);
            }
        }

        public void Add(IList<Face> faces)
        {
            foreach (Face face in faces)
            {
                this.faces.Add(face);
            }
        }
    }
}
