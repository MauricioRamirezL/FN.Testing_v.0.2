using FN.Testing.DataLayer.Contract.Abstractions;
using FN.Testing.DataLayer.DataContext;

namespace FN.Testing.DataLayer.Repositories
{
    public class DataLookupRepository<TEntity> : GenericRepository<TEntity>, IDataLookupRepository<TEntity> where TEntity : class
    {
        public DataLookupRepository(ConnectionDataContext context) : base(context)
        {

        } 
    }
}
