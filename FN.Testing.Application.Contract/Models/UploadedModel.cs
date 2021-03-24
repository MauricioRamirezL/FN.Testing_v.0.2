using System;

namespace FN.Testing.Application.Contract.Models
{
    public class UploadedModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public DateTimeOffset UploadDate { get; set; }
        public string Extension { get; set; }
    }
}
