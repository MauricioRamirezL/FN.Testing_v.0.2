using FN.Test.Functions;
using FN.Testing.Common.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FN.Testing.Functions
{
    public class Uploader
    {
        public async Task<byte[]> DownloadFile(string fileName)
        {
            return await File.ReadAllBytesAsync(GetFullUploadFile(fileName));
        }
        private string GetFullUploadFile(string fileName)
        {
            return Path.Combine(StaticConfigs.GetConfig("UploadPath"), fileName);
        }
        public string GetContentType(string filePath)
        {
            filePath = GetFullUploadFile(filePath);
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
        public string UploadFile(IFormFile file)
        {
            double widthPercent = Convert.ToDouble(StaticConfigs.GetConfig("WidthPercent")) / 100;
            double heightPercent = Convert.ToDouble(StaticConfigs.GetConfig("HeightPercent")) / 100;
            int allowedSize = Convert.ToInt32(StaticConfigs.GetConfig("AllowedSize"));
            string fileExtension = Path.GetExtension(file.FileName).ToLower();
            string compressedImagePath = Path.Combine(
                StaticConfigs.GetConfig("UploadPath"),
                string.Concat(Guid.NewGuid().ToString(), fileExtension)
                );
            new FileInfo(compressedImagePath).Directory?.Create();
            using (var stream = new FileStream(compressedImagePath, FileMode.Create))
            {
                stream.Position = 0;
                file.CopyTo(stream);
                stream.Flush();                
            }
            switch (fileExtension)
            {
                case ".jpg":
                    Image imagePrev = Image.FromFile(compressedImagePath);
                    Image image = new ImageResizer().ResizeImage(imagePrev,
                        (int)(imagePrev.Width * widthPercent),
                        (int)(imagePrev.Height * heightPercent));
                    imagePrev.Dispose();
                    File.Delete(compressedImagePath);
                    compressedImagePath = new Generic().SaveImageToFile(image, Path.GetDirectoryName(compressedImagePath));
                    compressedImagePath = new ImageResizer().ScaleImage(compressedImagePath, allowedSize, true);
                    break;
            }
            return Path.GetFileName(compressedImagePath);
        }
    }
}
