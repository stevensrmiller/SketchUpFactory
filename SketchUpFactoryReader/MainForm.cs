using ExLumina.SketchUp.Factory;
using System;
using System.Windows.Forms;

namespace SketchUp.Factory.Reader
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "SketchUp Factory Reader";
                openFileDialog.InitialDirectory = 
                    System.Environment.GetFolderPath(
                        Environment.SpecialFolder.UserProfile);
                openFileDialog.Filter = "SketchUp files (*.skp)|*.skp|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.CheckFileExists = true;
                openFileDialog.FileOk += FileOkHandler;
                openFileDialog.ShowDialog();
            }
        }

        private void FileOkHandler(object sender,
            System.ComponentModel.CancelEventArgs e)
        {
            OpenFileDialog ofd = sender as OpenFileDialog;

            Model model = Reader.Read(ofd.FileName);

            //MessageBox.Show(
            //    "\"" + ofd.FileName + "\"",
            //    "File Path",
            //    MessageBoxButtons.OK,
            //    MessageBoxIcon.Information);
        }
    }
}
