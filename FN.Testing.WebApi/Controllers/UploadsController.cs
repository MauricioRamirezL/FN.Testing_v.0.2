using FN.Testing.Application.Contract.Models;
using FN.Testing.Application.Contract.Services;
using FN.Testing.WebApi.Common;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

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
        [ProducesResponseType(typeof(List<UploadModel>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get([FromRoute] string id, CancellationToken cancellationToken)
        {
            return await HandleRequestAsync(() => _uploadService.GetUpload(id, cancellationToken));
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Post([FromBody] UploadModel item, CancellationToken cancellationToken)
        {
            return await HandleRequestAsync(() => _uploadService.PostUpload(item, cancellationToken));
        }
    }
}
