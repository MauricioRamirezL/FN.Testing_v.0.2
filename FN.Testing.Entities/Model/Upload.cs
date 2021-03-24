using System;

namespace FN.Testing.Entities.Model
{
    public class Upload
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public string File { get; set; }
        public DateTimeOffset UploadDate { get; set; }
    }
}
