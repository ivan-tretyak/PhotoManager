using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ORMDatabaseModule;
using Microsoft.Win32;
using System.Threading.Tasks;

namespace PhotoManager
{
    public partial class MainWindow: Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            GUI.MainWindow.Helper.LoadMainWindow(AlbumList);
            splitContainer1.Cursor = Cursors.Default;
            splitContainer2.Cursor = Cursors.Default;
            AlbumList.SelectedNode = null;
            PathList.Size = new Size(PathList.Size.Width, AlbumList.Size.Height);
            PathList.Columns[0].Width = -2;
            comboBox1.Visible = false;
            MoveButton.Visible = false;
            CopyButton.Visible = false;
        }

        private void AlbumList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            comboBox1.Items.Clear();
            PathList.SelectedItems.Clear();
            foreach (TreeNode item in AlbumList.Nodes)
            {
                if (item != AlbumList.SelectedNode && AlbumList.SelectedNode.Level == 0)
                    comboBox1.Items.Add(item.Text);
                else if (item != AlbumList.SelectedNode.Parent && AlbumList.SelectedNode.Level == 1)
                    comboBox1.Items.Add(item.Text);
                else if (AlbumList.SelectedNode.Level == 2)
                {
                    if (item != AlbumList.SelectedNode.Parent.Parent)
                        comboBox1.Items.Add(item.Text);
                }
            }
            PathList.Items.Clear();
            var selectedAlbum = AlbumList.SelectedNode.Text;
            if (PreviewImage.Image is not null)
                PreviewImage.Image = null;
            ShowPathFromAlbum(selectedAlbum);
        }

        private void ShowPathFromAlbum(string selectedAlbum)
        {
            using (var db = new DatabaseContext())
            {
                List<string> paths = new List<string>();
                if (AlbumList.SelectedNode.Level == 0)
                {
                    List<int> photoId = GetPhotoFromAlbumByName(GetAlbumName(), db);

                    paths = db.Photos
                        .Where(p => photoId.Contains(p.PhotoId))
                        .Select(p => p.Path)
                        .ToList();
                }
                else if (AlbumList.SelectedNode.Level == 1)
                {
                    List<int> photoId = GetPhotoFromAlbumByName(GetAlbumName(), db);

                    string year = NormalizeYear(AlbumList.SelectedNode.Text);

                    paths = db.Photos
                        .Where(p => photoId.Contains(p.PhotoId))
                        .Where(p => p.MetaData.DateCreation.Contains(year))
                        .Select(p => p.Path)
                        .ToList();
                }
                else if (AlbumList.SelectedNode.Level == 2)
                {
                    List<int> photoId = GetPhotoFromAlbumByName(GetAlbumName(), db);

                    string year = NormalizeYear(AlbumList.SelectedNode.Parent.Text);

                    paths = db.keyWordsLists
                        .Where(kwl => photoId.Contains(kwl.PhotoId))
                        .Where(kwl => kwl.Photo.MetaData.DateCreation.Contains(year))
                        .Where(kwl => kwl.KeyWords.KeyWord == AlbumList.SelectedNode.Text)
                        .Select(kwl => kwl.Photo.Path)
                        .ToList();
                }
                AddPathFromAlbum(paths);
            }
        }

        private static List<int> GetPhotoFromAlbumByName(string selectedAlbum, DatabaseContext db)
        {
            return db.AlbumContexts
                     .Where(al => al.Album.Name == selectedAlbum)
                     .Select(al => al.PhotoId)
                     .ToList();
        }

        private string NormalizeYear(string year)
        {
            int nullYear = 4 - year.Length;
            string[] nullYearsString = new string[] { "", "0", "00", "000" };
            year = $"{nullYearsString[nullYear]}{year}";
            return year;
        }

        private void AddPathFromAlbum(List<string> paths)
        {
            foreach (var path in paths)
            {
                PathList.Items.Add(path);
            }
        }

        private void PathList_SizeChanged(object sender, EventArgs e)
        {
            PathList.Size = new Size(PathList.Size.Width, AlbumList.Size.Height);
            PathList.Columns[0].Width = PathList.Width;
        }

        List<int> SelIndex = new List<int>();
        private void PathList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedNode = AlbumList.SelectedNode;
            // Очистка списка
            if (PathList.SelectedItems.Count == 0) SelIndex.Clear();
            AlbumList.SelectedNode = selectedNode;
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.Visible = true;
                comboBox1.Items.Clear();
                foreach (TreeNode item in AlbumList.Nodes)
                {
                    if (item != AlbumList.SelectedNode && AlbumList.SelectedNode.Level == 0)
                        comboBox1.Items.Add(item.Text);
                    else if (item != AlbumList.SelectedNode.Parent && AlbumList.SelectedNode.Level == 1)
                        comboBox1.Items.Add(item.Text);
                    else if (AlbumList.SelectedNode.Level == 2)
                    {
                        if (item != AlbumList.SelectedNode.Parent.Parent)
                            comboBox1.Items.Add(item.Text);
                    }
                }
                comboBox1.SelectedItem = comboBox1.Items[0];
            }
            this.Update();
        }

        private void PathList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected) SelIndex.Add(e.ItemIndex);
            else SelIndex.Remove(e.ItemIndex);

            if (SelIndex.Count > 0)
            {
                RegistryKey currentUser = Registry.CurrentUser;
                RegistryKey registry = currentUser.OpenSubKey("appPhotoOrginizer");
                var pathToSearchKey = registry.GetValue("FolderSync");
                var pathToSearch = pathToSearchKey.ToString();

                var path = $"{pathToSearch}{System.IO.Path.DirectorySeparatorChar}{PathList.Items[SelIndex[^1]].Text}";

                PreviewImage.SizeMode = PictureBoxSizeMode.Zoom;
                var image = new Bitmap(path);
                PreviewImage.Image = (Image)image;
            }

            else
            {
                PreviewImage.Image = null;
            }
        }

        private void CreateAlbumButton_Click(object sender, EventArgs e)
        {
            var albumCreatorWindow = new PhotoManager.GUI.AlbumCreator.Form1();
            albumCreatorWindow.ShowDialog();
            AlbumList.Nodes.Add(albumCreatorWindow.Name);
        }

        private void PathList_DragItem(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MoveButton.Visible = true;
            CopyButton.Visible = true;
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            using (var db = new DatabaseContext())
            {
                foreach (ListViewItem path in PathList.SelectedItems)
                {
                    var photo = db.Photos
                        .Where(x => x.Path == path.Text)
                        .First();
                    var albumContext = new AlbumContext();
                    var albumName = comboBox1.SelectedItem.ToString();
                    var album = db.Albums
                        .Where(x => x.Name == albumName)
                        .First();
                    var exists = db.AlbumContexts
                        .Where(x => x.Album == album)
                        .Where(x => x.Photo == photo)
                        .FirstOrDefault();
                    if (exists == null)
                    {
                        albumContext.Album = album;
                        albumContext.Photo = photo;
                        db.Add(albumContext);
                        db.SaveChanges();
                    }
                }
            }
            foreach (TreeNode node in AlbumList.Nodes)
            {
                if (node.Text == comboBox1.SelectedItem.ToString())
                {
                    AlbumList.SelectedNode = node;
                    comboBox1.Visible = false;
                    CopyButton.Visible=false;
                    MoveButton.Visible=false;
                    return;
                }
            }
        }

        private void MoveButton_Click(object sender, EventArgs e)
        {
            using (var db = new DatabaseContext())
            {
                foreach (ListViewItem path in PathList.SelectedItems)
                {
                    var photo = db.Photos
                        .Where(x => x.Path == path.Text)
                        .First();
                    var newAlbumName = comboBox1.SelectedItem.ToString();
                    var oldAlubmName = GetAlbumName();
                    var oldalbum = db.Albums
                        .Where(x => x.Name == oldAlubmName)
                        .First();
                    var newalbum = db.Albums
                        .Where(x => x.Name == newAlbumName)
                        .First();
                    var exists = db.AlbumContexts
                        .Where(x => x.Album == oldalbum)
                        .Where(x => x.Photo == photo)
                        .FirstOrDefault();;
                    db.Remove(exists);
                    var newExists = new AlbumContext();
                    newExists.Album = newalbum;
                    newExists.Photo = photo;
                    db.Add(newExists);
                    db.SaveChanges();
                }
            }
            PathList.Items.Clear();
            var selectedAlbum = AlbumList.SelectedNode.Text;
            if (PreviewImage.Image is not null)
                PreviewImage.Image = null;
            ShowPathFromAlbum(selectedAlbum);
        }

        private string GetAlbumName()
        {
            switch (AlbumList.SelectedNode.Level)
            {
                case 0:
                    return AlbumList.SelectedNode.Text;
                case 1:
                    return AlbumList.SelectedNode.Parent.Text;
                case 2:
                    return AlbumList.SelectedNode.Parent.Parent.Text;
                default:
                    return null;
            }
        }

        private void PathList_DoubleClick(object sender, EventArgs e)
        {
            using (var db = new DatabaseContext())
            {
                var paths = new List<string>();
                List<int> photoId = GetPhotoFromAlbumByName(GetAlbumName(), db);

                paths = db.Photos
                    .Where(p => photoId.Contains(p.PhotoId))
                    .Select(p => p.Path)
                    .ToList();
                GUI.ShowImage.Form1 showImage = new GUI.ShowImage.Form1(paths, PathList.SelectedIndices[0]);
                showImage.ShowDialog();
            }
        }

        private async void PreviewImage_DoubleClick(object sender, EventArgs e)
        {
            Clipboard.SetImage(PreviewImage.Image);
            await Task.Delay(250);
        }
    }
}
