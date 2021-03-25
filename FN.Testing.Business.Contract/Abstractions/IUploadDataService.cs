using FN.Testing.Business.Contract.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FN.Testing.Business.Contract.Abstractions
{
    public interface IUploadDataService
    {
        Task<UploadedEntity> GetUpload(int id, CancellationToken cancellationToken);
        Task<IEnumerable<UploadedEntity>> GetUploads(CancellationToken cancellationToken);
        Task<UploadedEntity> AddUpload(UploadEntity entity, CancellationToken cancellationToken);
        Task DeleteUpload(int id, CancellationToken cancellationToken);
        Task<byte[]> GetFile(string filePath, CancellationToken cancellationToken);
        string GetContentType(string filePath);
    }
}
