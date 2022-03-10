using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoManager.GUI.ChooseDirectoryToSync
{
    public partial class Form1 : Form
    {
        public static partial class Helper
        {
            public static void HideElement(Control control)
            {
                control.Visible = false;
            }

            public static void ShowElement(Control control)
            {
                control.Visible = true;
            }

            public static Button AddRowTable(TableLayoutPanel table)
            {
                table.RowCount++;

                Label label = new Label();
                label.Size = new Size(651, 45);
                label.TextAlign = ContentAlignment.MiddleLeft;

                Button button = new Button();
                button.Name = "SelectFolderButton";
                button.Size = new Size(111, 27);
                button.Text = "Select Folder";
                button.UseVisualStyleBackColor = true;

                table.Controls.Add(label, 0, table.RowCount);
                table.Controls.Add(button, 1, table.RowCount);

                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 85F));
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
                table.RowStyles.Add(new RowStyle(SizeType.Absolute, 31F));

                table.AutoSize = true;

                return button;
            }

            public static void RemoveRowTable(TableLayoutPanel table)
            {
                int row = table.RowCount;
                if(row != 0)
                {
                    for (int i = 0; i < table.ColumnCount; i++)
                    {
                        Control control = table.GetControlFromPosition(i, row);
                        table.Controls.Remove(control);
                        control.Dispose();
                    }

                    table.RowCount--;

                    table.AutoSize = false;
                    table.Size = new Size(776, 31 * table.RowCount);

                    table.Refresh();
                }
            }
        }
        public Form1()
        {
            InitializeComponent();
            Helper.HideElement(RemoveRowButton);
            Helper.HideElement(AddRowButton);
            Helper.HideElement(ScanningButton);
            Helper.HideElement(ChooseFolderTable);
        }

        private void SelectFolderSync_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                LabelFoldeSyncPath.Text = folderBrowserDialog.SelectedPath;
                Helper.ShowElement(ChooseFolderTable);
                if (ChooseFolderTable.GetControlFromPosition(0, 0).Text != "")
                {
                    Helper.ShowElement(AddRowButton);
                }
            }
        }

        private void AddRowButton_Click(object sender, EventArgs e)
        {
            Button buttton = Helper.AddRowTable(ChooseFolderTable);
            buttton.Click += new System.EventHandler(this.SelectFolderButton_Click);
            Helper.HideElement(AddRowButton);
            Helper.HideElement(ScanningButton);
            Helper.ShowElement(RemoveRowButton);
            this.Refresh();
        }

        private void SelectFolderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog.ShowDialog();

            if ((Control)sender != null && result == DialogResult.OK)
            {
                int row = ChooseFolderTable.GetPositionFromControl((Control)sender).Row;
                int column = ChooseFolderTable.GetPositionFromControl((Control)sender).Column;
                ChooseFolderTable.GetControlFromPosition(column - 1, row).Text = folderBrowserDialog.SelectedPath;
                Helper.ShowElement(AddRowButton);
                Helper.ShowElement(ScanningButton);
            }
        }

        private void RemoveRowButton_Click(object sender, EventArgs e)
        {
            Helper.RemoveRowTable(ChooseFolderTable);
            if (ChooseFolderTable.RowCount == 1)
            {
                Helper.HideElement(RemoveRowButton);
                Helper.ShowElement(AddRowButton);
            }
            Helper.ShowElement(ScanningButton);
        }
    }
}
