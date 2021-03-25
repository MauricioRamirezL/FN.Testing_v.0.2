using FN.Testing.Application.Contract.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FN.Testing.Application.Contract.Services
{
    public interface IUploadService
    {
        Task<UploadedModel> GetUpload(int id, CancellationToken cancellationToken);
        Task<IEnumerable<UploadedModel>> GetUploads(CancellationToken cancellationToken);
        Task<UploadedModel> PostUpload(UploadModel uploadModel, CancellationToken cancellationToken);
        Task DeleteUpload(int id, CancellationToken cancellationToken);
        Task<byte[]> GetFile(string filePath, CancellationToken cancellationToken);
        string GetContentType(string filePath);
    }
}
