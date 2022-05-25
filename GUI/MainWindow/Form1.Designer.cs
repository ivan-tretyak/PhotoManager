
namespace PhotoManager
{
    partial class MainWindow
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.AlbumList = new System.Windows.Forms.TreeView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.PathList = new System.Windows.Forms.ListView();
            this.Path = new System.Windows.Forms.ColumnHeader();
            this.PreviewImage = new System.Windows.Forms.PictureBox();
            this.CreateAlbumButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PreviewImage)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.splitContainer1.Location = new System.Drawing.Point(12, 40);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.AlbumList);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(10);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(776, 398);
            this.splitContainer1.SplitterDistance = 214;
            this.splitContainer1.SplitterWidth = 10;
            this.splitContainer1.TabIndex = 0;
            // 
            // AlbumList
            // 
            this.AlbumList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AlbumList.Location = new System.Drawing.Point(0, 0);
            this.AlbumList.Name = "AlbumList";
            this.AlbumList.Size = new System.Drawing.Size(212, 396);
            this.AlbumList.TabIndex = 0;
            this.AlbumList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.AlbumList_AfterSelect);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.PathList);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.PreviewImage);
            this.splitContainer2.Size = new System.Drawing.Size(531, 395);
            this.splitContainer2.SplitterDistance = 177;
            this.splitContainer2.SplitterWidth = 10;
            this.splitContainer2.TabIndex = 0;
            // 
            // PathList
            // 
            this.PathList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PathList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Path});
            this.PathList.HideSelection = false;
            this.PathList.Location = new System.Drawing.Point(0, 1);
            this.PathList.Name = "PathList";
            this.PathList.Size = new System.Drawing.Size(174, 396);
            this.PathList.TabIndex = 2;
            this.PathList.UseCompatibleStateImageBehavior = false;
            this.PathList.View = System.Windows.Forms.View.Details;
            this.PathList.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.PathList_ItemSelectionChanged);
            this.PathList.SelectedIndexChanged += new System.EventHandler(this.PathList_SelectedIndexChanged);
            this.PathList.Resize += new System.EventHandler(this.PathList_SizeChanged);
            // 
            // Path
            // 
            this.Path.Tag = "Path";
            this.Path.Text = "Path";
            this.Path.Width = 173;
            // 
            // PreviewImage
            // 
            this.PreviewImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PreviewImage.Location = new System.Drawing.Point(6, 0);
            this.PreviewImage.Name = "PreviewImage";
            this.PreviewImage.Size = new System.Drawing.Size(344, 393);
            this.PreviewImage.TabIndex = 0;
            this.PreviewImage.TabStop = false;
            // 
            // CreateAlbumButton
            // 
            this.CreateAlbumButton.Location = new System.Drawing.Point(12, 11);
            this.CreateAlbumButton.Name = "CreateAlbumButton";
            this.CreateAlbumButton.Size = new System.Drawing.Size(212, 23);
            this.CreateAlbumButton.TabIndex = 1;
            this.CreateAlbumButton.Text = "Create Album";
            this.CreateAlbumButton.UseVisualStyleBackColor = true;
            this.CreateAlbumButton.Click += new System.EventHandler(this.CreateAlbumButton_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.CreateAlbumButton);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainWindow";
            this.Text = "Main Window";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PreviewImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView AlbumList;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListView PathList;
        private System.Windows.Forms.PictureBox PreviewImage;
        private System.Windows.Forms.ColumnHeader Path;
        private System.Windows.Forms.Button CreateAlbumButton;
    }
}