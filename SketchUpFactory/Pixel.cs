using ExLumina.SketchUp.API;

namespace ExLumina.SketchUp.Factory
{
    public class Pixel
    {
        public byte Red
        {
            get => channels[suColorOrder.redIndex];
            set => channels[suColorOrder.redIndex] = value;
        }

        public byte Green
        {
            get => channels[suColorOrder.greenIndex];
            set => channels[suColorOrder.greenIndex] = value;
        }

        public byte Blue
        {
            get => channels[suColorOrder.blueIndex];
            set => channels[suColorOrder.blueIndex] = value;
        }
        public byte Alpha
        {
            get => channels[suColorOrder.alphaIndex];
            set => channels[suColorOrder.alphaIndex] = value;
        }

        readonly byte[] channels = new byte[4];

        static readonly SU.ColorOrder suColorOrder;

        static Pixel()
        {
            suColorOrder = SU.GetColorOrder();
        }

        public byte this[long index]
        {
            get => channels[index];
            set => channels[index] = value;
        }
    }
}
