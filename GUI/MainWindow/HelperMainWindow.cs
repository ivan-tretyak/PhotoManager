using System;
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

        public void showData(ListBox AlbumList, ComboBox comboBox)
        {
            try
            {
                using (var db = new DatabaseContext())
                {
                    //Selected all albums
                    var albums = db.Albums.ToList();

                    //Show albums
                    foreach(var album in albums)
                    {
                        AlbumList.Items.Add(album.Name);
                    }

                    //Select metadata
                    var metadatas = db.MetaDatas.ToList();

                    //Get years
                    List<string> years = new();
                    foreach (var metadata in metadatas)
                    {
                        var date = DateTime.Parse(metadata.DateCreation);
                        years.Add(date.Year.ToString());
                    }

                    var yearsUnique = years.ToHashSet();
                    //Add unique value
                    foreach (var year in yearsUnique)
                    {
                        comboBox.Items.Add(year);
                    }
                }
            }
            catch(Exception)
            {
                var s = new SelectPathWindows();
                s.ShowDialog();
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
                int counter = 0;

                List<Photo> photos = new();

                foreach (var albumContext in albumsContexts)
                {
                    photos.Add(db.Photos.Where(photo => photo.PhotoId == albumContext.PhotoId).First());
                }

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
                    //Create photo preview
                    var i = new ImagesPreview(photo.Path);
                    ImageList.LargeImageList.Images.Add(i.thumbnail);
                    ImageList.Items.Add(photo.Path, counter);
                    counter++;
                }
            }

        }

        public void showPhotoFromChooseYearCurrentAlbum(string year, string albumName, ListView ImageList)
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
                int counter = 0;

                foreach (var photo in photos)
                {
                    //Create photo preview
                    var i = new ImagesPreview(photo.Path);
                    ImageList.LargeImageList.Images.Add(i.thumbnail);
                    ImageList.Items.Add(photo.Path, counter);
                    counter++;
                }

            }
        }
    }
}
