using ExLumina.SketchUp.Factory;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace ExLumina.Examples.SketchUp.Factory
{
    // Copy the parts of a SketchUp model that are implemented
    // in the factory.

    class CopyModel : Example
    {
        public CopyModel(string display) : base(display)
        {

        }

        public override void Run(string path)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "SketchUp Factory Copier";
                openFileDialog.InitialDirectory = path;
                openFileDialog.Filter = "SketchUp files (*.skp)|*.skp|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.CheckFileExists = true;
                openFileDialog.FileOk += FileOkHandler;
                openFileDialog.ShowDialog();
            }
        }

        private void FileOkHandler(
            object sender,
            CancelEventArgs e)
        {
            OpenFileDialog ofd = sender as OpenFileDialog;

            // Create a Model object from a SketchUp file.

            Model model = new Model(ofd.FileName);

            string copy =
                Path.GetDirectoryName(ofd.FileName) + @"\" +
                Path.GetFileNameWithoutExtension(ofd.FileName) + " - Copy.skp";

            // Write the Model to a new SketchUp file.

            model.WriteSketchUpFile(copy);
        }
    }
}
