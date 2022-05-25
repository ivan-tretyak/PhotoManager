using Microsoft.Win32;
using ORMDatabaseModule;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PhotoManager.GUI.ChooseDirectoryToSync
{
    public class Helper
    {
        public void HideElement(Control control)
        {
            control.Visible = false;
        }

        public void ShowElement(Control control)
        {
            control.Visible = true;
        }

        public Button AddRowTable(TableLayoutPanel table)
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

        public void RemoveRowTable(TableLayoutPanel table)
        {
            int row = table.RowCount;
            if (row != 0)
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

        public string ShowFolderBrowserDialog()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                return folderBrowserDialog.SelectedPath;
            }
            else
            {
                return "";
            }
        }

        public string Prefix(int lengthPrefix)
        {
            Random random = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXY";
            chars += chars.ToLower();
            return new string(Enumerable.Repeat(chars, lengthPrefix)
        .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
