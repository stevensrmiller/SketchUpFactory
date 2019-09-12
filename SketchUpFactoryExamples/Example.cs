namespace ExLumina.Examples.SketchUp.Factory
{
    // Parent class for examples to provide a uniform interface
    // and display format for the list of checkboxes.

    abstract class Example
    {
        readonly string name;

        public Example(string name)
        {
            this.name = name;
        }

        public Example() : this("<no name>")
        {

        }

        public abstract void Run(string path);

        public override string ToString()
        {
            return name;
        }
    }
}
