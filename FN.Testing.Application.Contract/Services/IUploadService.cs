using FN.Testing.Application.Contract.Models;
using System.Threading;
using System.Threading.Tasks;

namespace FN.Testing.Application.Contract.Services
{
    public interface IUploadService
    {
        Task<UploadModel> GetUpload(string id, CancellationToken cancellationToken);
        Task<string> PostUpload(UploadModel uploadModel, CancellationToken cancellationToken);
    }
}
