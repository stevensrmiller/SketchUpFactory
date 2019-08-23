using ExLumina.SketchUp.API;

namespace ExLumina.SketchUp.Factory
{
    public class Color
    {
        public byte red;
        public byte green;
        public byte blue;
        public byte alpha;

        internal SU.Color SUColor
        {
            get => new SU.Color { red = red, green = green, blue = blue, alpha = alpha };
        }

        public Color()
        {
            alpha = 0xFF;
        }
    }
}
