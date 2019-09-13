using ExLumina.SketchUp.Factory;

namespace ExLumina.Examples.SketchUp.Factory
{
    // Create a group with a single quad in it.

    class GroupQuad : Example
    {
        public GroupQuad(string display) : base(display)
        {

        }

        public override void Run(string path)
        {
            Model model = new Model();

            // Create the group, with a name.

            Group group = new Group("I am a group name");

            // Add the group to the model.

            model.Add(group);

            // Define a simple quad.

            Point3[] quadPoints =
            {
                new Point3(-1, 0, -1),
                new Point3(1, 0, -1),
                new Point3(1, 0, 1),
                new Point3(-1, 0, 1)
            };

            // Add the quad to the group.

            group.Add(quadPoints);

            model.WriteSketchUpFile(path + @"\GroupQuad.skp");
        }
    }
}
