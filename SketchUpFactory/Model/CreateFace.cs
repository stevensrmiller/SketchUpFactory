using ExLumina.SketchUp.API;
using System;

namespace ExLumina.SketchUp.Factory
{
    public partial class Model
    {
        public void CreateFace(
            Face face,
            SU.GeometryInputRef geometryInputRef,
            ref int vertexIndex)
        {
            const int maxUVcoords = 4;
            SU.LoopInputRef loopInputRef = new SU.LoopInputRef();
            SU.LoopInputCreate(loopInputRef);
            Vector2[] uvCoords = new Vector2[maxUVcoords];
            int[] indices = new int[maxUVcoords];
            int numUVcoords = 0;

            int edgeIndex = 0; // There are other ways, but this is so simple.

            foreach (Ray ray in face.outerLoop.rays)
            {
                SU.GeometryInputAddVertex(geometryInputRef, ray.vertex.SUPoint3D);
                SU.LoopInputAddVertexIndex(loopInputRef, vertexIndex);

                if (ray.isSmooth)
                {
                    SU.LoopInputEdgeSetSmooth(loopInputRef, edgeIndex, true);
                    SU.LoopInputEdgeSetSoft(loopInputRef, edgeIndex, true);
                }

                if (ray.uvCoords != null && numUVcoords < maxUVcoords)
                {
                    uvCoords[numUVcoords] = ray.uvCoords;
                    indices[numUVcoords] = vertexIndex;

                    numUVcoords = numUVcoords + 1;
                }

                vertexIndex = vertexIndex + 1;
                edgeIndex = edgeIndex + 1;
            }

            long faceIndex;

            try
            {
                SU.GeometryInputAddFace(geometryInputRef, loopInputRef, out faceIndex);
            }
            catch
            {
                // Success would have deallocated this for us.

                SU.LoopInputRelease(loopInputRef);
                throw;
            }

            foreach (Loop loop in face.innerLoops)
            {
                loopInputRef = new SU.LoopInputRef();
                SU.LoopInputCreate(loopInputRef);

                foreach (Ray ray in loop.rays)
                {
                    SU.GeometryInputAddVertex(geometryInputRef, ray.vertex.SUPoint3D);
                    SU.LoopInputAddVertexIndex(loopInputRef, vertexIndex);

                    vertexIndex = vertexIndex + 1;
                }

                try
                {
                    SU.GeometryInputFaceAddInnerLoop(
                        geometryInputRef,
                        faceIndex,
                        loopInputRef);
                }
                catch
                {
                    // Success would have deallocated this for us.

                    SU.LoopInputRelease(loopInputRef);
                    throw;
                }
            }

            if (face.materialName != null)
            {
                Material material = null;

                try
                {
                    material = materialsLib[face.materialName];
                } catch (Exception e)
                {
                    string msg = "\nCould not find a material named " + face.materialName;
                    throw new Exception(e.Message + msg);
                }

                SU.MaterialInput materialInput = new SU.MaterialInput
                {
                    numUVCoords = numUVcoords
                };

                for (int i = 0; i < numUVcoords; ++i)
                {
                    materialInput.UVCoords[i].x = uvCoords[i].x;
                    materialInput.UVCoords[i].y = uvCoords[i].y;
                    materialInput.vertexIndices[i] = indices[i];
                }

                materialInput.materialRef = material.materialRef;

                SU.GeometryInputFaceSetFrontMaterial(
                    geometryInputRef,
                    faceIndex,
                    materialInput);
            }
        }
    }
}
