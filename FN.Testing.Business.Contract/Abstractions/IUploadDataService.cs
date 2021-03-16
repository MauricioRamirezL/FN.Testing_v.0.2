using FN.Testing.Business.Contract.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace FN.Testing.Business.Contract.Abstractions
{
    public interface IUploadDataService
    {
        Task<UploadEntity> GetUpload(string id, CancellationToken cancellationToken);
        Task<string> AddUpload(UploadEntity entity, CancellationToken cancellationToken);
    }
}
