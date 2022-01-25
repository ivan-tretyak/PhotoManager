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
using System.Collections.Generic;
using System.Windows.Forms;
using PhotoManager.GUI.MainWindow;


namespace PhotoManager
{
    public partial class MainWindow : Form
    {
        HelperMainWindow helper;
        DbHelper dbHelper;
        public MainWindow()
        {
            helper = new(this);
            dbHelper = new();
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            helper.showData();
        }

        private void CreateAlbumButton_Click(object sender, EventArgs e)
        {
            var s = new GUI.AlbumCreator.Form1();
            s.ShowDialog();
            helper.showData();
        }

        private void ImageListForAlbum_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ImageListForAlbum.SelectedItems.Count != 0)
            {
                this.label2.Visible = true;
                this.moveAlbumBox.Visible = true;
                this.RemoveButton.Visible = true;
                this.moveAlbumBox.Items.Clear();
                for(int i = 0; i < AlbumList.Nodes.Count; i++)
                {
                    this.moveAlbumBox.Items.Add(AlbumList.Nodes[i].Text);
                }
            }
            else
            {
                this.label2.Visible = false;
                this.moveAlbumBox.Visible = false;
            }
        }

        private void moveAlbumBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (moveAlbumBox.SelectedItem != null)
            {
                moveButton.Visible = true;
                CopyButton.Visible = true;
                this.RemoveButton.Visible = true;
            }
            else
            {
                moveButton.Visible = false;
                CopyButton.Visible = false;
                this.RemoveButton.Visible = true;
            }
        }

        private void moveButton_Click(object sender, EventArgs e)
        {
            try
            {
                var newAlbum = this.moveAlbumBox.SelectedItem;
                var oldAlbum = this.AlbumList.SelectedNode.Text;
                foreach (int index in this.ImageListForAlbum.SelectedIndices)
                {
                    var newAl = newAlbum.ToString();
                    var oldAl = oldAlbum.ToString();
                    var path = this.ImageListForAlbum.Items[index].Text.ToString();
                    dbHelper.MoveToAnotherAlbum(newAl, oldAl, path);
                }
                this.label2.Visible = false;
                this.moveAlbumBox.Visible = false;
                this.moveButton.Visible = false;
                this.CopyButton.Visible = false;
                this.RemoveButton.Visible = false;
                helper.ShowPhotoFromAlbum(oldAlbum.ToString());
                helper.showData();
            }
            catch (Exception)
            {
                label3.Text = MainWindowStrings.Error;
            }
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            try
            {
                var newAlbum = this.moveAlbumBox.SelectedItem;
                foreach (int index in this.ImageListForAlbum.SelectedIndices)
                {
                    var albumNew = newAlbum.ToString();
                    var path = this.ImageListForAlbum.Items[index].Text.ToString();
                    dbHelper.CopyToAnotherAlbum(albumNew, path);
                }
                this.label2.Visible = false;
                this.moveAlbumBox.Visible = false;
                this.moveButton.Visible = false;
                this.CopyButton.Visible = false;
                this.RemoveButton.Visible = false;
                helper.ShowPhotoFromAlbum(this.AlbumList.SelectedNode.ToString());
                helper.showData();
            }
            catch (Exception)
            {
                label3.Text = MainWindowStrings.Error;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var album = this.AlbumList.SelectedNode.ToString();
            foreach(int index in this.ImageListForAlbum.SelectedIndices)
            {
                var path = this.ImageListForAlbum.Items[index].Text.ToString();
                dbHelper.Remove(album, path);
            }
            helper.ShowPhotoFromAlbum(this.AlbumList.SelectedNode.ToString());
        }

        private void ImageListForAlbum_DoubleClick(object sender, EventArgs e)
        {
            List<string> paths = new();
            for (int i = 0; i < ImageListForAlbum.Items.Count; i++)
            {
                paths.Add(ImageListForAlbum.Items[i].Text.ToString());
            }
            var show = new PhotoManager.GUI.ShowImage.Form1(paths, AlbumList.SelectedNode.ToString(), ImageListForAlbum.SelectedIndices[0]);
            show.ShowDialog();
        }

        private void AlbumList_SelectedIndexChanged(object sender, TreeViewEventArgs e)
        {
            if (AlbumList.SelectedNode != null && AlbumList.SelectedNode.Parent == null)
            {
                helper.ShowPhotoFromAlbum(AlbumList.SelectedNode.Text);
                this.label2.Visible = false;
                this.moveAlbumBox.Visible = false;
                this.moveButton.Visible = false;
                this.CopyButton.Visible = false;
                this.Text = $"{MainWindowStrings.Album}: {AlbumList.SelectedNode.Text}";
            }
            if (AlbumList.SelectedNode != null && AlbumList.SelectedNode.Parent != null)
            {
                helper.ShowPhotoFromAlbum(AlbumList.SelectedNode.Text, AlbumList.SelectedNode.Parent.Text);
            }
        }
    }
}
