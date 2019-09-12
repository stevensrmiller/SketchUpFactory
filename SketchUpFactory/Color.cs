using ExLumina.SketchUp.API;

namespace ExLumina.SketchUp.Factory
{
    /// <summary>
    /// Define an RGBA color.
    /// </summary>
    public class Color
    {
        /// <summary>
        /// Red channel, [0, 255]
        /// </summary>
        public byte Red { get; set; } = 0x00;

        /// <summary>
        /// Green channel, [0, 255]
        /// </summary>
        public byte Green { get; set; } = 0x00;

        /// <summary>
        /// Blue channel, [0, 255]
        /// </summary>
        public byte Blue { get; set; } = 0x00;

        /// <summary>
        /// Alpha channel, [0, 255]
        /// </summary>
        public byte Alpha { get; set; } = 0xFF;

        internal SU.Color SUColor
        {
            get => new SU.Color
            {
                red = Red,
                green = Green,
                blue = Blue,
                alpha = Alpha
            };
        }

        /// <summary>
        /// Create a color RGB = 0, A = 255;
        /// </summary>
        public Color()
        {

        }

        /// <summary>
        /// Construct a color with preset declared values.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a">Optional, defaults to 255.</param>
        public Color(byte r, byte g, byte b, byte a = 0xFF)
        {
            Red = r;
            Green = g;
            Blue = b;
            Alpha = a;
        }

        internal Color(SU.Color color)
        {
            Red = color.red;
            Green = color.green;
            Blue = color.blue;
            Alpha = color.alpha;
        }
    }
}
