namespace ExLumina.SketchUp.Factory.Examples
{
    class PlainQuad : Example
    {
        public PlainQuad(string display) : base(display)
        {

        }

        public override void Run(string path)
        {
            Model model = new Model();
            
            model.Entities.Add(
                new Vector3(-1, 0, -1),
                new Vector3(1, 0, -1),
                new Vector3(1, 0, 1),
                new Vector3(-1, 0, 1));

            model.WriteSketchUpFile(path + @"\PlainQuad.skp");
        }
    }
}
