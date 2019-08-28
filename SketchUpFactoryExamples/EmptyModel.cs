using System;
using Newtonsoft.Json;

namespace ExLumina.SketchUp.Factory.Examples
{
    class EmptyModel : Example
    {
        public EmptyModel(string display) : base(display)
        {

        }

        public override void Run(string path)
        {
            using (Model model = new Model())
            {
                model.WriteSketchUpFile(path + @"\EmptyModel.skp");
            }
        }
    }
}
