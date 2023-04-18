using BackOffice.Application.Dtos.Enumes;
using BackOffice.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace BackOffice.Api.Filters
{
    public class ServerErrorResult : IActionResult
    {
        public string Message { get; }

        public ServerErrorResult(string message)
        {
            Message = message;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var response = new ApiResult(false, ApiResultStatusCode.ServerError, Message);

            await context.HttpContext.Response.WriteAsJsonAsync(response);
        }
    }


 
}
