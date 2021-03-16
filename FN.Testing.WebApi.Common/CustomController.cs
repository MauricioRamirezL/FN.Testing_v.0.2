using FN.Testing.Common.Contract;
using FN.Testing.Common.WebCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FN.Testing.WebApi.Common
{
    [ApiController]
    public abstract class CustomController : ControllerBase
    {
        protected async Task<IActionResult> HandleRequestAsync<T>(Func<Task<T>> serviceRequest)
        {
            if (serviceRequest == null) throw new ArgumentNullException(nameof(serviceRequest));
            try
            {
                var response = await serviceRequest();
                if (response == null)
                {
                    var message = $"Service response is null, but it should be returned an object with type {typeof(T)}.";
                    if (HttpContext.Request.Method == HttpMethod.Get.Method)
                        return NotFound(message);
                    else
                        return BadRequest(message);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }
        protected async Task<IActionResult> HandleRequestAsync(Func<Task> serviceRequest)
        {
            if (serviceRequest == null) throw new ArgumentNullException(nameof(serviceRequest));
            try
            {
                await serviceRequest();

                return Ok();
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }
        private Task<IActionResult> HandleExceptionAsync(Exception ex)
        {
            switch (ex)
            {
                case ValidationResultException e:
                    return Task.FromResult<IActionResult>(BadRequest(e.ValidationResult.ToValidationResponseModel()));
                case BusinessValidationException e:
                    {
                        var responseModel = new ValidationResponseModel();
                        responseModel.AddError(e.PropertyName, e.ValidationKey);
                        return Task.FromResult<IActionResult>(BadRequest(responseModel));
                    }
                case ArgumentNullException e:
                    return Task.FromResult<IActionResult>(BadRequest(ex.Message));
                default:
                    throw ex;
            }
        }
    }
}
