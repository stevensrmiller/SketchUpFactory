using ExLumina.SketchUp.API;

namespace ExLumina.SketchUp.Factory
{
    /// <summary>
    /// A single pixel from an ImageBuffer.
    /// </summary>
    /// <remarks>
    /// Note that SketchUp adapts the channel positions for each
    /// color within a pixel according to the platform. The 
    /// property accessors for this class are platform-independent.
    /// They use the channel position as provided by SketchUp. Thus
    /// you can rely on each property to return the proper channel's
    /// value for Red, Green, Blue, or Alpha, without needing to
    /// write your code for specific channels.
    /// </remarks>
    public class Pixel
    {
        /// <summary>
        /// The value of the Red channel.
        /// </summary>
        public byte Red
        {
            get => channels[colorOrder.redIndex];
            set => channels[colorOrder.redIndex] = value;
        }

        /// <summary>
        /// The value of the Green channel.
        /// </summary>
        public byte Green
        {
            get => channels[colorOrder.greenIndex];
            set => channels[colorOrder.greenIndex] = value;
        }

        /// <summary>
        /// The value of the Blue channel.
        /// </summary>
        public byte Blue
        {
            get => channels[colorOrder.blueIndex];
            set => channels[colorOrder.blueIndex] = value;
        }

        /// <summary>
        /// The value of the Alpha channel.
        /// </summary>
        public byte Alpha
        {
            get => channels[colorOrder.alphaIndex];
            set => channels[colorOrder.alphaIndex] = value;
        }

        readonly byte[] channels = new byte[4];

        static readonly SU.ColorOrder colorOrder;

        static Pixel()
        {
            colorOrder = SU.GetColorOrder();
        }

        /// <summary>
        /// [ ] Obtain channel data directly by number.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public byte this[long index]
        {
            get => channels[index];
            set => channels[index] = value;
        }
    }
}
