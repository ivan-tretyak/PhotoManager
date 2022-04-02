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
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using PhotoManager.GUI.MainWindow;


namespace PhotoManager
{
    public partial class MainWindow : Form
    {
        HelperMainWindow helper;
        DbHelper dbHelper;
        TreeNode selected;
        public MainWindow()
        {
            helper = new(this);
            dbHelper = new();
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            //helper.ScanningOnStart();
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
                string oldAlbum;
                if (selected.Level == 2)
                {
                    oldAlbum = selected.Parent.Parent.Text;
                }
                else if (selected.Level == 1)
                {
                    oldAlbum = selected.Parent.Text;
                }
                else
                {
                    oldAlbum = selected.Text;
                }
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
                if (selected.Level == 2)
                {
                    helper.ShowPhotoFromAlbum(selected.Parent.Text, selected.Parent.Parent.Text, selected.Text);
                }
                else if (selected.Level == 1)
                {
                    helper.ShowPhotoFromAlbum(selected.Text, selected.Parent.Text);
                }
                else
                {
                    helper.ShowPhotoFromAlbum(selected.Text);
                }
                
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
            var albumNode = this.AlbumList.SelectedNode;
            string album;
            if (albumNode.Level != 0)
            {
                album = albumNode.Parent.Text;
            }
            else
            {
                album = albumNode.Text;
            }
            foreach(int index in this.ImageListForAlbum.SelectedIndices)
            {
                var path = this.ImageListForAlbum.Items[index].Text.ToString();
                dbHelper.Remove(album, path);
            }
            helper.ShowPhotoFromAlbum(album);
        }

        private void ImageListForAlbum_DoubleClick(object sender, EventArgs e)
        {
            RegistryKey currentUser = Registry.CurrentUser;
            RegistryKey registry = currentUser.OpenSubKey("appPhotoOrginizer");
            string pathToSearch = registry.GetValue("FolderSync").ToString();

            List<string> paths = new();
            for (int i = 0; i < ImageListForAlbum.Items.Count; i++)
            {
                paths.Add($"{pathToSearch}{Path.DirectorySeparatorChar}{ImageListForAlbum.Items[i].Text.ToString()}");
            }
            var show = new PhotoManager.GUI.ShowImage.Form1(paths, AlbumList.SelectedNode.ToString(), ImageListForAlbum.SelectedIndices[0]);
            show.ShowDialog();
            if (selected.Level == 2)
            {
                helper.ShowPhotoFromAlbum(selected.Parent.Text, selected.Parent.Parent.Text, selected.Text);
            }
            else if (selected.Level == 1)
            {
                helper.ShowPhotoFromAlbum(selected.Text, selected.Parent.Text);
            }
            else
            {
                helper.ShowPhotoFromAlbum(selected.Text);
            }
            helper.showData();
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
                if (AlbumList.SelectedNode.Level == 1)
                    helper.ShowPhotoFromAlbum(AlbumList.SelectedNode.Text, AlbumList.SelectedNode.Parent.Text);
                else if (AlbumList.SelectedNode.Level == 2)
                    helper.ShowPhotoFromAlbum(AlbumList.SelectedNode.Parent.Text, AlbumList.SelectedNode.Parent.Parent.Text, AlbumList.SelectedNode.Text);
            }
            selected = AlbumList.SelectedNode;
        }
    }
}
