using ExLumina.SketchUp.Factory;

namespace ExLumina.Examples.SketchUp.Factory
{
    // Create a component definition with a reference forward to
    // another definition that will be created later.

    class ForwardComponent : Example
    {
        public ForwardComponent(string display) : base(display)
        {

        }

        public override void Run(string path)
        {
            Model model = new Model();

            // Create a definition of a square shape.

            CompDef square = new CompDef(
                "Flat",
                "A Square");

            // Add a square polygon to the definition.

            Point3[] squarePoints =
            {
                new Point3(-1, 0, -1),
                new Point3(1, 0, -1),
                new Point3(1, 0, 1),
                new Point3(-1, 0, 1)
            };

            square.Add(squarePoints);

            // Add the square definition to the model.

            model.Add(square);

            // Create an instance of a definition that does not
            // yet exist in the model.

            CompInst pointyInstance = new CompInst
            {
                // Refer here to a definition we'll create later.

                ComponentName = "Pointy",
                InstanceName = "Tri the Angle"
            };

            // Move the instance away from the origin a bit.

            pointyInstance.Transform.Translation.Y = 1;
            pointyInstance.Transform.Rotation.Z = -20;

            // Add the instance to the square's definition.

            square.Add(pointyInstance);

            // Instantiate the square definition.

            CompInst squareInstance = new CompInst
            {
                ComponentName = "Flat",
                InstanceName = "Quad the First"
            };

            // Add the instance to the model.

            model.Add(squareInstance);

            // Now create the "Pointy" definition (a triangle).

            CompDef pointy = new CompDef(
                "Pointy",
                "A Triangle");

            // Define a triangular shape.

            Point3[] pointyPoints =
            {
                new Point3(-1, 0, -1),
                new Point3(1, 0, -1),
                new Point3(0, 0, 1)
            };

            // Add the shape to the defintion.

            pointy.Add(pointyPoints);

            // Add the definition to the model.

            model.Add(pointy);

            model.WriteSketchUpFile(path + @"\ForwardComponent.skp");
        }
    }
}
