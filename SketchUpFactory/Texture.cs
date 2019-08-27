namespace ExLumina.SketchUp.Factory
{
    public class Texture
    {
        public string filename;

        public Texture() : this ("<texture filename unset>")
        {

        }

        public Texture(string filename)
        {
            this.filename = filename;
        }
    }
}
