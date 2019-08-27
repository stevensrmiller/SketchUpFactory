using System;
using System.Collections.Generic;

namespace ExLumina.SketchUp.Factory
{
    /// <summary>
    /// Container for use by models, component definitions, and groups.
    /// </summary>
    /// <remarks>
    /// Holds lists of geometries, groups, and component instances.
    /// </remarks>
    public class Entities : IDisposable
    {
        public IList<Geometry> geometries;
        public IList<Group> groups;
        public IList<ComponentInstance> componentInstances;

        internal Geometry currentGeometry;

        public Entities()
        {
            geometries = new List<Geometry>();
            groups = new List<Group>();
            componentInstances = new List<ComponentInstance>();
            currentGeometry = new Geometry();
        }

        public void Add(IList<Vector3> vectorList)
        {
            currentGeometry.Add(new Face(vectorList));
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

        public void Add(Geometry geometry)
        {
            geometries.Add(geometry);
        }

        public void Add(ComponentInstance instance)
        {
            componentInstances.Add(instance);
        }

        public void Add(params Face[] faces)
        {
            currentGeometry.Add(faces);
        }

        public void Add(IList<Face> faces)
        {
            currentGeometry.Add(faces);
        }

        public void Add(Ray[] rays)
        {
            currentGeometry.Add(new Face(rays));
        }

        public void Add(IList<Ray> rays)
        {
            currentGeometry.Add(new Face(rays));
        }

        public void Dispose()
        {
            if (currentGeometry.isDirty)
            {
                geometries.Add(currentGeometry);
            }

            currentGeometry = new Geometry();
        }
    }
}
