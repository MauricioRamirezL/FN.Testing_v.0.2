using System;

namespace FN.Testing.Business.Contract.Entities
{
    public class UploadEntity : IEntity
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public string File { get; set; }
        public DateTimeOffset UploadDate { get; set; }
        public string AllowedSize { get; set; }
        public string UploadPath { get; set; }
        public string WidthPercent { get; set; }
        public string HeightPercent { get; set; }
        public string UploadUri { get; set; }
    }
}
