using System;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace FN.Test.Functions
{
    public class ImageResizer
    {
        private int allowedFileSizeInByte;
        private string sourcePath;
        private string destinationPath;

        public ImageResizer(int allowedSize, string sourcePath, string destinationPath)
        {
            allowedFileSizeInByte = allowedSize;
            this.sourcePath = sourcePath;
            this.destinationPath = destinationPath;
        }
        public ImageResizer()
        {
        }
        public void ScaleImage()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (FileStream fs = new FileStream(sourcePath, FileMode.Open))
                {
                    Bitmap bmp = (Bitmap)Image.FromStream(fs);
                    SaveTemporary(bmp, ms, 100);
                    while (ms.Length < 0.9 * allowedFileSizeInByte || ms.Length > allowedFileSizeInByte)
                    {
                        double scale = Math.Sqrt((double)allowedFileSizeInByte / (double)ms.Length);
                        ms.SetLength(0);
                        bmp = ScaleImage(bmp, scale);
                        SaveTemporary(bmp, ms, 100);
                    }

                    if (bmp != null)
                        bmp.Dispose();
                    SaveImageToFile(ms);
                }
            }
        }
        private void SaveImageToFile(MemoryStream ms)
        {
            byte[] data = ms.ToArray();

            using (FileStream fs = new FileStream(destinationPath, FileMode.Create))
            {
                fs.Write(data, 0, data.Length);
            }
        }
        private void SaveTemporary(Bitmap bmp, MemoryStream ms, int quality)
        {
            EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            var codec = string.IsNullOrEmpty(sourcePath) ? GetImageCodecInfo(bmp) : GetImageCodecInfo();
            var encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            if (codec != null)
                bmp.Save(ms, codec, encoderParams);
            else
                bmp.Save(ms, GetImageFormat());
        }
        public Bitmap ScaleImage(Bitmap image, double scale)
        {
            int newWidth = (int)(image.Width * scale);
            int newHeight = (int)(image.Height * scale);

            Bitmap result = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb);
            result.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (Graphics g = Graphics.FromImage(result))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                g.DrawImage(image, 0, 0, result.Width, result.Height);
            }
            return result;
        }

        private ImageCodecInfo GetImageCodecInfo()
        {
            FileInfo fi = new FileInfo(sourcePath);

            switch (fi.Extension)
            {
                case ".bmp": return ImageCodecInfo.GetImageEncoders()[0];
                case ".jpg":
                case ".jpeg": return ImageCodecInfo.GetImageEncoders()[1];
                case ".gif": return ImageCodecInfo.GetImageEncoders()[2];
                case ".tiff": return ImageCodecInfo.GetImageEncoders()[3];
                case ".png": return ImageCodecInfo.GetImageEncoders()[4];
                default: return null;
            }
        }
        private ImageCodecInfo GetImageCodecInfo(Image image)
        {
            if (image.RawFormat.Equals(ImageFormat.Bmp)) return ImageCodecInfo.GetImageEncoders()[0];
            if (image.RawFormat.Equals(ImageFormat.Jpeg)) return ImageCodecInfo.GetImageEncoders()[1];
            if (image.RawFormat.Equals(ImageFormat.Gif)) return ImageCodecInfo.GetImageEncoders()[2];
            if (image.RawFormat.Equals(ImageFormat.Tiff)) return ImageCodecInfo.GetImageEncoders()[3];
            if (image.RawFormat.Equals(ImageFormat.Png)) return ImageCodecInfo.GetImageEncoders()[4];
            return null;
        }

        private ImageFormat GetImageFormat()
        {
            FileInfo fi = new FileInfo(sourcePath);

            switch (fi.Extension)
            {
                case ".jpg": return ImageFormat.Jpeg;
                case ".bmp": return ImageFormat.Bmp;
                case ".gif": return ImageFormat.Gif;
                case ".png": return ImageFormat.Png;
                case ".tiff": return ImageFormat.Tiff;
                default: return ImageFormat.Png;
            }
        }
        public Image ResizeImage(Image srcImage, int newWidth, int newHeight)
        {
            using (Bitmap imagenBitmap =
               new Bitmap(newWidth, newHeight, PixelFormat.Format32bppRgb))
            {
                imagenBitmap.SetResolution(
                   Convert.ToInt32(srcImage.HorizontalResolution),
                   Convert.ToInt32(srcImage.HorizontalResolution));

                using (Graphics imagenGraphics =
                        Graphics.FromImage(imagenBitmap))
                {
                    imagenGraphics.SmoothingMode =
                       SmoothingMode.AntiAlias;
                    imagenGraphics.InterpolationMode =
                       InterpolationMode.HighQualityBicubic;
                    imagenGraphics.PixelOffsetMode =
                       PixelOffsetMode.HighQuality;
                    imagenGraphics.DrawImage(srcImage,
                       new Rectangle(0, 0, newWidth, newHeight),
                       new Rectangle(0, 0, srcImage.Width, srcImage.Height),
                       GraphicsUnit.Pixel);
                    MemoryStream imagenMemoryStream = new MemoryStream();
                    imagenBitmap.Save(imagenMemoryStream, ImageFormat.Jpeg);
                    srcImage = Image.FromStream(imagenMemoryStream);
                }
            }
            return srcImage;
        }

        public string ScaleImage(string sourcePath, int allowedSize, bool deleteSrcFile = false)
        {
            string destPath = new Generic().GetNewFilePath(Path.GetDirectoryName(sourcePath));
            ImageResizer resizer = new ImageResizer(allowedSize, sourcePath, destPath);
            resizer.ScaleImage();
            if (deleteSrcFile) File.Delete(sourcePath);
            return destPath;
        }
    }
}
