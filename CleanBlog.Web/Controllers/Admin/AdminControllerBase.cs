using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CleanBlog.Models;
using Microsoft.AspNetCore.Mvc;

namespace CleanBlog.Web.Controllers.Admin
{
    [Area("admin")]
    [Produces("application/json")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public abstract class AdminControllerBase : ControllerBase
    {
        protected virtual ApiResponseModel NotFoundResponse(object objectId, string message)
        {
            var response =  new ApiResponseModel()
            {
                Status = ResponseStatus.Fail,
                Data = new Dictionary<string, object>(),
                Message = message
            };
            var errors = new List<object>();
            errors.Add(new { Id = objectId });
            response.Data.Add("errors", errors);

            return response;
        }

        protected virtual ApiResponseModel OkResponse(object data, string message = null)
        {
            var response = new ApiResponseModel()
            {
                Status = ResponseStatus.Success,
                Data = new Dictionary<string, object>(),
                Message = message
            };
            response.Data.Add(data.GetType().Name, data);
            return response;
        }
    }
}
