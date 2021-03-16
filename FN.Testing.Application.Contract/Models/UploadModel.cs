using System;

namespace FN.Testing.Application.Contract.Models
{
    public class UploadModel
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        //public byte[] File { get; set; }
        public string File { get; set; }
        public DateTimeOffset UploadDate { get; set; }
    }
}
