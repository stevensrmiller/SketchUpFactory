using System;

namespace ExLumina.SketchUp.Factory
{
    public class Group
    {
        public string name;
        public Transform transform;
        public Entities entities;

        public Group() : this(null)
        {

        }

        public Group(Entities parent, string name)
        {
            this.name = name;
            transform = new Transform();
            entities = new Entities();
            parent?.groups.Add(this);
        }

        public Group(Entities parent) 
            : this(parent, "<Group name unset>")
        {

        }
    }
}
