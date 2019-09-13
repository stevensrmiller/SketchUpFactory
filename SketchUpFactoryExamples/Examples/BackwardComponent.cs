using ExLumina.SketchUp.Factory;

namespace ExLumina.Examples.SketchUp.Factory
{
    // Create a component definition with a reference back to
    // another definition previously created.

    class BackwardComponent : Example
    {
        public BackwardComponent(string display) : base(display)
        {

        }

        public override void Run(string path)
        {
            Model model = new Model();

            // Create a definition in a triangle shape.

            CompDef pointy = new CompDef("Pointy", "A Triangle");

            // Add the definition to the model.

            model.Add(pointy);

            // Lay out the points of the triangle.

            Point3[] pointyPoints =
            {
                new Point3(-1, 0, -1),
                new Point3(1, 0, -1),
                new Point3(0, 0, 3)
            };

            // Use them to add a face to the definition.

            pointy.Add(pointyPoints);

            // Create a definition in a square shape.

            CompDef flat = new CompDef(
                "Flat",
                "A Square");

            // Add the definition to the model.

            model.Add(flat);

            // Lay out the points of a square.

            Point3[] flatPoints =
            {
                new Point3(-1, 0, -1),
                new Point3(1, 0, -1),
                new Point3(1, 0, 1),
                new Point3(-1, 0, 1)
            };

            // Use them to add a face to the definition.

            flat.Add(flatPoints);

            // Create an instance of the triangle.

            CompInst ci2 = new CompInst
            {
                ComponentName = "Pointy",
                InstanceName = "Tri the Angle"
            };

            // Move it away from the square.

            ci2.Transform.Translation.Y = 10;
            ci2.Transform.Rotation.X = 45;
            ci2.Transform.Rotation.Y = 90;
            ci2.Transform.Rotation.Z = -40;

            // Add the instance of the triangle to the square's definition.

            flat.Add(ci2);

            // Create an instance of the square (which now includes the triangle).

            CompInst ci = new CompInst
            {
                ComponentName = "Flat",
                InstanceName = "Quad the First"
            };

            // Add that instance of the square to the model.

            model.Add(ci);

            model.WriteSketchUpFile(path + @"\BackwardComponent.skp");
        }
    }
}
