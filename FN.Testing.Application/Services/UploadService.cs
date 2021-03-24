using AutoMapper;
using FluentValidation;
using FN.Testing.Application.Contract.Models;
using FN.Testing.Application.Contract.Services;
using FN.Testing.Business.Contract.Abstractions;
using FN.Testing.Business.Contract.Entities;
using FN.Testing.Common.Core;
using Microsoft.AspNetCore.StaticFiles;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FN.Testing.Application.Services
{
    public class UploadService : IUploadService
    {
        private readonly IUploadDataService _uploadDataService;
        private readonly IMapper _mapper;
        private readonly IValidator<UploadModel> _validator;
        public int Instance;
        public static int InstanceCount;
        public UploadService(
            IUploadDataService uploadDataService,
            IMapper mapper,
            IValidator<UploadModel> validator
            )
        {
            InstanceCount++;
            _uploadDataService = uploadDataService ?? throw new System.ArgumentNullException(nameof(uploadDataService));
            _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
            _validator = validator;
            Instance = InstanceCount;
        }
        public async Task<UploadedModel> GetUpload(int id, CancellationToken cancellationToken)
        {
            return _mapper.Map<UploadedModel>(
                await _uploadDataService.GetUpload(id, cancellationToken)
            );
        }
        public async Task<UploadedModel> PostUpload(UploadModel uploadModel, CancellationToken cancellationToken)
        {
            _validator.ValidateCustom(uploadModel);
            return _mapper.Map<UploadedModel>(
                await _uploadDataService.AddUpload(_mapper.Map<UploadEntity>(uploadModel), cancellationToken)
            );
        }
        public async Task DeleteUpload(int id, CancellationToken cancellationToken)
        {
            await _uploadDataService.DeleteUpload(id, cancellationToken);
        }
        public async Task<byte[]> GetFile(string filePath, CancellationToken cancellationToken)
        {
            return await _uploadDataService.GetFile(filePath, cancellationToken);
        }
        public string GetContentType(string filePath)
        {
            return _uploadDataService.GetContentType(filePath);
        }
    }
}
