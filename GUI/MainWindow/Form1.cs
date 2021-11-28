using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ORMDatabaseModule;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;


namespace PhotoManager
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            var s = new SelectPathWindows();
            try
            {
                using (var db = new DatabaseContext())
                {
                    var photos = db.Photos
                       .ToList();
                    int counter = 0;

                    this.ImageListForAlbum.View = View.LargeIcon;
                    this.ImageListForAlbum.LargeImageList = new ImageList();
                    this.ImageListForAlbum.LargeImageList.ImageSize = new Size(256, 256);
                    this.ImageListForAlbum.LargeImageList.ColorDepth = ColorDepth.Depth32Bit;

                    foreach (var photo in photos)
                    {
                        var i = new ImagesPreview(photo.Path);
                        this.ImageListForAlbum.LargeImageList.Images.Add(i.thumbnail);
                        this.ImageListForAlbum.Items.Add(photo.Path, counter);
                        counter++;
                    }
                }
            } catch (Exception) {
                s.ShowDialog();
            }
            
        }

        private void AlbumList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

    class ImagesPreview
    {
        public int height = 0;
        public int width = 256;
        public Image thumbnail;
        public ImagesPreview(string path)
        {
            try
            {
                Image image = new Bitmap(path);
                height = (int)(image.Height / (image.Width / width));
                if (height > 256)
                {
                    height = 256;
                    width = (int)(image.Width / (image.Height / height));
                }
                thumbnail = createThumbnail(ResizeImage(image));
                image.Dispose();
            }
            catch
            {
                thumbnail = new Bitmap(256, 256);
                using (var g = Graphics.FromImage(thumbnail))
                {
                    g.Clear(Color.Black);
                }
            }

        }

        private Bitmap ResizeImage(Image image)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            //destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.Clamp);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
                graphics.Dispose();
            }
            return destImage;
        }

        private Bitmap createThumbnail(Image image)
        {
            var thumbnail = new Bitmap(256, 256);

            using (var graphics = Graphics.FromImage(thumbnail))
            {
                graphics.Clear(Color.White);
                graphics.DrawImage(image, (int)((256 - image.Width) / 2), 256 - image.Height);
            }
            return thumbnail;
        }
    }
}
