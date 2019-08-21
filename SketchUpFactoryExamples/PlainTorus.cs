using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ExLumina.SketchUp.Factory.Examples
{
    class PlainTorus : Example
    {
        const int ringSteps = 96;
        const int pieSteps = 288;
        const double ringRadius = 1;
        const double pieRadius = 4;
        const int ringFreq = 17;
        const double ringAmp = ringRadius /2; 

        public PlainTorus(string display) : base(display)
        {

        }

        public override void Run(string path)
        {
            // Compute the ring.

            Vector3[] ring = new Vector3[ringSteps];

            for (int ringStep = 0; ringStep < ringSteps; ++ringStep)
            {
                double theta = ringStep * 2 * Math.PI / ringSteps;

                ring[ringStep] = new Vector3(
                    ringRadius * Math.Cos(theta) + pieRadius,
                    0,
                    ringRadius * Math.Sin(theta));
            }

            // Open the model.

            Model model = new Model();

            Geometry geometry = new Geometry();

            model.Add(geometry);

            // Compute the pie.

            for (int pieStep = 0; pieStep < pieSteps; ++pieStep)
            {
                double theta0 = pieStep * 2 * Math.PI / pieSteps;
                double theta1 = (((pieStep + 1)) % pieSteps) * 2 * Math.PI / pieSteps;

                for (int ringStep = 0; ringStep < ringSteps; ++ringStep)
                {
                    //Ray[] corners = new Ray[3];
                    //Vector3[] corners = new Vector3[3];
                    IList<Ray> corners = new List<Ray>();

                    double x = ring[ringStep].x;
                    double y = ring[ringStep].y;
                    double z = ring[ringStep].z;

                    double xNext = ring[((ringStep + 1) % ringSteps)].x;
                    double yNext = ring[((ringStep + 1) % ringSteps)].y;
                    double zNext = ring[((ringStep + 1) % ringSteps)].z;

                    double xMod;
                    double yMod;
                    double zMod;

                    RingMod(
                        ringFreq, ringAmp, theta1,
                        x, y, z,
                        out xMod, out yMod, out zMod);

                    double x0 = xMod * Math.Cos(theta1) - yMod * Math.Sin(theta1);
                    double y0 = xMod * Math.Sin(theta1) + yMod * Math.Cos(theta1);
                    double z0 = zMod;

                    RingMod(
                        ringFreq, ringAmp, theta1,
                        xNext, yNext, zNext,
                        out xMod, out yMod, out zMod);

                    double x1 = xMod * Math.Cos(theta1) - yMod * Math.Sin(theta1);
                    double y1 = xMod * Math.Sin(theta1) + yMod * Math.Cos(theta1);
                    double z1 = zMod;

                    RingMod(
                        ringFreq, ringAmp, theta0,
                        xNext, yNext, zNext,
                        out xMod, out yMod, out zMod);

                    double x2 = xMod * Math.Cos(theta0) - yMod * Math.Sin(theta0);
                    double y2 = xMod * Math.Sin(theta0) + yMod * Math.Cos(theta0);
                    double z2 = zMod;

                    RingMod(
                        ringFreq, ringAmp, theta0,
                        x, y, z,
                        out xMod, out yMod, out zMod);

                    double x3 = xMod * Math.Cos(theta0) - yMod * Math.Sin(theta0);
                    double y3 = xMod * Math.Sin(theta0) + yMod * Math.Cos(theta0);
                    double z3 = zMod;

                    corners.Add(new Ray(x1, y1, z1, true));
                    corners.Add(new Ray(x2, y2, z2, true));
                    corners.Add(new Ray(x3, y3, z3, true));

                    Face face = MakeFace.From(corners);
                    geometry.Add(face);

                    corners.Clear();
                    corners.Add(new Ray(x0, y0, z0, true));
                    corners.Add(new Ray(x1, y1, z1, true));
                    corners.Add(new Ray(x3, y3, z3, true));

                    face = MakeFace.From(corners);
                    geometry.Add(face);
                }
            }

            Factory.MakeSketchUpFile(model, path + @"\PlainTorus.skp");
        }

        void RingMod(
            int f,
            double a,
            double t,
            double x,
            double y,
            double z,
            out double xm,
            out double ym,
            out double zm)
        {
            double am = a * Math.Sin(t * f) + ringRadius;

            xm = am * (x - pieRadius) + pieRadius;
            ym = y;
            zm = am * z;
        }
    }
}
