using FN.Testing.DataLayer.Contract.Abstractions;
using FN.Testing.DataLayer.Contract.Tables;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FN.Testing.DataLayer.DataContext;

namespace FN.Testing.DataLayer.Repositories
{
    public class UploadRepository : GenericRepository<Upload>, IUploadRepository
    {
        private ConnectionDataContext connectionDBContext;

        public UploadRepository(ConnectionDataContext context) : base (context)
        {
            connectionDBContext = context;
        }
        public async Task<IList<Upload>> GetUploads()
        {
            return await GetAllAsync();
        }
        public async Task<Upload> GetUploadById(int id, CancellationToken cancellationToken)
        {
            return await GetByIdAsync(id, cancellationToken);
        }
        public async Task<int> AddUpload(Upload upload, CancellationToken cancellationToken)
        {
            await InsertAsync(upload, cancellationToken, true);
            return upload.Id;
        }
        public async Task DeleteUpload(int id, CancellationToken cancellationToken)
        {
            await DeleteAsync(id, cancellationToken, true);
        }
    }
}
