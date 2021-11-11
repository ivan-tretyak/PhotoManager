using System.Windows.Forms;

namespace PhotoManager
{
    public class HelperSelectedPathWindows
    {
        public Button AddRow(TableLayoutPanel panel)
        {
            panel.RowCount++;

            var textbox = new TextBox();
            var label = new Label();
            var button = new Button();

            //label.TabIndex = 0;
            label.Text = "Path:";
            label.AutoSize = true;

            textbox.Size = new System.Drawing.Size(588, 23);
            textbox.Name = $"textBox{panel.RowCount}";
            //textbox.TabIndex = 1;

            button.Size = new System.Drawing.Size(139, 23);
            //button.TabIndex = 2;
            button.Text = "Choose";
            button.UseVisualStyleBackColor = true;
            button.Name = $"{panel.RowCount}";

            panel.Controls.Add(textbox, 1, panel.RowCount);
            panel.Controls.Add(label, 0, panel.RowCount);
            panel.Controls.Add(button, 2, panel.RowCount);
            panel.RowStyles.Add(new RowStyle());
            panel.Size = new System.Drawing.Size(776, 31 * panel.RowCount);

            return button;
        }

        public void RemoveRow(TableLayoutPanel panel)
        {
            int row = panel.RowCount;
            if (row != 1)
            {
                for (int i = 0; i < panel.ColumnCount; i++)
                {
                    Control c = panel.GetControlFromPosition(i, row);
                    panel.Controls.Remove(c);
                    c.Dispose();
                }
                panel.RowStyles.RemoveAt(row - 1);
                panel.RowCount--;

                panel.Size = new System.Drawing.Size(776, 31 * panel.RowCount);
            }
        }
        public bool SelectFolderPath(object sender, TableLayoutPanel panel)
        {
            var s = new FolderBrowserDialog();
            var result = s.ShowDialog();
            if ((Control)sender != null && result == DialogResult.OK)
            {
                int row = panel.GetPositionFromControl((Control)sender).Row;
                int column = panel.GetPositionFromControl((Control)sender).Column;
                panel.GetControlFromPosition(column - 1, row).Text = s.SelectedPath;
                return true;
            }
            return false;
        }
    }
}
