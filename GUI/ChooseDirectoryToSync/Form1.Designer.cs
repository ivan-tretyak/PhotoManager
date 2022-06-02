
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ScanningButton = new System.Windows.Forms.Button();
            this.ChooseFolderTable = new System.Windows.Forms.TableLayoutPanel();
            this.SelectFolderButton = new System.Windows.Forms.Button();
            this.LabelFolderScanPath = new System.Windows.Forms.Label();
            this.SelectFolderSync = new System.Windows.Forms.Button();
            this.LabelFolderSyncPath = new System.Windows.Forms.Label();
            this.RemoveRowButton = new System.Windows.Forms.Button();
            this.DBAlreadyExists = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.AddRowButton = new System.Windows.Forms.Button();
            this.ChooseFolderTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // ScanningButton
            // 
            resources.ApplyResources(this.ScanningButton, "ScanningButton");
            this.ScanningButton.Name = "ScanningButton";
            this.ScanningButton.UseVisualStyleBackColor = true;
            this.ScanningButton.Click += new System.EventHandler(this.ScanningButton_Click);
            // 
            // ChooseFolderTable
            // 
            resources.ApplyResources(this.ChooseFolderTable, "ChooseFolderTable");
            this.ChooseFolderTable.Controls.Add(this.SelectFolderButton, 1, 0);
            this.ChooseFolderTable.Controls.Add(this.LabelFolderScanPath, 0, 0);
            this.ChooseFolderTable.Name = "ChooseFolderTable";
            // 
            // SelectFolderButton
            // 
            resources.ApplyResources(this.SelectFolderButton, "SelectFolderButton");
            this.SelectFolderButton.Name = "SelectFolderButton";
            this.SelectFolderButton.UseVisualStyleBackColor = true;
            this.SelectFolderButton.Click += new System.EventHandler(this.SelectFolderButton_Click);
            // 
            // LabelFolderScanPath
            // 
            resources.ApplyResources(this.LabelFolderScanPath, "LabelFolderScanPath");
            this.LabelFolderScanPath.Name = "LabelFolderScanPath";
            // 
            // SelectFolderSync
            // 
            resources.ApplyResources(this.SelectFolderSync, "SelectFolderSync");
            this.SelectFolderSync.Name = "SelectFolderSync";
            this.SelectFolderSync.UseVisualStyleBackColor = true;
            this.SelectFolderSync.Click += new System.EventHandler(this.SelectFolderSync_Click);
            // 
            // LabelFolderSyncPath
            // 
            resources.ApplyResources(this.LabelFolderSyncPath, "LabelFolderSyncPath");
            this.LabelFolderSyncPath.Name = "LabelFolderSyncPath";
            // 
            // RemoveRowButton
            // 
            resources.ApplyResources(this.RemoveRowButton, "RemoveRowButton");
            this.RemoveRowButton.Name = "RemoveRowButton";
            this.RemoveRowButton.UseVisualStyleBackColor = true;
            this.RemoveRowButton.Click += new System.EventHandler(this.RemoveRowButton_Click);
            // 
            // DBAlreadyExists
            // 
            resources.ApplyResources(this.DBAlreadyExists, "DBAlreadyExists");
            this.DBAlreadyExists.Name = "DBAlreadyExists";
            this.DBAlreadyExists.UseVisualStyleBackColor = true;
            this.DBAlreadyExists.Click += new System.EventHandler(this.DBAlreadyExists_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // progressBar1
            // 
            resources.ApplyResources(this.progressBar1, "progressBar1");
            this.progressBar1.Name = "progressBar1";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // AddRowButton
            // 
            resources.ApplyResources(this.AddRowButton, "AddRowButton");
            this.AddRowButton.Name = "AddRowButton";
            this.AddRowButton.UseVisualStyleBackColor = false;
            this.AddRowButton.Click += new System.EventHandler(this.AddRowButton_Click);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DBAlreadyExists);
            this.Controls.Add(this.SelectFolderSync);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ChooseFolderTable);
            this.Controls.Add(this.LabelFolderSyncPath);
            this.Controls.Add(this.RemoveRowButton);
            this.Controls.Add(this.AddRowButton);
            this.Controls.Add(this.ScanningButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ChooseFolderTable.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ScanningButton;
        private System.Windows.Forms.TableLayoutPanel ChooseFolderTable;
        private System.Windows.Forms.Button SelectFolderButton;
        private System.Windows.Forms.Label LabelFolderScanPath;
        private System.Windows.Forms.Button SelectFolderSync;
        private System.Windows.Forms.Label LabelFolderSyncPath;
        private System.Windows.Forms.Button RemoveRowButton;
        private System.Windows.Forms.Button DBAlreadyExists;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button AddRowButton;
    }
}