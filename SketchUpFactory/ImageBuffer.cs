using ExLumina.SketchUp.API;

namespace ExLumina.SketchUp.Factory
{
    public class ImageBuffer
    {
        public long width;
        public long height;
        public long bitsPerPixel;
        public long rowPadding; // in bytes (not pixels) per row of data
        public byte[] pixelData;
        public long dataSize;

        const int bitsPerByte = 8;

        long bytesPerPixel;

        public ImageBuffer(long width, long height, long bitsPerPixel = 24, long rowPadding = 0)
        {
            this.width = width;
            this.height = height;
            this.bitsPerPixel = bitsPerPixel;
            this.rowPadding = rowPadding;

            bytesPerPixel = bitsPerPixel / bitsPerByte;

            long bufferSize = height * (width * bytesPerPixel + rowPadding);

            pixelData = new byte[bufferSize];
        }

        public ImageBuffer(SU.TextureRef suTextureRef)
        {
            SU.ImageRepRef suImageRepRef = new SU.ImageRepRef();
            SU.ImageRepCreate(suImageRepRef);

            // This gets either the non-colorized image, if the texture
            // is not colorized, or else a colorized version.

            // TODO: Find out how the HLS deltas work. Note that the
            // ability to colorize is SketchUp-specific.

            SU.TextureGetColorizedImageRep(suTextureRef, suImageRepRef);

            SU.ImageRepGetPixelDimensions(suImageRepRef, out width, out height);

            SU.ImageRepGetRowPadding(suImageRepRef, out rowPadding);

            SU.ImageRepGetDataSize(suImageRepRef, out dataSize, out bitsPerPixel);

            bytesPerPixel = bitsPerPixel / bitsPerByte;

            // Check for a match.

            if (dataSize != height * (width * bytesPerPixel + rowPadding))
            {
                throw new System.Exception("Data size of ImageRep conflicts with dimensions.");
            }

            pixelData = new byte[dataSize];

            SU.ImageRepGetData(suImageRepRef, dataSize, pixelData);
        }

        public byte this [long index]
        {
            get
            {
                return pixelData[index];
            }

            set
            {
                pixelData[index] = value;
            }
        }

        public Pixel this [long x, long y]
        {
            get
            {
                long indexer = y * (width * bytesPerPixel + rowPadding) + x * bytesPerPixel;

                Pixel pixel = new Pixel();

                for (int channel = 0; channel < bytesPerPixel; ++channel)
                {
                    pixel[channel] = pixelData[indexer + channel];
                }

                return pixel;
            }

            set
            {
                long indexer = y * (width * bytesPerPixel + rowPadding) + x * bytesPerPixel;

                for (int channel = 0; channel < bytesPerPixel; ++channel)
                {
                    pixelData[indexer + channel] = value[channel];
                }
            }
        }

        public byte this [long x, long y, int c]
        {
            get
            {
                return this[x, y][c];
            }

            set
            {
                this[x, y][c] = value;
            }
        }
    }
}
