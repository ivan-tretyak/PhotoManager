
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
            this.SuspendLayout();
            // 
            // AlbumList
            // 
            this.AlbumList.FormattingEnabled = true;
            resources.ApplyResources(this.AlbumList, "AlbumList");
            this.AlbumList.Name = "AlbumList";
            // 
            // ImageListForAlbum
            // 
            this.ImageListForAlbum.HideSelection = false;
            resources.ApplyResources(this.ImageListForAlbum, "ImageListForAlbum");
            this.ImageListForAlbum.Name = "ImageListForAlbum";
            this.ImageListForAlbum.UseCompatibleStateImageBehavior = false;
            // 
            // MainWindow
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ImageListForAlbum);
            this.Controls.Add(this.AlbumList);
            this.Name = "MainWindow";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox AlbumList;
        private System.Windows.Forms.ListView ImageListForAlbum;
    }
}

