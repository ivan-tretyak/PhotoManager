using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;


namespace PhotoManager
{
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
