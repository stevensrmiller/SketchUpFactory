using ExLumina.SketchUp.API;
using System;

namespace ExLumina.SketchUp.Factory
{
    public partial class Model
    {
        public void ReadFaces(SU.EntitiesRef entitiesRef)
        {
            // Get the faces.

            long numFaces;
            SU.EntitiesGetNumFaces(entitiesRef, out numFaces);

            SU.FaceRef[] faceRefs = new SU.FaceRef[numFaces];
            long numFacesRetrieved;
            SU.EntitiesGetFaces(entitiesRef, numFaces, faceRefs, out numFacesRetrieved);

            // Add each Face to the Geometry.

            for (int face = 0; face < numFaces; ++face)
            {
                //entities.Add(new Face(faceRefs[face]));
            }
        }
    }
}
