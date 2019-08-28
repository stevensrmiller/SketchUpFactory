using ExLumina.SketchUp.API;

namespace ExLumina.SketchUp.Factory
{
    public interface IEntitiesParent
    {
        SU.EntitiesRef SUEntitiesRef { get; }
    }
}
