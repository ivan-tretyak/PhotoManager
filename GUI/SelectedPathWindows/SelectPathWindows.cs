using System;
using System.Windows.Forms;



namespace PhotoManager
{
    public partial class SelectPathWindows : Form
    {
        private HelperSelectedPathWindows helper = new();
        public SelectPathWindows()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Button button = helper.AddRow(this.tableLayoutPanel1);
            button.Click += new System.EventHandler(button_Click);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            helper.RemoveRow(this.tableLayoutPanel1);
        }

        private void button_Click(object sender, EventArgs e)
        {
            bool result = helper.SelectFolderPath(sender, this.tableLayoutPanel1);
            if (result)
            {
                this.scanstart.Enabled = true;
            }
        }

        private void scanstart_Click(object sender, EventArgs e)
        {
            helper.Scanning(tableLayoutPanel1, progressBar1, label1);
            this.Close();
        }
    }
}
