using AutoMapper;
using FN.Testing.Business.Contract.Abstractions;
using FN.Testing.Business.Contract.Entities;
using FN.Testing.DataLayer.Contract.Abstractions;
using FN.Testing.DataLayer.Contract.Tables;
using FN.Testing.Functions;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FN.Testing.Business.Services
{
    public class UploadDataService : IUploadDataService
    {
        private readonly IUploadRepository _repository;
        private readonly IMapper _mapper;
        public int Instance;
        public static int InstanceCount;

        public UploadDataService(
            IUploadRepository repository,
            IMapper mapper)
        {
            InstanceCount++;
            Instance = InstanceCount;
            _repository = repository ?? throw new System.ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
        }
        public async Task<UploadedEntity> GetUpload(int id, CancellationToken cancellationToken)
        {
            var row = await _repository.GetUploadById(id, cancellationToken);
            return _mapper.Map<UploadedEntity>(row);
        }
        public async Task DeleteUpload(int id, CancellationToken cancellationToken)
        {
            await _repository.DeleteUpload(id, cancellationToken);
        }
        public async Task<UploadedEntity> AddUpload(UploadEntity entity, CancellationToken cancellationToken)
        {
            var uploadPath = new Uploader().UploadFile(entity.File);
            var inputEt = _mapper.Map<Upload>(entity);
            inputEt.UploadDate = DateTime.Now;
            inputEt.Id = 0;
            inputEt.FileName = Path.GetFileNameWithoutExtension(uploadPath);
            inputEt.Extension = Path.GetExtension(uploadPath);
            return new UploadedEntity
            {
                Id = await _repository.AddUpload(inputEt, cancellationToken),
                Extension = inputEt.Extension, FileName = inputEt.FileName, UploadDate = inputEt.UploadDate
            };
        }
        public async Task<byte[]> GetFile(string filePath, CancellationToken cancellationToken)
        {
            return await new Uploader().DownloadFile(filePath);           
        }
        public string GetContentType(string filePath)
        {
            return new Uploader().GetContentType(filePath);
        }
    }
}
