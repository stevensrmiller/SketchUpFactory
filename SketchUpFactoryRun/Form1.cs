using ExLumina.SketchUp.Factory;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows.Forms;

namespace ExLumina.SketchUp.Factory.Run
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            string json = File.ReadAllText("model.json");

            Model model = JsonConvert.DeserializeObject<Model>(json);

            try
            {
                Factory.MakeSketchUpFile(model, "model.skp");
                MessageBox.Show("Nobody died.", "Pretty Good Night");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "That Went Badly.");
            }
        }
    }
}
