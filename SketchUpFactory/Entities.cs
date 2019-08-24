using System.Collections.Generic;

namespace ExLumina.SketchUp.Factory
{
    /// <summary>
    /// Container for use by models, component definitions, and groups.
    /// </summary>
    /// <remarks>
    /// Holds lists of geometries, groups, and component instances.
    /// </remarks>
    public abstract class Entities
    {
        public string name;
        public string description;
        public IList<Geometry> geometries;
        public IList<Group> groups;
        public IList<ComponentInstance> componentInstances;

        internal Geometry currentGeometry;
        internal bool currentGeometryIsDirty;

        public Entities()
        {
            geometries = new List<Geometry>();
            groups = new List<Group>();
            componentInstances = new List<ComponentInstance>();

            Separate();
        }

        public Entities(string name, string description) : this()
        {
            this.name = name;
            this.description = description;
        }

        public void Add(IList<Vector3> vectorList)
        {
            currentGeometry.Add(new Face(vectorList));

            currentGeometryIsDirty = true;
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

        public void Separate()
        {
            if (currentGeometryIsDirty)
            {
                geometries.Add(currentGeometry);
            }

            currentGeometry = new Geometry();

            currentGeometryIsDirty = false;
        }
    }
}
