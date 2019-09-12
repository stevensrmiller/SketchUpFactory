using ExLumina.SketchUp.API;

namespace ExLumina.SketchUp.Factory
{
    internal class UVHelper
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

        public void Assign(EdgePointList rayList)
        {
            foreach (EdgePoint ray in rayList.EdgePoints)
            {
                ray.UVCoords = Coords(ray.Vertex);
            }
        }

        public Point2 Coords(Point3 v)
        {
            SU.Point3D point = new SU.Point3D(v.X, v.Y, v.Z);

            SU.UVQ uvq = new SU.UVQ();
            
            SU.UVHelperGetFrontUVQ(uvHelperRef, point, out uvq);

            return new Point2(uvq.u / uvq.q, uvq.v / uvq.q);
        }
    }
}
