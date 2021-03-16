using AutoMapper;
using FN.Testing.Business.Contract.Abstractions;
using FN.Testing.Business.Contract.Entities;
using FN.Testing.DataLayer.Contract.Abstractions;
using FN.Testing.DataLayer.Contract.Tables;
using System.Threading;
using System.Threading.Tasks;

namespace FN.Testing.Business.Services
{
    public class UploadDataService : IUploadDataService
    {
        private readonly IRepository<Upload> _repository;
        private readonly IMapper _mapper;
        public int Instance;
        public static int InstanceCount;

        public UploadDataService(
            IRepository<Upload> repository,
            IMapper mapper)
        {
            InstanceCount++;
            Instance = InstanceCount;
            _repository = repository ?? throw new System.ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
        }
        public async Task<UploadEntity> GetUpload(string id, CancellationToken cancellationToken)
        {
            var row = await _repository.Get(id, cancellationToken);
            return _mapper.Map<UploadEntity>(row);
        }
        public async Task<string> AddUpload(UploadEntity entity, CancellationToken cancellationToken)
        {
            return await _repository.Add(_mapper.Map<Upload>(entity), cancellationToken);
        }
    }
}
