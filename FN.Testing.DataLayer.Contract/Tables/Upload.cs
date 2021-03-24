using System;

namespace FN.Testing.DataLayer.Contract.Tables
{
    public class Upload //: ITable
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public DateTimeOffset UploadDate { get; set; }
        public string Extension { get; set; }
    }
}
