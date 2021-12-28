using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;


namespace PhotoManager
{
    class ImagesPreview
    {
        public int height = 0;
        public int width = 256;
        public string path;
        public int orientation;

        private static RotateFlipType OrientationToFlipType(int orientation)
        {
            switch (orientation)
            {
                case 1:
                    return RotateFlipType.RotateNoneFlipNone;
                case 2:
                    return RotateFlipType.RotateNoneFlipX;
                case 3:
                    return RotateFlipType.Rotate180FlipNone;
                case 4:
                    return RotateFlipType.Rotate180FlipX;
                case 5:
                    return RotateFlipType.Rotate90FlipX;
                case 6:
                    return RotateFlipType.Rotate90FlipNone;
                case 7:
                    return RotateFlipType.Rotate270FlipX;
                case 8:
                    return RotateFlipType.Rotate270FlipNone;
                default:
                    return RotateFlipType.RotateNoneFlipNone;
            }
        }

        public ImagesPreview(string path, int orientation)
        {
            this.path = path;
            this.orientation = orientation;
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

        private Bitmap createThumbnail(Image image, int orientation)
        {
            var thumbnail = new Bitmap(256, 256);
            image.RotateFlip(OrientationToFlipType(orientation));
            using (var graphics = Graphics.FromImage(thumbnail))
            {
                graphics.Clear(Color.White);
                graphics.DrawImage(image, (int)((256 - image.Width) / 2), 256 - image.Height);
            }
            return thumbnail;
        }

        public  Bitmap thumbnail()
        {
            Image image = new Bitmap(path);
            height = (int)(image.Height / (image.Width / width));
            if (height > 256)
            {
                height = 256;
                width = (int)(image.Width / (image.Height / height));
            }
            var thumbnail = createThumbnail(ResizeImage(image), orientation);
            image.Dispose();
            return thumbnail;
        }
    }
}
