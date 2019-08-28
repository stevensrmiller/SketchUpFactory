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

        IEntitiesParent parent;

        public Entities(IEntitiesParent parent)
        {
            this.parent = parent;
            faces = new List<Face>();
            groups = new List<Group>();
            componentInstances = new List<ComponentInstance>();
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

        public void SULoad(Model model)
        {
            SU.EntitiesRef entitiesRef = parent.SUEntitiesRef;

            // Faces

            Face.SULoad(model, entitiesRef, faces);

            // Groups

            foreach (Group group in groups)
            {
                group.SULoad(model, entitiesRef);
            }

            // Component instances

            foreach (ComponentInstance componentInstance in componentInstances)
            {
                componentInstance.SULoad(model, entitiesRef);
            }
        }
    }
}
