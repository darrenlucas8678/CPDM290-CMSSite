using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Drawing;
using System.Web.Util;

namespace CPDM.LucasD.Midterm
{
    /// <summary>
    /// Summary description for Utility
    /// </summary>
    public abstract class ImageProcessor
    {
        protected abstract int imageMaxHeight { get; }
        protected abstract int imageMaxWidth { get; }
        protected abstract Bitmap ResizeImage(Stream imageFileStream);
        public string ProcessImage(string filename, Stream imageFileStream)
        {
            if (IsImage(filename))
            {
                filename = RenameFile(filename);
                Bitmap bitmap = ResizeImage(imageFileStream);
                bitmap.Save(HttpContext.Current.Server.MapPath("~/upload/" + filename));
                return "~/upload/" + filename;
            }
            else
            {
                return null;
            }
        }
        private string RenameFile(string filename)
        {
            return Guid.NewGuid().ToString() + Path.GetExtension(filename).ToLower();
        }
        private bool IsImage(string filename)
        {

            switch (Path.GetExtension(filename.ToLower()))
            {
                case ".jpg":
                    return true;
                case ".jpeg":
                    return true;
                case ".png":
                    return true;
                case ".gif":
                    return true;
                default:
                    return false;
            }
        }
    }
    public class ThumbnailImageProcessor : ImageProcessor
    {
        protected override int imageMaxHeight { get { return 200; } }
        protected override int imageMaxWidth { get { return 200; } }
        protected override Bitmap ResizeImage(Stream imageFileStream)
        {
            Image originalImage = Image.FromStream(imageFileStream);

            int imageHeight = originalImage.Height;
            int imageWidth = originalImage.Width;

            double scaleWidth = (double)imageMaxWidth / (double)imageWidth;
            double scaleHeight = (double)imageMaxHeight / (double)imageHeight;

            if (scaleHeight >= scaleWidth)
            {
                imageHeight = Convert.ToInt32(imageHeight * scaleHeight);
                imageWidth = Convert.ToInt32(imageWidth * scaleHeight);
            }
            else
            {
                imageHeight = Convert.ToInt32(imageHeight * scaleWidth);
                imageWidth = Convert.ToInt32(imageWidth * scaleWidth);
            }

            Bitmap resizedBitmap = new Bitmap(originalImage, imageWidth, imageHeight);


            int centerPointOffsetX = imageWidth / 2 - imageMaxWidth / 2;
            int centerPointOffsetY = imageHeight / 2 - imageMaxHeight / 2;

            Rectangle sourceRectangle = new Rectangle(centerPointOffsetX, centerPointOffsetY, imageMaxWidth, imageMaxHeight);
            Rectangle destinationRectangle = new Rectangle(0, 0, imageMaxWidth, imageMaxHeight);

            Bitmap croppedImage = new Bitmap(imageMaxWidth, imageMaxHeight);

            using (Graphics graphic = Graphics.FromImage(croppedImage))
            {
                graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphic.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                graphic.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphic.DrawImage(resizedBitmap, destinationRectangle, sourceRectangle, GraphicsUnit.Pixel);
            }
            return croppedImage;
        }
    }
    public class ArticleImageProcessor : ImageProcessor
    {
        protected override int imageMaxHeight { get { return 400; } }
        protected override int imageMaxWidth { get { return 400; } }
        protected override Bitmap ResizeImage(Stream imageFileStream)
        {
            Image originalImage = Image.FromStream(imageFileStream);

            int imageHeight = originalImage.Height;
            int imageWidth = originalImage.Width;

            double scaleWidth = (double)imageMaxWidth / (double)imageWidth;
            double scaleHeight = (double)imageMaxHeight / (double)imageHeight;

            if (scaleHeight <= scaleWidth)
            {
                imageHeight = Convert.ToInt32(imageHeight * scaleHeight);
                imageWidth = Convert.ToInt32(imageWidth * scaleHeight);
            }
            else
            {
                imageHeight = Convert.ToInt32(imageHeight * scaleWidth);
                imageWidth = Convert.ToInt32(imageWidth * scaleWidth);
            }
            Bitmap resizedImage = new Bitmap(originalImage, imageWidth, imageHeight);
            return resizedImage;
        }
    }
}
