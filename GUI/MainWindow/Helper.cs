using Microsoft.Win32;
using ORMDatabaseModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoManager.GUI.MainWindow
{
    public static class Helper
    {
        public static void ShowYears(TreeView AlbumList)
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
        }

        public static void checkRegistry()
        {
            RegistryKey currentUser = Registry.CurrentUser;
            RegistryKey registry = currentUser.OpenSubKey("appPhotoOrginizer");
            if (registry == null)
            {
                throw new Exception();
            }

            var FolderSync = registry.GetValue("FolderSync");
            if (FolderSync == null)
            {
                throw new Exception();
            }
        }

        public static void LoadMainWindow(TreeView album)
        {
            try
            {
                checkRegistry();
                ShowYears(album);
            }
            catch (Exception)
            {
                var s = new GUI.ChooseDirectoryToSync.Form1();
                s.ShowDialog();
                LoadMainWindow(album);
            }
        }


    }
}
