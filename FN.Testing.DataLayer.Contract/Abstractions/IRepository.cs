using FN.Testing.DataLayer.Contract.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FN.Testing.DataLayer.Contract.Abstractions
{
    public interface IRepository<TTable> where TTable : ITable
    {
        Task<Upload> Get(string id, CancellationToken cancellationToken);
        Task<string> Add(TTable entity, CancellationToken cancellationToken);
    }
}
