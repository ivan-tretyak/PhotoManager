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
            this.ImageListForAlbum = new System.Windows.Forms.ListView();
            this.CreateAlbumButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.moveAlbumBox = new System.Windows.Forms.ComboBox();
            this.moveButton = new System.Windows.Forms.Button();
            this.CopyButton = new System.Windows.Forms.Button();
            this.RemoveButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.AlbumList = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // ImageListForAlbum
            // 
            resources.ApplyResources(this.ImageListForAlbum, "ImageListForAlbum");
            this.ImageListForAlbum.HideSelection = false;
            this.ImageListForAlbum.Name = "ImageListForAlbum";
            this.ImageListForAlbum.UseCompatibleStateImageBehavior = false;
            this.ImageListForAlbum.SelectedIndexChanged += new System.EventHandler(this.ImageListForAlbum_SelectedIndexChanged);
            this.ImageListForAlbum.DoubleClick += new System.EventHandler(this.ImageListForAlbum_DoubleClick);
            // 
            // CreateAlbumButton
            // 
            resources.ApplyResources(this.CreateAlbumButton, "CreateAlbumButton");
            this.CreateAlbumButton.Name = "CreateAlbumButton";
            this.CreateAlbumButton.UseVisualStyleBackColor = true;
            this.CreateAlbumButton.Click += new System.EventHandler(this.CreateAlbumButton_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // moveAlbumBox
            // 
            resources.ApplyResources(this.moveAlbumBox, "moveAlbumBox");
            this.moveAlbumBox.FormattingEnabled = true;
            this.moveAlbumBox.Name = "moveAlbumBox";
            this.moveAlbumBox.SelectedIndexChanged += new System.EventHandler(this.moveAlbumBox_SelectedIndexChanged);
            // 
            // moveButton
            // 
            resources.ApplyResources(this.moveButton, "moveButton");
            this.moveButton.Name = "moveButton";
            this.moveButton.UseVisualStyleBackColor = true;
            this.moveButton.Click += new System.EventHandler(this.moveButton_Click);
            // 
            // CopyButton
            // 
            resources.ApplyResources(this.CopyButton, "CopyButton");
            this.CopyButton.Name = "CopyButton";
            this.CopyButton.UseVisualStyleBackColor = true;
            this.CopyButton.Click += new System.EventHandler(this.CopyButton_Click);
            // 
            // RemoveButton
            // 
            resources.ApplyResources(this.RemoveButton, "RemoveButton");
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.UseVisualStyleBackColor = true;
            this.RemoveButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // AlbumList
            // 
            resources.ApplyResources(this.AlbumList, "AlbumList");
            this.AlbumList.ItemHeight = 15;
            this.AlbumList.Name = "AlbumList";
            this.AlbumList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.AlbumList_SelectedIndexChanged);
            // 
            // MainWindow
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.AlbumList);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.RemoveButton);
            this.Controls.Add(this.CopyButton);
            this.Controls.Add(this.moveButton);
            this.Controls.Add(this.moveAlbumBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CreateAlbumButton);
            this.Controls.Add(this.ImageListForAlbum);
            this.DoubleBuffered = true;
            this.Name = "MainWindow";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.ListView ImageListForAlbum;
        public System.Windows.Forms.Button CreateAlbumButton;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox moveAlbumBox;
        public System.Windows.Forms.Button moveButton;
        public System.Windows.Forms.Button CopyButton;
        public System.Windows.Forms.Button RemoveButton;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.TreeView AlbumList;
    }
}

