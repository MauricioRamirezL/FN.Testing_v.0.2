namespace FN.Testing.DataLayer.Contract.Abstractions
{
    public interface IDataLookupRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class 
    {
    }
}
