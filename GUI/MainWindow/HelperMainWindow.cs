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

        public void ShowPhotoFromAlbum(System.Windows.Forms.ListBox AlbumList, ListView ImageList, int albumId, System.Windows.Forms.ComboBox comboBox)
        {
            var s = new SelectPathWindows();
            try
            {
                using (var db = new DatabaseContext())
                {
                    //Get album context
                    var albumsContexts = db.AlbumContexts
                       .Where(b => b.AlbumId == albumId)
                       .ToList();
                    int counter = 0;

                    List<Photo> photos = new();

                    foreach (var albumContext in albumsContexts)
                    {
                        photos.Add(db.Photos.Find(albumContext.PhotoId));
                    }

                    //Delete LargeImageList
                    if (ImageList.LargeImageList != null)
                    {
                        ImageList.LargeImageList.Dispose();
                    }

                    //Create image list
                    ImageList.View = View.LargeIcon;
                    ImageList.LargeImageList = new ImageList();
                    ImageList.LargeImageList.ImageSize = new Size(256, 256);
                    ImageList.LargeImageList.ColorDepth = ColorDepth.Depth32Bit;
                    List<string> years = new();

                    foreach (var photo in photos)
                    {
                        //Create photo preview
                        var i = new ImagesPreview(photo.Path);
                        ImageList.LargeImageList.Images.Add(i.thumbnail);
                        ImageList.Items.Add(photo.Path, counter);
                        counter++;

                        //Add a year for sorting
                        //Get metadata
                        var metadata = db.MetaDatas
                            .Where(b => b.MetadataId == photo.MetaDataId)
                            .First();

                        //Parse date from DB
                        var date = DateTime.Parse(metadata.DateCreation);

                        years.Add(date.Year.ToString());
                    }

                    var yearsUnique = years.ToHashSet();
                    //Add unique value
                    foreach (var year in yearsUnique)
                    {
                        comboBox.Items.Add(year);
                    }

                    //Get albums
                    var albums = db.Albums
                        .ToList();
                    counter = 0;

                    foreach (var album in albums)
                    {
                        //Generate album list
                        AlbumList.Items.Add(album.Name);
                        counter++;
                    }
                }
            }
            catch (Exception)
            {
                s.ShowDialog();
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
