using AutoMapper;
using FluentValidation;
using FN.Testing.Application.Contract.Models;
using FN.Testing.Application.Contract.Services;
using FN.Testing.Business.Contract.Abstractions;
using FN.Testing.Business.Contract.Entities;
using FN.Testing.Common.Core;
using FN.Testing.Entities;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;

namespace FN.Testing.Application.Services
{
    public class UploadService : IUploadService
    {
        private readonly IUploadDataService _uploadDataService;
        private readonly IMapper _mapper;
        private readonly IValidator<UploadModel> _validator;
        private readonly IOptions<CustomConfig> _config;
        public int Instance;
        public static int InstanceCount;
        public UploadService(
            IUploadDataService uploadDataService,
            IMapper mapper,
            IValidator<UploadModel> validator, IOptions<CustomConfig> config)
        {
            InstanceCount++;
            _uploadDataService = uploadDataService ?? throw new System.ArgumentNullException(nameof(uploadDataService));
            _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
            _validator = validator;
            Instance = InstanceCount;
            _config = config;
        }
        public async Task<UploadModel> GetUpload(string id, CancellationToken cancellationToken)
        {
            var entity = await _uploadDataService.GetUpload(id, cancellationToken);
            var upload = _mapper.Map<UploadModel>(entity);

            return upload;
        }
        public async Task<string> PostUpload(UploadModel uploadModel, CancellationToken cancellationToken)
        {
            _validator.ValidateCustom(uploadModel);

            var entity = _mapper.Map<UploadEntity>(uploadModel);            
            entity.AllowedSize = _config.Value.AllowedSize;
            entity.HeightPercent = _config.Value.HeightPercent;
            entity.WidthPercent = _config.Value.WidthPercent;
            entity.UploadPath = _config.Value.UploadPath;
            entity.UploadUri = _config.Value.UploadUri;
            return await _uploadDataService.AddUpload(entity, cancellationToken);
        }
    }
}
