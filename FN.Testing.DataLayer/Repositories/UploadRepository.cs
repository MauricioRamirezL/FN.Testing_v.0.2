using FN.Testing.DataLayer.Contract.Abstractions;
using FN.Testing.DataLayer.Contract.Tables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using FN.Test.Functions;

namespace FN.Testing.DataLayer.Repositories
{
    public class UploadRepository : IRepository<Upload>
    {
        private List<Upload> _store;

        public UploadRepository()
        {
            _store = new List<Upload>
            {
                new Upload
                {
                    Id = "1",
                    FileName = "First",
                    UploadDate = new DateTimeOffset(new DateTime(2020, 1, 1)),
                },
                new Upload
                {
                    Id = "2",
                    FileName = "Second",
                    UploadDate = new DateTimeOffset(new DateTime(2020, 1, 2)),
                },
                new Upload
                {
                    Id = "3",
                    FileName = "Third",
                    UploadDate = new DateTimeOffset(new DateTime(2020, 1, 3)),
                },
            };
        }

        public Task<string> Add(Upload upload, CancellationToken cancellationToken)
        {
            //if (_store.SingleOrDefault(x => x.Id == upload.Id) != null)
            //    throw new InvalidOperationException($"The upload with id: '{upload.Id}' could not be added because it already exists.");
            Image image;
            string compressedImagePath = string.Empty ;
            double widthPercent = Convert.ToDouble(upload.WidthPercent) / 100;
            double heightPercent = Convert.ToDouble(upload.HeightPercent) / 100;
            byte[] bytes = Convert.FromBase64String(upload.File);
            using (var ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
                image = new ImageResizer().ResizeImage(image,
                    (int)(image.Width * widthPercent),
                    (int)(image.Height * heightPercent));
                compressedImagePath = new Generic().SaveImageToFile(image, upload.UploadPath);
                compressedImagePath = new ImageResizer().ScaleImage(compressedImagePath, Convert.ToInt32(upload.AllowedSize), true);
                compressedImagePath = string.Concat(upload.UploadUri, Path.GetFileName(compressedImagePath));
            }
            _store.Add(upload);
            return Task.FromResult(compressedImagePath);
        }

        public Task<Upload> Get(string id, CancellationToken cancellationToken)
            => Task.FromResult(_store.SingleOrDefault(x => x.Id == id));
    }
}
