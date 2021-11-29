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

        public void ShowPhotoFromAlbum(System.Windows.Forms.ListBox AlbumList, ListView ImageList, int albumId)
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

                    foreach (var photo in photos)
                    {
                        var i = new ImagesPreview(photo.Path);
                        ImageList.LargeImageList.Images.Add(i.thumbnail);
                        ImageList.Items.Add(photo.Path, counter);
                        counter++;
                    }

                    //Get albums
                    var albums = db.Albums
                        .ToList();
                    counter = 0;

                    foreach (var album in albums)
                    {
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

    }
}
