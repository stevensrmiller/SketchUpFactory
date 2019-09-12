using ExLumina.SketchUp.Factory;

namespace ExLumina.Examples.SketchUp.Factory
{
    // Create a component definition with a reference forward to
    // another definition not yet created.

    class ForwardComponent : Example
    {
        public ForwardComponent(string display) : base(display)
        {

        }

        public override void Run(string path)
        {
            Model model = new Model();

            CompDef flat = new CompDef(
                "Flat",
                "A Square");

            Point3[] flatPoints =
            {
                new Point3(-1, 0, -1),
                new Point3(1, 0, -1),
                new Point3(1, 0, 1),
                new Point3(-1, 0, 1)
            };

            flat.Add(flatPoints);

            model.Add(flat);

            CompInst ci2 = new CompInst
            {
                // Refer here to a Component we'll create later.

                ComponentName = "Pointy",
                InstanceName = "Tri the Angle"
            };

            ci2.Transform.Translation.Y = 1;
            ci2.Transform.Rotation.Z = -20;

            flat.Add(ci2);

            CompInst ci = new CompInst
            {
                ComponentName = "Flat",
                InstanceName = "Quad the First"
            };

            model.Add(ci);

            CompDef pointy = new CompDef(
                "Pointy",
                "A Triangle");

            Point3[] pointyPoints =
            {
                new Point3(-1, 0, -1),
                new Point3(1, 0, -1),
                new Point3(0, 0, 1)
            };

            pointy.Add(pointyPoints);
            model.Add(pointy);

            model.WriteSketchUpFile(path + @"\ForwardComponent.skp");
        }
    }
}
