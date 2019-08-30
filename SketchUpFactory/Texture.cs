using ExLumina.SketchUp.API;

namespace ExLumina.SketchUp.Factory
{
    public class Texture
    {
        // Used when packing Materials.

        internal SU.TextureRef suTextureRef;

        ImageBuffer imageBuffer;

        public Texture(string filename) : this(filename, SU.MetersToInches, SU.MetersToInches)
        {

        }

        public Texture(string filename, double xScale, double yScale)
        {
            SU.TextureRef suTextureRef = new SU.TextureRef();

            SU.TextureCreateFromFile(suTextureRef, filename, xScale, yScale);

            imageBuffer = new ImageBuffer(suTextureRef);

            SU.TextureRelease(suTextureRef);
        }

        public Texture(ImageBuffer imageBuffer)
        {
            this.imageBuffer = imageBuffer;
        }

        public void Pack()
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

            suTextureRef = new SU.TextureRef();

            SU.TextureCreateFromImageRep(suTextureRef, suImageRepRef);
        }

        public Texture(SU.TextureRef suTextureRef)
        {
            imageBuffer = new ImageBuffer(suTextureRef);
        }
    }
}
