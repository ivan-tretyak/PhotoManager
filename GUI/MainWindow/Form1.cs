using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ORMDatabaseModule;
using Microsoft.Win32;

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
            using (var db = new DatabaseContext())
            {
                var albums = db.Albums.ToList();
                for (int i = 0; i < albums.Count(); i++)
                {
                    var albumContexts = db.AlbumContexts
                        .Where(al => al.AlbumId == albums[i].AlbumId)
                        .Select(al => al.PhotoId)
                        .ToList();
                    var idMetadatas = db.Photos
                        .Where(p => albumContexts.Contains(p.PhotoId))
                        .Select(p => p.MetaDataId)
                        .ToList();
                    var years = db.MetaDatas
                        .Where(m => idMetadatas.Contains(m.MetadataId))
                        .Select(m => DateTime.Parse(m.DateCreation).Year.ToString())
                        .ToList();
                    years = years.Distinct().ToList();
                    AlbumList.Nodes.Add(albums[i].Name);
                    for (int j = 0; j < years.Count(); j++)
                    {
                        AlbumList.Nodes[i].Nodes.Add(years[j]);
                        var keyWordsList = db.keyWordsLists
                            .Where(kw => albumContexts.Contains(kw.PhotoId))
                            .Select(kw => kw.KeyWords.KeyWord)
                            .ToList();
                        keyWordsList = keyWordsList.Distinct().ToList();
                        foreach (var keyWord in keyWordsList)
                        {
                            AlbumList.Nodes[i].Nodes[j].Nodes.Add(keyWord);
                        }
                    }
                }
            }
            splitContainer1.Cursor = Cursors.Default;
            splitContainer2.Cursor = Cursors.Default;
            AlbumList.SelectedNode = null;
            PathList.Size = new Size(PathList.Size.Width, AlbumList.Size.Height);
            PathList.Columns[0].Width = -2;
        }

        private void AlbumList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            PathList.Items.Clear();
            var selectedAlbum = AlbumList.SelectedNode.Text;
            if (PreviewImage.Image is not null)
                PreviewImage.Image = null;
            using (var db = new DatabaseContext())
            {
                if (AlbumList.SelectedNode.Level == 0)
                {
                    List<int> photoId = GetPhotoFromAlbumByName(selectedAlbum, db);

                    var paths = db.Photos
                        .Where(p => photoId.Contains(p.PhotoId))
                        .Select(p => p.Path)
                        .ToList();
                    AddPathFromAlbum(paths);
                }
                else if (AlbumList.SelectedNode.Level == 1)
                {
                    List<int> photoId = GetPhotoFromAlbumByName(AlbumList.SelectedNode.Parent.Text, db);

                    string year = NormalizeYear(AlbumList.SelectedNode.Text);

                    var paths = db.Photos
                        .Where(p => photoId.Contains(p.PhotoId))
                        .Where(p => p.MetaData.DateCreation.Contains(year))
                        .Select(p => p.Path)
                        .ToList();
                    AddPathFromAlbum(paths);
                }
                else if (AlbumList.SelectedNode.Level == 2)
                {
                    List<int> photoId = GetPhotoFromAlbumByName(AlbumList.SelectedNode.Parent.Parent.Text, db);

                    string year = NormalizeYear(AlbumList.SelectedNode.Parent.Text);

                    var paths = db.keyWordsLists
                        .Where(kwl => photoId.Contains(kwl.PhotoId))
                        .Where(kwl => kwl.Photo.MetaData.DateCreation.Contains(year))
                        .Where(kwl => kwl.KeyWords.KeyWord == AlbumList.SelectedNode.Text)
                        .Select(kwl => kwl.Photo.Path)
                        .ToList();
                    AddPathFromAlbum(paths);
                }
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
            // Очистка списка
            if (PathList.SelectedItems.Count == 0) SelIndex.Clear();
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
    }
}
