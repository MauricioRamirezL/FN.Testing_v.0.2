using Microsoft.AspNetCore.Http;

namespace FN.Testing.Application.Contract.Models
{
    public class UploadModel
    {
        public IFormFile File { get; set; }
    }
}
