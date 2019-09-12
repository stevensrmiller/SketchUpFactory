using ExLumina.SketchUp.Factory;

namespace ExLumina.Examples.SketchUp.Factory
{
    // Create a hierarchy of nested components, three
    // plies deep.

    class ThreePlyTree : Example
    {
        public ThreePlyTree(string display) : base(display)
        {

        }

        public override void Run(string path)
        {
            CompInst ci;

            Model model = new Model();

            // Define the root level.

            CompDef def = new CompDef(
                "Flat",
                "A Square");

            model.Add(def);

            Point3[] flatPoints =
            {
                new Point3(-1, 0, -1),
                new Point3(1, 0, -1),
                new Point3(1, 0, 1),
                new Point3(-1, 0, 1)
            };

            def.Add(flatPoints);

            // Two branches.

            ci = new CompInst
            {
                ComponentName = "Pointy",
                InstanceName = "On the right."
            };

            ci.Transform.Translation.X = 3;
            ci.Transform.Translation.Z = 3;

            def.Add(ci);

            ci = new CompInst
            {
                ComponentName = "Pointy",
                InstanceName = "On the left."
            };

            ci.Transform.Translation.X = -3;
            ci.Transform.Translation.Z = 3;

            def.Add(ci);

            ci = new CompInst
            {
                ComponentName = "Flat",
                InstanceName = "At the origin."
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

            // Two branches of its own.

            ci = new CompInst
            {
                ComponentName = "Skinny",
                InstanceName = "On the far upper right."
            };

            ci.Transform.Translation.X = 3;
            ci.Transform.Translation.Z = 3;

            pointy.Add(ci);

            ci = new CompInst
            {
                ComponentName = "Skinny",
                InstanceName = "On the far upper left."
            };

            ci.Transform.Translation.X = -3;
            ci.Transform.Translation.Z = 3;

            pointy.Add(ci);

            CompDef skinny = new CompDef(
                "Skinny",
                "A Slim Quad");

            Point3[] pts =
            {
                new Point3(-.2, 0, -1),
                new Point3(.2, 0, -1),
                new Point3(.2, 0, 1),
                new Point3(-.2, 0, 1)
            };

            skinny.Add(pts);

            model.Add(skinny);

            model.WriteSketchUpFile(path + @"\ThreePlyTree.skp");

        }
    }
}
