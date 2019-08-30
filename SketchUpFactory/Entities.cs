using System;
using System.Collections.Generic;
using ExLumina.SketchUp.API;

namespace ExLumina.SketchUp.Factory
{
    /// <summary>
    /// Container for use by models, component definitions, and groups.
    /// </summary>
    /// <remarks>
    /// Holds lists of faces, groups, and component instances.
    /// </remarks>
    public class Entities
    {
        public IList<Face> faces;
        public IList<Group> groups;
        public IList<ComponentInstance> componentInstances;

        Model model;

        public Entities(Model model)
        {
            this.model = model;
            faces = new List<Face>();
            groups = new List<Group>();
            componentInstances = new List<ComponentInstance>();
        }

        public Entities(Model model, SU.EntitiesRef suEntitiesRef) : this(model)
        {
            // Get the Faces.

            long count;

            SU.EntitiesGetNumFaces(suEntitiesRef, out count);

            SU.FaceRef[] faceRefs = new SU.FaceRef[count];

            long len = count;

            SU.EntitiesGetFaces(suEntitiesRef, len, faceRefs, out count);

            foreach (SU.FaceRef faceRef in faceRefs)
            {
                faces.Add(new Face(faceRef));
            }

            // Get the groups.

            SU.EntitiesGetNumGroups(suEntitiesRef, out count);

            SU.GroupRef[] groupRefs = new SU.GroupRef[count];

            len = count;

            SU.EntitiesGetGroups(suEntitiesRef, len, groupRefs, out count);

            foreach (SU.GroupRef groupRef in groupRefs)
            {
                groups.Add(new Group(model, this, groupRef));
            }
        }

        public void Add(Group group)
        {
            groups.Add(group);
        }

        public void Add(IList<Vector3> vectorList)
        {
            faces.Add(new Face(vectorList));
        }

        public void Add(params Vector3[] vectors)
        {
            IList<Vector3> vectorList = new List<Vector3>();
            
            foreach(Vector3 vector in vectors)
            {
                vectorList.Add(vector);
            }

            Add(vectorList);
        }

        public void Add(ComponentInstance instance)
        {
            componentInstances.Add(instance);
        }

        public void Add(params Face[] faces)
        {
            foreach (Face face in faces)
            {
                this.faces.Add(face);
            }
        }

        public void Add(IList<Face> faces)
        {
            foreach (Face face in faces)
            {
                this.faces.Add(face);
            }
        }

        public void Add(params Ray[] rays)
        {
            faces.Add(new Face(rays));
        }

        public void Add(IList<Ray> rays)
        {
            faces.Add(new Face(rays));
        }

        public void Pack(SU.EntitiesRef suEntitiesRef)
        {
            // Faces

            Face.Pack(model, suEntitiesRef, faces);

            // Groups

            foreach (Group group in groups)
            {
                group.Pack(suEntitiesRef);
            }

            // Component instances

            foreach (ComponentInstance componentInstance in componentInstances)
            {
                componentInstance.Pack(model, suEntitiesRef);
            }
        }
    }
}
