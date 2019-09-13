using ExLumina.SketchUp.Factory;

namespace ExLumina.Examples.SketchUp.Factory
{
    // Create a group with a quad in it.

    class GroupQuad : Example
    {
        public GroupQuad(string display) : base(display)
        {

        }

        public override void Run(string path)
        {
            Model model = new Model();

            Group group = new Group("I am a group name");

            model.Add(group);

            Point3[] quadPoints =
            {
                new Point3(-1, 0, -1),
                new Point3(1, 0, -1),
                new Point3(1, 0, 1),
                new Point3(-1, 0, 1)
            };

            group.Add(quadPoints);

            model.WriteSketchUpFile(path + @"\GroupQuad.skp");
        }
    }
}
