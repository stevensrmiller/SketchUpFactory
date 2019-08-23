using System;

namespace ExLumina.SketchUp.Factory
{
    public class Group : Entities, IDisposable
    {
        public Transform transform;

        public Group() : this(null)
        {

        }

        public Group(Entities parent, string name, string description)
            : base(name, description)
        {
            transform = new Transform();

            parent?.groups.Add(this);
        }

        public Group(Entities parent) 
            : this(parent, "<Group name unset>", "<Group description unset>")
        {

        }

        public void Dispose()
        {
            Separate();
        }
    }
}
