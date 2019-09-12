using ExLumina.SketchUp.API;

namespace ExLumina.SketchUp.Factory
{
    /// <summary>
    /// A generic image object.
    /// </summary>
    public class ImageBuffer
    {
        /// <summary>
        /// Horizontal width in pixels.
        /// </summary>
        public long width;

        /// <summary>
        /// Vertical height in pixels.
        /// </summary>
        public long height;

        /// <summary>
        /// Number of bits per pixel (typically 24 or 32).
        /// </summary>
        public long bitsPerPixel;

        /// <summary>
        /// Number of bytes (not pixesl) after the last pixel in a row before the next row's first pixel.
        /// </summary>
        /// <remarks>
        /// Hardware graphics adapters often exploit speed advantages derived from assuming their image
        /// buffers align the addresses of their rows with powers of two. For a row where this isn't
        /// the case, additional "padding" fills the space. On the hardware, memory devoted to this
        /// address area probably doesn't actually exist. For in-memory images, you need it provide it
        /// or else play some complex tricks to pack/unpack the image data. This implementation
        /// provides it.
        /// <para></para>
        /// Note that the padding is not counted in pixels, but in bytes. Thus, a row of 1,920 pixels will
        /// require 7,680 bytes (at four bytes per pixel). The next power of two equal to or greater than
        /// this is 8,192. The padding will be 512 bytes. In most cases, you will not compute this padding.
        /// It will be provided for you when you load a model containing an image.</remarks>
        public long rowPadding; // in bytes (not pixels) per row of data

        /// <summary>
        /// Raw pixel data.
        /// </summary>
        public byte[] pixelData;

        /// <summary>
        /// Size of the pixelData buffer.
        /// </summary>
        public long dataSize;

        const int bitsPerByte = 8;

        long bytesPerPixel;

        /// <summary>
        /// Creates a new, blank image buffer.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="bitsPerPixel">Optional, defaults to 24.</param>
        /// <param name="rowPadding">Optional, defaults, to zero.</param>
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

        internal ImageBuffer(SU.TextureRef suTextureRef)
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

        /// <summary>
        /// [ ] Retrieves a byte from the raw pixel buffer.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
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

        /// <summary>
        /// [ , ] Retrieves a pixel at a given location.
        /// </summary>
        /// <remarks>
        /// This two-index indexer resolves all issues of padding and pixel size. Simply
        /// provide the x and y coordinates in the image. Note that images put their
        /// origin at the upper-left corner, with positive row counts moving downward.
        /// </remarks>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Retrieves the single byte in a pixel from a given channel.
        /// </summary>
        /// <param name="x">x coordinate of the pixel.</param>
        /// <param name="y">y coordinate of the pixel.</param>
        /// <param name="c">color (or alpha) channel number within the pixel.</param>
        /// <returns></returns>
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
