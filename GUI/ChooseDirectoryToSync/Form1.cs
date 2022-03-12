using IndexingModule;
using Microsoft.Win32;
using ORMDatabaseModule;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PhotoManager.GUI.ChooseDirectoryToSync
{
    public partial class Form1 : Form
    {
        Helper helper = new();
        public Form1()
        {
            InitializeComponent();
            helper.HideElement(RemoveRowButton);
            helper.HideElement(AddRowButton);
            helper.HideElement(ScanningButton);
            helper.HideElement(ChooseFolderTable);
            helper.HideElement(DBAlreadyExists);
        }

        private void SelectFolderSync_Click(object sender, EventArgs e)
        {
            String result = helper.ShowFolderBrowserDialog();

            if (result.Length != 0)
            {
                string[] dbFiles = Directory.GetFiles(result, "*.db");
                if (dbFiles.Length > 0)
                {
                    helper.ShowElement(DBAlreadyExists);
                    LabelFolderSyncPath.Text = result;
                }
                else
                {
                    LabelFolderSyncPath.Text = result;
                    helper.ShowElement(ChooseFolderTable);
                    if (ChooseFolderTable.GetControlFromPosition(0, 0).Text != "")
                    {
                        helper.ShowElement(AddRowButton);
                    }
                }
            }
        }

        private void AddRowButton_Click(object sender, EventArgs e)
        {
            Button buttton = helper.AddRowTable(ChooseFolderTable);
            buttton.Click += new EventHandler(this.SelectFolderButton_Click);
            helper.HideElement(AddRowButton);
            helper.HideElement(ScanningButton);
            helper.ShowElement(RemoveRowButton);
            this.Refresh();
        }

        private void SelectFolderButton_Click(object sender, EventArgs e)
        {
            String result = helper.ShowFolderBrowserDialog();
            if ((Control)sender != null && result.Length != 0)
            {
                int row = ChooseFolderTable.GetPositionFromControl((Control)sender).Row;
                int column = ChooseFolderTable.GetPositionFromControl((Control)sender).Column;
                ChooseFolderTable.GetControlFromPosition(column - 1, row).Text = result;
                helper.ShowElement(AddRowButton);
                helper.ShowElement(ScanningButton);
            }
        }

        private void RemoveRowButton_Click(object sender, EventArgs e)
        {
            helper.RemoveRowTable(ChooseFolderTable);
            if (ChooseFolderTable.RowCount == 1)
            {
                helper.HideElement(RemoveRowButton);
                helper.ShowElement(AddRowButton);
            }
            helper.ShowElement(ScanningButton);
        }

        private void ScanningButton_Click(object sender, EventArgs e)
        {
            //Папка синхронизации
            string pathSync = LabelFolderSyncPath.Text;

            //Запись в реестр о выбранном месте сохранения
            RegistryKey key = Registry.CurrentUser;
            RegistryKey appPhotoOrginizer = key.CreateSubKey("appPhotoOrginizer");
            appPhotoOrginizer.SetValue("FolderSync", pathSync);
            appPhotoOrginizer.Close();

            //Создание нового альбома
            var album = new Album();
            album.Name = "Common";
            album.DateCreation = DateTime.Now.ToString();

            //Сохраним альбом в базу данных
            using (DatabaseContext db = new())
            {
                var b = db.Database.EnsureCreated();
                db.Add(album);
                db.SaveChanges();
            }

            //Получим записанный альбом
            var albums = new Album();
            using (DatabaseContext db = new())
            {
                albums = db.Albums.First();
            }

            //Начнем индексацию
            Indexing indexing = new();
            for (int i = 0; i < ChooseFolderTable.RowCount; i++)
            {
                  
                string path = ChooseFolderTable.Controls[i * 2].Text;
                var res = indexing.IndexingDirectory(path);

                foreach (Image image in res)
                {
                    //Создаем новое фото
                    Photo p = new();

                    //Создаем новые метаданные
                    ORMDatabaseModule.MetaData m = new();
                    Random rnd = new Random();
                    //Копируем файл в папку синхронизации
                    string filename = $"{helper.Prefix(rnd.Next(10, 15))}_{Path.GetFileName(image.path)}";
                    string destPath = $"{pathSync}{Path.DirectorySeparatorChar}{filename}";
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
                    albumContext.Album = albums;
                    albumContext.Photo = p;

                    //Сохряняем фото в базе
                    using (var db = new DatabaseContext())
                    {
                        var b = db.Database.EnsureCreated();
                        db.AlbumContexts.Add(albumContext);
                        db.Albums.Attach(albums);
                        db.SaveChanges();
                    }
                }
            }
        }

        private void DBAlreadyExists_Click(object sender, EventArgs e)
        {
            //Папка синхронизации
            string pathSync = LabelFolderSyncPath.Text;

            //Запись в реестр о выбранном месте сохранения
            RegistryKey key = Registry.CurrentUser;
            RegistryKey appPhotoOrginizer = key.CreateSubKey("appPhotoOrginizer");
            appPhotoOrginizer.SetValue("FolderSync", pathSync);
            appPhotoOrginizer.Close();

            this.Close();
        }
    }
}
