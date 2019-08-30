using System;
using Newtonsoft.Json;

namespace ExLumina.SketchUp.Factory.Examples
{
    class CopyModel : Example
    {
        public CopyModel(string display) : base(display)
        {

        }

        public override void Run(string path)
        {
            Model model = new Model(path + @"\A Model to Copy.skp");

            model.WriteSketchUpFile(path + @"\A Model we Copied.skp");
        }
    }
}
