using IndexingModule;
using Microsoft.Win32;
using ORMDatabaseModule;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            if (PreviewImage.Image is not null)
                PreviewImage.Image = null;
            ShowPathFromAlbum();
        }

        private void ShowPathFromAlbum()
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
                PreviewImage.Image = (System.Drawing.Image)image;
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
            if (PreviewImage.Image is not null)
                PreviewImage.Image = null;
            ShowPathFromAlbum();
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

        private void addNewFolder(object sender, EventArgs e)
        {
            var choosePath = showFolderBrowserDialog();
            if (choosePath == "")
            {
                return;
            }
            //Начнем индексацию
            Indexing indexing = new();
            var res = indexing.IndexingDirectory(choosePath);

            RegistryKey currentUser = Registry.CurrentUser;
            RegistryKey registry = currentUser.OpenSubKey("appPhotoOrginizer");
            var pathToSearchKey = registry.GetValue("FolderSync");
            var pathSync = pathToSearchKey.ToString();

            progressBar1.Maximum = res.Count;

            foreach (IndexingModule.Image image in res)
            {
                Album album = new();
                using (var db = new DatabaseContext())
                {
                    album = db.Albums
                        .Where(x => x.Name == GetAlbumName())
                        .First();
                }

                //Создаем новое фото
                Photo p = new();

                //Создаем новые метаданные
                ORMDatabaseModule.MetaData m = new();
                Random rnd = new Random();
                //Копируем файл в папку синхронизации
                string filename = $"{prefix(rnd.Next(10, 15))}_{System.IO.Path.GetFileName(image.path)}";
                string destPath = $"{pathSync}{System.IO.Path.DirectorySeparatorChar}{filename}";
                File.Copy(image.path, destPath);

                //Заполняем метаданные
                p.Path = filename;
                m.DateCreation = image.metaData.DateCreation.ToString();
                m.Flash = image.metaData.Flash;
                m.Latitude = image.metaData.Latitude;
                m.Longitude = image.metaData.Longitude;
                m.FocusLength = (float)image.metaData.FocalLength;
                m.Orientation = image.metaData.Orientation;
                m.Model = image.metaData.Model;
                m.Manufacturer = image.metaData.Manufacturer;
                p.MetaData = m;

                //Создаем запись о хранение фото в таком альбоме
                AlbumContext albumContext = new();

                //Ассоциируем фото с альбомом
                albumContext.Album = album;
                albumContext.Photo = p;

                //Сохряняем фото в базе
                using (var db = new DatabaseContext())
                {
                    var b = db.Database.EnsureCreated();
                    db.AlbumContexts.Add(albumContext);
                    db.Albums.Attach(album);
                    db.SaveChanges();
                }
                progressBar1.Value += 1;
            }
            PathList.Items.Clear();
            ShowPathFromAlbum();
            progressBar1.Value = 0;
        }

        private string showFolderBrowserDialog()
        {
            
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                return folderBrowserDialog.SelectedPath;
            }
            else
            {
                return "";
            }
        }

        public string prefix(int lengthPrefix)
        {
            Random random = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXY";
            chars += chars.ToLower();
            return new string(Enumerable.Repeat(chars, lengthPrefix)
        .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
