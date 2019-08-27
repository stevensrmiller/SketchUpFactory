using System;
using System.Windows.Forms;

namespace ExLumina.SketchUp.Factory.Examples
{
    public partial class MainForm : Form
    {
        string location;
        private Button btnSetLocation;
        private Button btnRunExamples;
        private Label lblLocation;
        private CheckedListBox clbList;
        private Button btnExit;
        Example[] examples =
        {
            new PlainQuad("Plain Quad"),
            new PlainCube("Plain Cube"),
            new PlainTorus("Plain Torus"),
            new ThreeCubesWelded("Three Cubes Welded"),
            new ThreeCubesApart("Three Cubes Apart (using Model.Separate)"),
            new QuadComponent("Quad Component"),
            new ThreePlyTree("Three Ply Tree"),
            new GroupQuad("Group Quad"),
            new TexturedQuad("Textured Quad"),
        };

        public MainForm()
        {
            InitializeComponent();

            foreach (var example in examples)
            {
                clbList.Items.Add(example);
            }

            for (int index = 0; index < clbList.Items.Count; ++index)
            {
                clbList.SetItemChecked(index, true);
            }
        }

        private void btnRunExamples_Click(object sender, EventArgs e)
        {

        }

        private void btnSetLocation_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            fd.Description = "Select the location where you want the example " +
                             "programs to create their SketchUp output files.";

            fd.SelectedPath = @"C:\Users\smiller\Factory Output";

            if (fd.ShowDialog() == DialogResult.OK)
            {
                btnRunExamples.Enabled = true;
                location = fd.SelectedPath;
                lblLocation.Text = location;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void InitializeComponent()
        {
            this.btnSetLocation = new System.Windows.Forms.Button();
            this.btnRunExamples = new System.Windows.Forms.Button();
            this.lblLocation = new System.Windows.Forms.Label();
            this.clbList = new System.Windows.Forms.CheckedListBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSetLocation
            // 
            this.btnSetLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetLocation.Location = new System.Drawing.Point(8, 8);
            this.btnSetLocation.Name = "btnSetLocation";
            this.btnSetLocation.Size = new System.Drawing.Size(152, 64);
            this.btnSetLocation.TabIndex = 1;
            this.btnSetLocation.Text = "Set Location";
            this.btnSetLocation.UseVisualStyleBackColor = true;
            this.btnSetLocation.Click += new System.EventHandler(this.btnSetLocation_Click);
            // 
            // btnRunExamples
            // 
            this.btnRunExamples.Enabled = false;
            this.btnRunExamples.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRunExamples.Location = new System.Drawing.Point(8, 80);
            this.btnRunExamples.Name = "btnRunExamples";
            this.btnRunExamples.Size = new System.Drawing.Size(152, 64);
            this.btnRunExamples.TabIndex = 3;
            this.btnRunExamples.Text = "Run Examples";
            this.btnRunExamples.UseVisualStyleBackColor = true;
            this.btnRunExamples.Click += new System.EventHandler(this.btnRunExamples_Click_1);
            // 
            // lblLocation
            // 
            this.lblLocation.BackColor = System.Drawing.SystemColors.Window;
            this.lblLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLocation.Location = new System.Drawing.Point(192, 24);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(336, 40);
            this.lblLocation.TabIndex = 4;
            this.lblLocation.Text = "<unset>";
            // 
            // clbList
            // 
            this.clbList.CheckOnClick = true;
            this.clbList.FormattingEnabled = true;
            this.clbList.Location = new System.Drawing.Point(192, 96);
            this.clbList.Name = "clbList";
            this.clbList.Size = new System.Drawing.Size(336, 214);
            this.clbList.TabIndex = 5;
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(8, 248);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(152, 64);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click_1);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(536, 319);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.clbList);
            this.Controls.Add(this.lblLocation);
            this.Controls.Add(this.btnRunExamples);
            this.Controls.Add(this.btnSetLocation);
            this.Name = "MainForm";
            this.Text = "SkecthUpFactory Examples";
            this.ResumeLayout(false);

        }

        private void btnRunExamples_Click_1(object sender, EventArgs e)
        {
            foreach (int index in clbList.CheckedIndices)
            {
                
                Console.WriteLine("Running [{0}]", clbList.Items[index].ToString());
                ((Example)(clbList.Items[index])).Run(location);
            }

            MessageBox.Show(
                "Done.",
                "SketchUp Factory Examples",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
