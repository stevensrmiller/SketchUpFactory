using ExLumina.SketchUp.API;
using System.Collections.Generic;

namespace ExLumina.SketchUp.Factory
{
    /// <summary>
    /// Parent class for Model, CompInst, and Group.
    /// </summary>
    /// <remarks>
    /// In an actual SketchUp model, the model, instances,
    /// and groups all have a reference to an entities set.
    /// Rather thatn use composition here, those three
    /// classes are now subclassed from Entities. This
    /// allows a single set of methods to serve those classes
    /// directly when adding faces, groups, and instances
    /// to any of them. This avoids having to implement all
    /// such methods three times or, alternatively, requiring
    /// client code to dereference an Entities object from
    /// each Model, CompDef, or Group object.</remarks>
    public abstract class Entities
    {
        /// <summary>
        /// "Loose" Faces (not part of any Group or CompInst instance in this set of entities.
        /// </summary>
        public IList<Face> Faces { get; set; } = new List<Face>();

        /// <summary>
        /// All Groups in this set of entities.
        /// </summary>
        public IList<Group> Groups { get; set; } = new List<Group>();

        /// <summary>
        /// All CompInst instances in this set of entities.
        /// </summary>
        public IList<CompInst> Instances { get; set; } =
            new List<CompInst>();

        /// <summary>
        /// Add one or more Group objects (or an array) to this set of entitites.
        /// </summary>
        /// <param name="group"></param>
        public void Add(params Group[] groups)
        {
            foreach (Group group in groups)
            {
                Groups.Add(group);
            }
        }

        /// <summary>
        /// Add one or more Group objects from a list to this set of entitites.
        /// </summary>
        /// <param name="group"></param>
        public void Add(IList<Group> groups)
        {
            foreach (Group group in groups)
            {
                Groups.Add(group);
            }
        }

        /// <summary>
        /// Add one or more CompInst instances (or an array) to this set of entities.
        /// </summary>
        /// <param name="instance"></param>
        public void Add(params CompInst[] instances)
        {
            foreach (CompInst instance in instances)
            {
                Instances.Add(instance);
            }
        }

        /// <summary>
        /// Add one or more CompInst instances from a list to this set of entities.
        /// </summary>
        /// <param name="instance"></param>
        public void Add(IList<CompInst> instances)
        {
            foreach (CompInst instance in instances)
            {
                Instances.Add(instance);
            }
        }

        /// <summary>
        /// Add a new Face from a list of points to this set of entities.
        /// </summary>
        /// <param name="pointList"></param>
        /// <param name="materialName">Optional.</param>
        public void Add(IList<Point3> pointList, string materialName = null)
        {
            Faces.Add(new Face(pointList, materialName));
        }

        /// <summary>
        /// Add a new Face from an array of points to this set of entities.
        /// </summary>
        /// <param name="pointList"></param>
        /// <param name="materialName">Optional.</param>
        public void Add(Point3[] points, string materialName = null)
        {
            IList<Point3> vectorList = new List<Point3>();

            foreach (Point3 vector in points)
            {
                vectorList.Add(vector);
            }

            Add(vectorList, materialName);
        }

        /// <summary>
        /// Add one or more Faces (or an array) to this set of entities.
        /// </summary>
        /// <param name="faces"></param>
        public void Add(params Face[] faces)
        {
            foreach (Face face in faces)
            {
                Faces.Add(face);
            }
        }

        /// <summary>
        /// Add one or more Faces from a list to this set of entities.
        /// </summary>
        /// <param name="faces"></param>
        public void Add(IList<Face> faces)
        {
            foreach (Face face in faces)
            {
                Faces.Add(face);
            }
        }

        /// <summary>
        /// Add one new Face from an array of EdgePoints to this entity.
        /// </summary>
        /// <param name="edgePoints"></param>
        /// <param name="materialName">Optional.</param>
        public void Add(EdgePoint[] edgePoints, string materialName = null)
        {
            Faces.Add(new Face(edgePoints, materialName));
        }

        /// <summary>
        /// Add one new Face from a list of EdgePoints to this entity.
        /// </summary>
        /// <param name="edgePoints"></param>
        /// <param name="materialName"></param>
        public void Add(IList<EdgePoint> edgePoints, string materialName = null)
        {
            Faces.Add(new Face(edgePoints, materialName));
        }

        internal void Pack(Model model, SU.EntitiesRef entitiesRef)
        {
            // Faces

            Face.Pack(model, entitiesRef, Faces);

            // Groups

            foreach (Group group in Groups)
            {
                group.Pack(model, entitiesRef);
            }

            // Component instances

            foreach (CompInst componentInstance in Instances)
            {
                componentInstance.Pack(model, entitiesRef);
            }
        }

        protected void UnpackEntities(SU.EntitiesRef entitiesRef)
        {
            // Get the faces.

            long count;

            SU.EntitiesGetNumFaces(entitiesRef, out count);

            SU.FaceRef[] faceRefs = new SU.FaceRef[count];

            long len = count;

            SU.EntitiesGetFaces(entitiesRef, len, faceRefs, out count);

            foreach (SU.FaceRef faceRef in faceRefs)
            {
                Faces.Add(new Face(faceRef));
            }

            // Get the groups.

            SU.EntitiesGetNumGroups(entitiesRef, out count);

            SU.GroupRef[] groupRefs = new SU.GroupRef[count];

            len = count;

            SU.EntitiesGetGroups(entitiesRef, len, groupRefs, out count);

            foreach (SU.GroupRef groupRef in groupRefs)
            {
                Groups.Add(new Group(groupRef));
            }

            // Get the instances.

            SU.EntitiesGetNumInstances(entitiesRef, out count);

            SU.ComponentInstanceRef[] instanceRefs = new SU.ComponentInstanceRef[count];

            len = count;

            SU.EntitiesGetInstances(entitiesRef, len, instanceRefs, out count);

            foreach (SU.ComponentInstanceRef instanceRef in instanceRefs)
            {
                Instances.Add(new CompInst(instanceRef));
            }
        }
    }
}
