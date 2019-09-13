using ExLumina.SketchUp.Factory;
using System;
using System.Collections.Generic;

namespace ExLumina.Examples.SketchUp.Factory
{
    // Create a torus. It can be plain, but this one is set
    // to have a modulated surface.

    class PlainTorus : Example
    {
        const int ringSteps = 96;
        const int pieSteps = 288;
        const double ringRadius = 1;
        const double pieRadius = 4;
        const int ringFreq = 17;
        const double ringAmp = ringRadius / 2;

        public PlainTorus(string display) : base(display)
        {

        }

        public override void Run(string path)
        {
            // Compute the ring.

            Point3[] ring = new Point3[ringSteps];

            for (int ringStep = 0; ringStep < ringSteps; ++ringStep)
            {
                double theta = ringStep * 2 * Math.PI / ringSteps;

                ring[ringStep] = new Point3(
                    ringRadius * Math.Cos(theta) + pieRadius,
                    0,
                    ringRadius * Math.Sin(theta));
            }

            // Open the model.

            Model model = new Model();

            // Compute the pie.

            for (int pieStep = 0; pieStep < pieSteps; ++pieStep)
            {
                double theta0 = pieStep * 2 * Math.PI / pieSteps;
                double theta1 = (((pieStep + 1)) % pieSteps) * 2 * Math.PI / pieSteps;

                for (int ringStep = 0; ringStep < ringSteps; ++ringStep)
                {
                    IList<EdgePoint> corners = new List<EdgePoint>();

                    double x = ring[ringStep].X;
                    double y = ring[ringStep].Y;
                    double z = ring[ringStep].Z;

                    double xNext = ring[((ringStep + 1) % ringSteps)].X;
                    double yNext = ring[((ringStep + 1) % ringSteps)].Y;
                    double zNext = ring[((ringStep + 1) % ringSteps)].Z;

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

                    corners.Add(new EdgePoint(x1, y1, z1, true));
                    corners.Add(new EdgePoint(x2, y2, z2, true));
                    corners.Add(new EdgePoint(x3, y3, z3, true));

                    model.Add(corners);

                    corners.Clear();
                    corners.Add(new EdgePoint(x0, y0, z0, true));
                    corners.Add(new EdgePoint(x1, y1, z1, true));
                    corners.Add(new EdgePoint(x3, y3, z3, true));

                    model.Add(corners);
                }
            }

            model.WriteSketchUpFile(path + @"\PlainTorus.skp");
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
