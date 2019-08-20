using ExLumina.SketchUp.API;

namespace ExLumina.SketchUp.Factory
{
    public static partial class Factory
    {
        static void CreateGeometry(Geometry geometry, SU.EntitiesRef entitiesRef)
        {
            SU.GeometryInputRef geometryInputRef = new SU.GeometryInputRef();
            SU.GeometryInputCreate(geometryInputRef);

            int vertexIndex = 0;

            foreach (Face face in geometry.faces)
            {
                CreateFace(face, geometryInputRef,ref vertexIndex);
            }

            SU.EntitiesFill(entitiesRef, geometryInputRef, true);
            SU.GeometryInputRelease(geometryInputRef);
        }
    }
}
