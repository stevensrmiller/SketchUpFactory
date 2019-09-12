using ExLumina.SketchUp.API;

namespace ExLumina.SketchUp.Factory
{
    /// <summary>
    /// An image in the the form need for a Material.
    /// </summary>
    public class Texture
    {
        // Used when packing Materials.

        internal SU.TextureRef textureRef;

        ImageBuffer imageBuffer;

        /// <summary>
        /// Load a texture from an image file.
        /// </summary>
        /// <param name="filename"></param>
        public Texture(string filename) : this(filename, SU.MetersToInches, SU.MetersToInches)
        {

        }

        /// <summary>
        /// Load a texture from an image file, specifying its scale.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="xScale"></param>
        /// <param name="yScale"></param>
        public Texture(string filename, double xScale, double yScale)
        {
            SU.TextureRef textureRef = new SU.TextureRef();

            SU.TextureCreateFromFile(textureRef, filename, xScale, yScale);

            imageBuffer = new ImageBuffer(textureRef);

            SU.TextureRelease(textureRef);
        }

        /// <summary>
        /// Create a texture from an ImageBuffer.
        /// </summary>
        /// <param name="imageBuffer"></param>
        public Texture(ImageBuffer imageBuffer)
        {
            this.imageBuffer = imageBuffer;
        }

        internal void Pack()
        {
            SU.ImageRepRef suImageRepRef = new SU.ImageRepRef();

            SU.ImageRepCreate(suImageRepRef);

            SU.ImageRepSetData(
                suImageRepRef,
                imageBuffer.width,
                imageBuffer.height,
                imageBuffer.bitsPerPixel,
                imageBuffer.rowPadding,
                imageBuffer.pixelData);

            textureRef = new SU.TextureRef();

            SU.TextureCreateFromImageRep(textureRef, suImageRepRef);
        }

        internal Texture(SU.TextureRef textureRef)
        {
            imageBuffer = new ImageBuffer(textureRef);
        }
    }
}
