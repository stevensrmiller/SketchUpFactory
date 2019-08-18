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
    }
}
