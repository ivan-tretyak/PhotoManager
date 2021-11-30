
namespace PhotoManager
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.AlbumList = new System.Windows.Forms.ListBox();
            this.ImageListForAlbum = new System.Windows.Forms.ListView();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CreateAlbumButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AlbumList
            // 
            resources.ApplyResources(this.AlbumList, "AlbumList");
            this.AlbumList.FormattingEnabled = true;
            this.AlbumList.Name = "AlbumList";
            this.AlbumList.SelectedIndexChanged += new System.EventHandler(this.AlbumList_SelectedIndexChanged);
            // 
            // ImageListForAlbum
            // 
            resources.ApplyResources(this.ImageListForAlbum, "ImageListForAlbum");
            this.ImageListForAlbum.HideSelection = false;
            this.ImageListForAlbum.Name = "ImageListForAlbum";
            this.ImageListForAlbum.UseCompatibleStateImageBehavior = false;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox1, "comboBox1");
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // CreateAlbumButton
            // 
            resources.ApplyResources(this.CreateAlbumButton, "CreateAlbumButton");
            this.CreateAlbumButton.Name = "CreateAlbumButton";
            this.CreateAlbumButton.UseVisualStyleBackColor = true;
            this.CreateAlbumButton.Click += new System.EventHandler(this.CreateAlbumButton_Click);
            // 
            // MainWindow
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CreateAlbumButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.ImageListForAlbum);
            this.Controls.Add(this.AlbumList);
            this.Name = "MainWindow";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox AlbumList;
        private System.Windows.Forms.ListView ImageListForAlbum;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button CreateAlbumButton;
    }
}

