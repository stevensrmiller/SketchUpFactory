using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExLumina.SketchUp.API;
using System.Windows.Forms;
using ExLumina.SketchUp.Factory;

namespace SketchUp.Factory.Reader
{
    static class Reader
    {
        public static Model Read(string fileName)
        {
            SU.Initialize();

            SU.ModelRef modelRef = new SU.ModelRef();
            SU.ModelCreateFromFile(modelRef, fileName);

            SU.EntitiesRef entitiesRef = new SU.EntitiesRef();
            SU.ModelGetEntities(modelRef, entitiesRef);

            long numFaces;
            SU.EntitiesGetNumFaces(entitiesRef, out numFaces);

            MessageBox.Show($"{numFaces} faces.", "Face Count");

            SU.FaceRef[] faceRefs = new SU.FaceRef[numFaces];
            long numFacesRetrieved;
            SU.EntitiesGetFaces(entitiesRef, numFaces, faceRefs, out numFacesRetrieved);

            SU.ModelRelease(modelRef);
            SU.Terminate();

            return null;
        }
    }
}
