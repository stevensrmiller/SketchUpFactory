﻿using System;
using Newtonsoft.Json;

namespace ExLumina.SketchUp.Factory.Examples
{
    class ForwardComponent : Example
    {
        public ForwardComponent(string display) : base(display)
        {

        }

        public override void Run(string path)
        {
            Model model = new Model();

            ComponentDefinition flat = new ComponentDefinition(
                model,
                "Flat",
                "A Square");

            flat.Entities.Add(
                new Vector3(-1, 0, -1),
                new Vector3(1, 0, -1),
                new Vector3(1, 0, 1),
                new Vector3(-1, 0, 1));

            ComponentInstance ci2 = new ComponentInstance
            {
                definitionName = "Pointy",
                instanceName = "Tri the Angle"
            };

            ci2.transform.translation.y = 1;
            ci2.transform.rotation.z = -20;

            flat.Entities.Add(ci2);


            ComponentInstance ci = new ComponentInstance
            {
                definitionName = "Flat",
                instanceName = "Quad the First"
            };

            model.Entities.Add(ci);

            ComponentDefinition pointy = new ComponentDefinition(
                model,
                "Pointy",
                "A Triangle");

            pointy.Entities.Add(
                new Vector3(-1, 0, -1),
                new Vector3(1, 0, -1),
                new Vector3(0, 0, 1));

            model.WriteSketchUpFile(path + @"\ForwardComponent.skp");
        }
    }
}
