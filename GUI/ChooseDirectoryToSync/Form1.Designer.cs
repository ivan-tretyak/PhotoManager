
namespace PhotoManager.GUI.ChooseDirectoryToSync
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ScanningButton = new System.Windows.Forms.Button();
            this.ChooseFolderTable = new System.Windows.Forms.TableLayoutPanel();
            this.SelectFolderButton = new System.Windows.Forms.Button();
            this.LabelFolderScanPath = new System.Windows.Forms.Label();
            this.SelectFolderSync = new System.Windows.Forms.Button();
            this.LabelFolderSyncPath = new System.Windows.Forms.Label();
            this.AddRowButton = new System.Windows.Forms.Button();
            this.RemoveRowButton = new System.Windows.Forms.Button();
            this.ChooseFolderTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // ScanningButton
            // 
            this.ScanningButton.Location = new System.Drawing.Point(672, 12);
            this.ScanningButton.Name = "ScanningButton";
            this.ScanningButton.Size = new System.Drawing.Size(116, 23);
            this.ScanningButton.TabIndex = 0;
            this.ScanningButton.Text = "Start Scanning";
            this.ScanningButton.UseVisualStyleBackColor = true;
            this.ScanningButton.Click += new System.EventHandler(this.ScanningButton_Click);
            // 
            // ChooseFolderTable
            // 
            this.ChooseFolderTable.ColumnCount = 2;
            this.ChooseFolderTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.ChooseFolderTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.ChooseFolderTable.Controls.Add(this.LabelFolderScanPath, 0, 0);
            this.ChooseFolderTable.Controls.Add(this.SelectFolderButton, 1, 0);
            this.ChooseFolderTable.Location = new System.Drawing.Point(12, 99);
            this.ChooseFolderTable.Name = "ChooseFolderTable";
            this.ChooseFolderTable.RowCount = 1;
            this.ChooseFolderTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.ChooseFolderTable.Size = new System.Drawing.Size(776, 31);
            this.ChooseFolderTable.TabIndex = 2;
            // 
            // SelectFolderButton
            // 
            this.SelectFolderButton.Location = new System.Drawing.Point(662, 3);
            this.SelectFolderButton.Name = "SelectFolderButton";
            this.SelectFolderButton.Size = new System.Drawing.Size(111, 25);
            this.SelectFolderButton.TabIndex = 0;
            this.SelectFolderButton.Text = "Select Folder";
            this.SelectFolderButton.UseVisualStyleBackColor = true;
            this.SelectFolderButton.Click += new System.EventHandler(this.SelectFolderButton_Click);
            // 
            // LabelFolderScanPath
            // 
            this.LabelFolderScanPath.Location = new System.Drawing.Point(3, 0);
            this.LabelFolderScanPath.Name = "LabelFolderScanPath";
            this.LabelFolderScanPath.Size = new System.Drawing.Size(648, 30);
            this.LabelFolderScanPath.TabIndex = 1;
            this.LabelFolderScanPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SelectFolderSync
            // 
            this.SelectFolderSync.Location = new System.Drawing.Point(669, 41);
            this.SelectFolderSync.Name = "SelectFolderSync";
            this.SelectFolderSync.Size = new System.Drawing.Size(111, 39);
            this.SelectFolderSync.TabIndex = 1;
            this.SelectFolderSync.Text = "Select Folder Sync";
            this.SelectFolderSync.UseVisualStyleBackColor = true;
            this.SelectFolderSync.Click += new System.EventHandler(this.SelectFolderSync_Click);
            // 
            // LabelFolderSyncPath
            // 
            this.LabelFolderSyncPath.Location = new System.Drawing.Point(12, 38);
            this.LabelFolderSyncPath.Name = "LabelFolderSyncPath";
            this.LabelFolderSyncPath.Size = new System.Drawing.Size(651, 45);
            this.LabelFolderSyncPath.TabIndex = 2;
            this.LabelFolderSyncPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AddRowButton
            // 
            this.AddRowButton.Location = new System.Drawing.Point(591, 12);
            this.AddRowButton.Name = "AddRowButton";
            this.AddRowButton.Size = new System.Drawing.Size(75, 23);
            this.AddRowButton.TabIndex = 3;
            this.AddRowButton.Text = "Add row";
            this.AddRowButton.UseVisualStyleBackColor = false;
            this.AddRowButton.Click += new System.EventHandler(this.AddRowButton_Click);
            // 
            // RemoveRowButton
            // 
            this.RemoveRowButton.Location = new System.Drawing.Point(487, 12);
            this.RemoveRowButton.Name = "RemoveRowButton";
            this.RemoveRowButton.Size = new System.Drawing.Size(98, 23);
            this.RemoveRowButton.TabIndex = 4;
            this.RemoveRowButton.Text = "Remove row";
            this.RemoveRowButton.UseVisualStyleBackColor = true;
            this.RemoveRowButton.Click += new System.EventHandler(this.RemoveRowButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.SelectFolderSync);
            this.Controls.Add(this.ChooseFolderTable);
            this.Controls.Add(this.LabelFolderSyncPath);
            this.Controls.Add(this.RemoveRowButton);
            this.Controls.Add(this.AddRowButton);
            this.Controls.Add(this.ScanningButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ChooseFolderTable.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ScanningButton;
        private System.Windows.Forms.TableLayoutPanel ChooseFolderTable;
        private System.Windows.Forms.Button SelectFolderButton;
        private System.Windows.Forms.Label LabelFolderScanPath;
        private System.Windows.Forms.Button SelectFolderSync;
        private System.Windows.Forms.Label LabelFolderSyncPath;
        private System.Windows.Forms.Button AddRowButton;
        private System.Windows.Forms.Button RemoveRowButton;
    }
}