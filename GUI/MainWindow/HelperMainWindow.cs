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
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ORMDatabaseModule;


namespace PhotoManager
{
    public class HelperMainWindow
    {
        public HelperMainWindow()
        {

        }

       public void showData(TreeView AlbumList)
        {
           if (AlbumList.Nodes.Count != 0)
            {
                AlbumList.Nodes.Clear();
            }
            try
            {
                using (var db = new DatabaseContext())
                {
                    if (AlbumList.Nodes.Count != 0)
                    {
                        AlbumList.Nodes.Clear();
                    }
                    //Selected all albums
                    var albums = db.Albums.ToList();

                    //Show albums
                    foreach(var album in albums)
                    {
                        AlbumList.Nodes.Add(album.Name);
                    }
                }
                this.addYears(AlbumList);
            }
            catch(Exception)
            {
                var s = new SelectPathWindows();
                s.ShowDialog();
                showData(AlbumList);
            }
        }

        public void addYears(TreeView treeView)
        {
            //treeView.Items.Clear();
            for (int i = 0; i < treeView.Nodes.Count; i++)
            {
                using (var db = new DatabaseContext())
                {
                    //Get album context
                    var albumContexts = db.AlbumContexts
                        .Where(albumContext => albumContext.Album.Name == treeView.Nodes[i].Text)
                        .ToList();

                    //Get photo
                    List<Photo> photos = new();
                    foreach (var albumContext in albumContexts)
                    {
                        photos.Add(db.Photos
                            .Where(photo => photo.PhotoId == albumContext.PhotoId)
                            .First());
                    }

                    //Select metadata
                    List<MetaData> metadatas = new();
                    foreach (var photo in photos)
                    {
                        metadatas.Add(db.MetaDatas
                            .Where(metadata => metadata.MetadataId == photo.MetaDataId)
                            .First());
                    }

                    //Get years
                    List<string> years = new();

                    foreach (var metadata in metadatas)
                    {
                        var date = DateTime.Parse(metadata.DateCreation);
                        years.Add(date.Year.ToString());
                    }

                    var uniqueYears = years.Distinct();

                    foreach (var year in uniqueYears)
                    {
                        treeView.Nodes[i].Nodes.Add(year);
                    }
                }

            }
        }

        public void ShowPhotoFromAlbum(ListView ImageList, string albumName)
        {
            using (var db = new DatabaseContext())
            {
                //Get album context
                var albumsContexts = db.AlbumContexts
                   .Where(b => b.Album.Name == albumName)
                   .ToList();

                List<Photo> photos = new();

                foreach (var albumContext in albumsContexts)
                {
                    photos.Add(db.Photos.Where(photo => photo.PhotoId == albumContext.PhotoId).First());
                }
                DisplayImage(ImageList, photos);
            }

        }

        private static async void DisplayImage(ListView ImageList, List<Photo> photos)
        {
            int count = 0;
            //Delete LargeImageList
            if (ImageList.LargeImageList != null)
            {
                ImageList.LargeImageList.Dispose();
            }
            ImageList.Items.Clear();
            //Create image list
            ImageList.View = View.LargeIcon;
            var LargeImageList = new ImageList();
            LargeImageList.ImageSize = new Size(256, 256);
            LargeImageList.ColorDepth = ColorDepth.Depth32Bit;

            ImageList.UseWaitCursor = true;

            foreach (var photo in photos)
            {
                ImageList.BeginUpdate();
                try
                {
                    //Create photo preview
                    //Get orientation
                    int orientation = 0;
                    using (var db = new DatabaseContext())
                    {
                        var metadata = db.MetaDatas
                            .Where(m => m.MetadataId == photo.MetaDataId)
                            .First();
                        orientation = metadata.Orientation;
                    }
                    var i = new ImagesPreview(photo.Path, orientation);
                    var thumbnail = await Task.Run(() => i.thumbnail());
                    ImageList.BeginUpdate();
                    ImageList.Items.Add(photo.Path, count);
                    ImageList.EndUpdate();
                    count++;
                    LargeImageList.Images.Add(photo.Path, thumbnail);
                    if (photo.Exist == 1)
                    {
                        UpdateExist(photo, 0);
                    }
                }
                catch (Exception)
                {
                    UpdateExist(photo, 1);
                }
                ImageList.EndUpdate();
            }
            ImageList.LargeImageList = LargeImageList;
            ImageList.UseWaitCursor = false;
        }

        private static void UpdateExist(Photo photo, int existFlag)
        {
            using (var db = new DatabaseContext())
            {
                photo.Exist = existFlag;
                db.Photos.Update(photo);
                db.SaveChanges();
            }
        }

        public static void ShowPhotoFromAlbum(string year, string albumName, ListView ImageList)
        {
            year = NormalizeYear(year);
            using (var db = new DatabaseContext())
            {
                //Select album contexts
                var albumContexts = db.AlbumContexts
                    .Where(albumContext => albumContext.Album.Name == albumName && albumContext.Photo.MetaData.DateCreation.Contains(year))
                    .ToList();


                //Select photo
                List<Photo> photos = new();
                foreach (var albumContext in albumContexts)
                {
                    photos.Add(db.Photos.Where(photo => photo.PhotoId == albumContext.PhotoId).First());

                }
                DisplayImage(ImageList, photos);
            }
        }

        public static void MoveToAnotherAlbum(string newAlbumName, string oldAlbumName, string path)
        {
            using (var db = new DatabaseContext())
            {
                //Get album context
                var albumContext = db.AlbumContexts
                    .Where(aC => aC.Album.Name == oldAlbumName && aC.Photo.Path == path)
                    .First();

                //Get destation album
                var album = db.Albums
                    .Where(a => a.Name == newAlbumName)
                    .First();

                //Update
                albumContext.Album = album;
                albumContext.AlbumId = album.AlbumId;
                db.AlbumContexts.Update(albumContext);
                db.SaveChanges();
            }
        }

        public static void CopyToAnotherAlbum(string newAlbumName, string path)
        {
            using (var db = new DatabaseContext())
            {
                //Create new album context
                var albumContext = new AlbumContext();

                //Get destation album
                var album = db.Albums
                    .Where(a => a.Name == newAlbumName)
                    .First();

                //Get photo by path
                var photo = db.Photos
                    .Where(p => p.Path == path)
                    .First();

                //Associate
                albumContext.Album = album;
                albumContext.Photo = photo;
                db.Add(albumContext);
                db.SaveChanges();
            }
        }

        public static string NormalizeYear(string year)
        {
            while(year.Length < 4)
            {
                year = "0" + year;
            }
            return year;
        }

        public void Remove(string album, string path)
        {
            using (var db = new DatabaseContext())
            {
                //Get album context
                var albumContext = db.AlbumContexts
                     .Where(aC => aC.Photo.Path == path && aC.Album.Name == album)
                     .First();

                db.AlbumContexts.Remove(albumContext);
                db.SaveChanges();
            }
        }
    }
}
