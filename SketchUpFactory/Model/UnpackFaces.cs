using System;
using ExLumina.SketchUp.API;

namespace ExLumina.SketchUp.Factory
{
    public partial class Model
    {
        public void UnpackFaces(SU.EntitiesRef entitiesRef)
        {
            // Get the faces.

            long numFaces;
            SU.EntitiesGetNumFaces(entitiesRef, out numFaces);

            SU.FaceRef[] faceRefs = new SU.FaceRef[numFaces];
            long numFacesRetrieved;
            SU.EntitiesGetFaces(entitiesRef, numFaces, faceRefs, out numFacesRetrieved);

            // Add each Face to the Geometry.

            Console.WriteLine("# FACES = {0}", numFacesRetrieved);

            for (int face = 0; face < numFaces; ++face)
            {
                Entities.Add(new Face(faceRefs[face]));
            }
        }
    }
}
