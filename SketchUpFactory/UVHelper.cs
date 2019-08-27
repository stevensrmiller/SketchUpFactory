using ExLumina.SketchUp.API;

namespace ExLumina.SketchUp.Factory
{
    public class UVHelper
    {
        SU.UVHelperRef uvHelperRef;

        public UVHelper(SU.FaceRef faceRef)
        {
            SU.TextureWriterRef textureWriterRef = new SU.TextureWriterRef();

            uvHelperRef = new SU.UVHelperRef();

            SU.FaceGetUVHelper(
                faceRef,
                true,
                false,
                textureWriterRef,
                uvHelperRef);
        }

        public void Assign(RayList rayList)
        {
            foreach (Ray ray in rayList.Rays)
            {
                ray.uvCoords = Coords(ray.vertex);
            }
        }

        public Vector2 Coords(Vector3 v)
        {
            SU.Point3D point = new SU.Point3D(v.x, v.y, v.z);

            SU.UVQ uvq = new SU.UVQ();
            
            SU.UVHelperGetFrontUVQ(uvHelperRef, point, out uvq);

            return new Vector2(uvq.u / uvq.q, uvq.v / uvq.q);
        }
    }
}
