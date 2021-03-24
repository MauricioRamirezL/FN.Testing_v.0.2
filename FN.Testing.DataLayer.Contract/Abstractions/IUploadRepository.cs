using FN.Testing.DataLayer.Contract.Tables;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FN.Testing.DataLayer.Contract.Abstractions
{
    public interface IUploadRepository : IGenericRepository<Upload>
    {
        Task<IList<Upload>> GetUploads();
        Task<Upload> GetUploadById(int id, CancellationToken cancellationToken);
        Task<int> AddUpload(Upload inputEt, CancellationToken cancellationToken);
        Task DeleteUpload(int id, CancellationToken cancellationToken);
    }
}
