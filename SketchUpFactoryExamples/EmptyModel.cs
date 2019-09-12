using ExLumina.SketchUp.Factory;

namespace ExLumina.Examples.SketchUp.Factory
{
    // Create a model with nothing in it.

    class EmptyModel : Example
    {
        public EmptyModel(string display) : base(display)
        {

        }

        public override void Run(string path)
        {
            Model model = new Model();

            model.WriteSketchUpFile(path + @"\EmptyModel.skp");
        }
    }
}
