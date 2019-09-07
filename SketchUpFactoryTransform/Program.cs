using ExLumina.SketchUp.Factory;
using System;

namespace SketchUpFactoryTransform
{
    class Program
    {
        static void Main(string[] args)
        {
            Transform transform = new Transform();

            transform.translation.x = 10;
            transform.translation.y = 20;
            transform.translation.z = 30;

            transform.rotation.x = 0;
            transform.rotation.y = 180;
            transform.rotation.z = 0;

            transform.scale.x = 1;
            transform.scale.y = 1;
            transform.scale.z = 1;

            Dump(transform);

            Transform repro = new Transform(transform.SUTransformation);

            Dump(repro);

            transform.rotation.y = 1;
            transform.scale.x = -1;
            transform.scale.y = 1;
            transform.scale.z = -1;

            Dump(transform);

            repro = new Transform(transform.SUTransformation);

            Dump(repro);

            Console.ReadLine();
        }

        static void Dump(Transform t)
        {
            //Console.WriteLine("{0,5:F2} {1,5:F2} {2,5:F2} {3,5:F2}", 
            //    t.SUTransformation.m11,
            //    t.SUTransformation.m12,
            //    t.SUTransformation.m13,
            //    t.SUTransformation.m14);
            //Console.WriteLine("{0,5:F2} {1,5:F2} {2,5:F2} {3,5:F2}",
            //    t.SUTransformation.m21,
            //    t.SUTransformation.m22,
            //    t.SUTransformation.m23,
            //    t.SUTransformation.m24);
            //Console.WriteLine("{0,5:F2} {1,5:F2} {2,5:F2} {3,5:F2}",
            //    t.SUTransformation.m31,
            //    t.SUTransformation.m32,
            //    t.SUTransformation.m33,
            //    t.SUTransformation.m34);
            //Console.WriteLine("{0,5:F2} {1,5:F2} {2,5:F2} {3,5:F2}\n",
            //    t.SUTransformation.m41,
            //    t.SUTransformation.m42,
            //    t.SUTransformation.m43,
            //    t.SUTransformation.m44);

            for (int r = 0; r < 4; ++r)
            {
                for (int c = 0; c < 4; ++c)
                {
                    Console.Write("{0,5:F2} ", t.SUTransformation[r, c]);
                }

                Console.WriteLine();
            }

            Console.WriteLine("{0,5:F2} {1,5:F2} {2,5:F2}, {3,5:F2} {4,5:F2} {5,5:F2}, {6,5:F2} {7,5:F2} {8,5:F2}",
                t.scale.x, t.scale.y, t.scale.z,
                t.rotation.x, t.rotation.y, t.rotation.z,
                t.translation.x, t.translation.y, t.translation.z);

            Console.WriteLine("\n");
        }
    }
}
