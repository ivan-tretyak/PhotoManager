//PhotoManager. Program to organize your photo to album.
//Copyright (C) 2021 Ivan Tretyak Nickolaevich
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA

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
