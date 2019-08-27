﻿using System;
using Newtonsoft.Json;

namespace ExLumina.SketchUp.Factory.Examples
{
    class PlainQuad : Example
    {
        public PlainQuad(string display) : base(display)
        {

        }

        public override void Run(string path)
        {
            Model model = new Model();

            using (Entities entities = model.entities)
            {
                entities.Add(
                    new Vector3(-1, 0, -1),
                    new Vector3(1, 0, -1),
                    new Vector3(1, 0, 1),
                    new Vector3(-1, 0, 1));
            }
            
            //Console.WriteLine(JsonConvert.SerializeObject(model,
            //                    Newtonsoft.Json.Formatting.Indented));

            model.WriteSketchUpFile(path + @"\PlainQuad.skp");
        }
    }
}
