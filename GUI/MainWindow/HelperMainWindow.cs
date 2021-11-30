﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ORMDatabaseModule;


namespace PhotoManager
{
    public class HelperMainWindow
    {
        public HelperMainWindow()
        {

        }

       public void showData(ListBox AlbumList)
        {
            try
            {
                using (var db = new DatabaseContext())
                {
                    if (AlbumList.Items.Count != 0)
                    {
                        AlbumList.Items.Clear();
                    }
                    //Selected all albums
                    var albums = db.Albums.ToList();

                    //Show albums
                    foreach(var album in albums)
                    {
                        AlbumList.Items.Add(album.Name);
                    }
                }
            }
            catch(Exception)
            {
                var s = new SelectPathWindows();
                s.ShowDialog();
                showData(AlbumList);
            }
        }

        public void addYears(string albumName, ComboBox comboBox)
        {
            comboBox.Items.Clear();
            using (var db = new DatabaseContext())
            {
                //Get album context
                var albumContexts = db.AlbumContexts
                    .Where(albumContext => albumContext.Album.Name == albumName)
                    .ToList();

                //Get photo
                List<Photo> photos = new();
                foreach(var albumContext in albumContexts)
                {
                    photos.Add(db.Photos
                        .Where(photo => photo.PhotoId == albumContext.PhotoId)
                        .First());
                }

                //Select metadata
                List<MetaData> metadatas = new();
                foreach(var photo in photos)
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

                foreach(var year in uniqueYears)
                {
                    comboBox.Items.Add(year);
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

        private static void DisplayImage(ListView ImageList, List<Photo> photos)
        {
            int counter = 0;
            //Delete LargeImageList
            if (ImageList.LargeImageList != null)
            {
                ImageList.LargeImageList.Dispose();
                ImageList.Items.Clear();
            }

            //Create image list
            ImageList.View = View.LargeIcon;
            ImageList.LargeImageList = new ImageList();
            ImageList.LargeImageList.ImageSize = new Size(256, 256);
            ImageList.LargeImageList.ColorDepth = ColorDepth.Depth32Bit;

            foreach (var photo in photos)
            {
                try
                {
                    //Create photo preview
                    var i = new ImagesPreview(photo.Path);
                    ImageList.LargeImageList.Images.Add(i.thumbnail);
                    ImageList.Items.Add(photo.Path, counter);
                    counter++;
                    if (photo.Exist == 1)
                    {
                        UpdateExist(photo, 0);
                    }
                }
                catch (Exception)
                {
                    UpdateExist(photo, 1);
                }
            }
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

        public void ShowPhotoFromAlbum(string year, string albumName, ListView ImageList)
        {
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

        public void MoveToAnotherAlbum(string newAlbumName, string oldAlbumName, string path)
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

        public void CopyToAnotherAlbum(string newAlbumName, string path)
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

        public string normalizeYear(string year)
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
