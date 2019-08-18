using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ExLumina.SketchUp.API;

namespace ExLumina.SketchUp.Factory
{
    public static class Factory
    {
        static Dictionary<string, Material> materialsLib =
            new Dictionary<string, Material>();

        public static void MakeSketchUpFile(Model model, string path)
        {
            // Init and open a model structure.

            SU.Initialize();
            SU.ModelRef modelRef = new SU.ModelRef();
            SU.ModelCreate(modelRef);
            SU.EntitiesRef entitiesRef = new SU.EntitiesRef();
            SU.ModelGetEntities(modelRef, entitiesRef);

            // Create the materials.

            foreach (Material material in model.materials)
            {
                CreateMaterial(material, modelRef);
            }

            // Create the geometries.

            foreach (Geometry geometry in model.geometries)
            {
                CreateGeometry(geometry, entitiesRef);
            }

            // Set style and camera, write the file, terminate.

            SU.StylesRef stylesRef = new SU.StylesRef();
            SU.ModelGetStyles(modelRef, stylesRef);
            SU.StylesAddStyle(stylesRef, "base.style", true);

            SU.CameraRef cameraRef = new SU.CameraRef();

            SU.ModelGetCamera(modelRef, cameraRef);

            SU.CameraSetOrientation(
                cameraRef,
                new SU.Point3D(
                     10,// * SU.MetersToInches,
                    -10,// * SU.MetersToInches,
                     10),// * SU.MetersToInches),
                new SU.Point3D(0, 0, 0),
                new SU.Vector3D(0, 0, 1));
            
            SU.ModelSaveToFileWithVersion(modelRef, path, SU.ModelVersion_SU2017);

            // Might need to filter out those in use by a face.

            foreach (Material material in materialsLib.Values)
            {
                if (!material.isInUse)
                {
                    SU.MaterialRelease(material.materialRef);
                }
            }

            SU.ModelRelease(modelRef);

            SU.Terminate();
        }

        static void CreateMaterial(Material material, SU.ModelRef modelRef)
        {
            SU.MaterialRef materialRef = new SU.MaterialRef();
            SU.MaterialCreate(materialRef);
            SU.MaterialSetName(materialRef, material.name);
            SU.MaterialSetColor(materialRef, material.color.SUColor);

            if (material.texture != null)
            {
                SU.TextureRef textureRef = new SU.TextureRef();
                SU.TextureCreateFromFile(
                    textureRef,
                    material.texture.filename,
                    SU.MetersToInches,
                    SU.MetersToInches);
                SU.MaterialSetTexture(materialRef, textureRef);
            }

            SU.MaterialRef[] mats = new SU.MaterialRef[1];
            mats[0] = materialRef;
            SU.ModelAddMaterials(modelRef, 1, mats);

            material.materialRef = materialRef;
            materialsLib.Add(material.name, material);
            material.isInUse = true;
        }

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

        static void CreateFace(
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

            foreach (Ray ray in face.outerLoop.rays)
            {
                SU.GeometryInputAddVertex(geometryInputRef, ray.vertex.SUPoint3D);
                SU.LoopInputAddVertexIndex(loopInputRef, vertexIndex);

                if (ray.uvCoords != null && numUVcoords < maxUVcoords)
                {
                    uvCoords[numUVcoords] = ray.uvCoords;
                    indices[numUVcoords] = vertexIndex;

                    numUVcoords = numUVcoords + 1;
                }

                vertexIndex = vertexIndex + 1;
            }

            int faceIndex;

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
                SU.MaterialRef materialRef = null;

                try
                {
                    materialRef = materialsLib[face.materialName].materialRef;
                } catch (Exception e)
                {
                    string msg = "\nCould not find a material named " + face.materialName;
                    MessageBox.Show(e.Message + msg, "SketchUpFactory",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                SU.MaterialInput materialInput = new SU.MaterialInput();

                materialInput.numUVCoords = numUVcoords;

                for (int i = 0; i < numUVcoords; ++i)
                {
                    materialInput.UVCoords[i].x = uvCoords[i].x;
                    materialInput.UVCoords[i].y = uvCoords[i].y;
                    materialInput.vertexIndices[i] = indices[i];
                }

                materialInput.materialRef = materialRef;

                SU.GeometryInputFaceSetFrontMaterial(
                    geometryInputRef,
                    faceIndex,
                    materialInput);
            }
        }
    }
}
