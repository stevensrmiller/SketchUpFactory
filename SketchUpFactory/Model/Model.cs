using ExLumina.SketchUp.API;
using System.Collections.Generic;

namespace ExLumina.SketchUp.Factory
{
    /// <summary>
    /// The root of the document object model.
    /// </summary>
    /// <remarks>
    /// All faces, definitions, instances, and textures must
    /// be part of one Model object in order to write it to
    /// a file.
    /// </remarks>
    public partial class Model : Entities
    {
        /// <summary>
        /// An internal name. Need not be the filename.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// An internal description.
        /// </summary>
        public string Description { get; set; }

        internal IDictionary<string, CompDef> components =
            new Dictionary<string, CompDef>();

        internal IDictionary<string, Material> materials =
            new Dictionary<string, Material>();

        /// <summary>
        /// An empty model with a default name and description.
        /// </summary>
        public Model() : this("<model name unset>", "<model description unset>")
        {

        }

        /// <summary>
        /// An empty model.
        /// </summary>
        /// <remarks>
        /// Note that neither the model's Name nor Description must
        /// match the filename if you later write the Model to a
        /// file.
        /// </remarks>
        /// <param name="name">The internal name of the model.</param>
        /// <param name="description">An internal description of the model.</param>
        public Model(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }

        /// <summary>
        /// Construct a Model from a SketchUp file.
        /// </summary>
        /// <param name="path">SketchUp file path.</param>
        public Model(string path)
        {
            SU.Initialize();

            SU.ModelRef modelRef = new SU.ModelRef();

            SU.ModelCreateFromFile(modelRef, path);

            UnpackMaterials(modelRef);

            UnpackComponents(modelRef);

            SU.EntitiesRef entitiesRef = new SU.EntitiesRef();

            SU.ModelGetEntities(modelRef, entitiesRef);

            UnpackEntities(entitiesRef);

            SU.ModelRelease(modelRef);

            SU.Terminate();
        }

        /// <summary>
        /// Include a Material in a Model.
        /// </summary>
        /// <param name="material"></param>
        public void Add(Material material)
        {
            materials.Add(
                material.Name,
                material);
        }

        /// <summary>
        /// Include a ComponentDefinition in a Model.
        /// </summary>
        /// <param name="componentDefinition"></param>
        public void Add(CompDef componentDefinition)
        {
            components.Add(
                componentDefinition.Name,
                componentDefinition);
        }
    }
}
