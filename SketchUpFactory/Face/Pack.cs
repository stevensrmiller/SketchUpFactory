using ExLumina.SketchUp.API;
using System;

namespace ExLumina.SketchUp.Factory
{
    public partial class Face
    {
        void Pack(
            Model model,
            SU.GeometryInputRef geometryInputRef,
            ref int vertexIndex)
        {
            const int maxUVcoords = 4;
            SU.LoopInputRef loopInputRef = new SU.LoopInputRef();
            SU.LoopInputCreate(loopInputRef);
            Point2[] uvCoords = new Point2[maxUVcoords];
            int[] indices = new int[maxUVcoords];
            int numUVcoords = 0;

            // There are other ways to count foreach iterations,
            // but this is so simple.

            int edgeIndex = 0;

            foreach (EdgePoint edgePoint in outerLoop)
            {
                SU.GeometryInputAddVertex(geometryInputRef, edgePoint.Vertex.SUPoint3D);
                SU.LoopInputAddVertexIndex(loopInputRef, vertexIndex);

                if (edgePoint.IsSmooth)
                {
                    SU.LoopInputEdgeSetSmooth(loopInputRef, edgeIndex, true);
                    SU.LoopInputEdgeSetSoft(loopInputRef, edgeIndex, true);
                }

                if (edgePoint.UVCoords != null && numUVcoords < maxUVcoords)
                {
                    uvCoords[numUVcoords] = edgePoint.UVCoords;
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

            foreach (Loop loop in innerLoops)
            {
                loopInputRef = new SU.LoopInputRef();
                SU.LoopInputCreate(loopInputRef);

                foreach (EdgePoint edgePoint in loop.edgePoints)
                {
                    SU.GeometryInputAddVertex(geometryInputRef, edgePoint.Vertex.SUPoint3D);
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

            if (MaterialName != null)
            {
                Material material = null;

                try
                {
                    material = model.materials[MaterialName];
                }
                catch (Exception e)
                {
                    string msg = "\nCould not find a material named " + MaterialName;
                    throw new Exception(e.Message + msg);
                }

                SU.MaterialInput materialInput = new SU.MaterialInput
                {
                    numUVCoords = numUVcoords
                };

                for (int i = 0; i < numUVcoords; ++i)
                {
                    materialInput.UVCoords[i].x = uvCoords[i].X;
                    materialInput.UVCoords[i].y = uvCoords[i].Y;
                    materialInput.vertexIndices[i] = indices[i];
                }

                materialInput.materialRef = material.suMaterialRef;

                SU.GeometryInputFaceSetFrontMaterial(
                    geometryInputRef,
                    faceIndex,
                    materialInput);
            }
        }
    }
}
