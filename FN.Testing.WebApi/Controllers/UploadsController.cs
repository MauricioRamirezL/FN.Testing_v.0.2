using FN.Testing.Application.Contract.Models;
using FN.Testing.Application.Contract.Services;
using FN.Testing.WebApi.Common;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.StaticFiles;
using System.IO;

namespace FN.Testing.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadsController : CustomController
    {
        private readonly IUploadService _uploadService;
        public UploadsController(IUploadService uploadService)
        {
            _uploadService = uploadService ?? throw new System.ArgumentNullException(nameof(uploadService));
        }
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(UploadedModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetUpload([FromRoute] int id, CancellationToken cancellationToken)
        {
            return await HandleRequestAsync(() => _uploadService.GetUpload(id, cancellationToken));
        }
        [HttpGet]
        [Route("{id:int}/{fileName}")]
        [ProducesResponseType(typeof(UploadedModel), 200)]
        [ProducesResponseType(400)]
        public async Task<UploadedModel> ViewForm(int id, string fileName, string fileExtension, string uploadDate)
        {
            await Task.Delay(1000);
            return new UploadedModel {
                Id = id, FileName = fileName, Extension = fileExtension, UploadDate = DateTime.Parse(uploadDate)
            };
        }
        [HttpGet("download/{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DownloadFile(int id, CancellationToken cancellationToken)
        {
            var uploadData = await _uploadService.GetUpload(id, cancellationToken);
            if (uploadData != null)
            {
                string contentType = _uploadService.GetContentType(string.Concat(uploadData.FileName, uploadData.Extension));
                var bytes = await _uploadService.GetFile(
                    string.Concat(uploadData.FileName, uploadData.Extension), cancellationToken);
                return File(bytes, contentType, string.Concat(uploadData.FileName, uploadData.Extension));
            }
            else return NotFound();
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<UploadedModel>> PostUpload([FromForm] UploadModel item, CancellationToken cancellationToken)
        {
            var result = await HandleRequestAsync(() => _uploadService.PostUpload(item, cancellationToken));
            var okObjectResult = result as OkObjectResult;
            var submissionResult = (UploadedModel)okObjectResult.Value;
            return CreatedAtAction(nameof(ViewForm), new { 
                id = submissionResult.Id, 
                fileName = submissionResult.FileName,
                fileExtension = submissionResult.Extension,
                uploadDate = submissionResult.UploadDate.ToString()
            }, result);;
        }
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteUpload([FromRoute] int id, CancellationToken cancellationToken)
        {
            return await HandleRequestAsync(() => _uploadService.DeleteUpload(id, cancellationToken));
        }
    }
}
