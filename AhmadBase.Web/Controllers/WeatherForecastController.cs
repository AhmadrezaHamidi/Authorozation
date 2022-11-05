using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AhmadBase.Core.Dtos;
using AhmadBase.Core.interfere.IReposetory;
using AhmadBase.Core.Types;
using AhmadBase.Inferastracter;
using AhmadBase.Inferastracter.Datas.Entities;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using AhmadBase.Core.Dtos;
using AhmadBase.Web.Commands;
using AhmadBase.Web.Dtos;
using AhmadBase.web.Queries;
using Microsoft.IdentityModel.Tokens;

namespace AhmadBase.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : BaseController
    {
        private readonly IMediator _mediator;

        public WeatherForecastController(IMediator mediator, ILogger<WeatherForecastController> logger)
        {
            _mediator = mediator;
        }




        [HttpGet]
        public async Task<ServiceResult<MessageResultDto>> GetMessageById(string id)
        {
            var query = new GetMessageQuery(id);
            var result = await _mediator.Send(query);
            return result;
        }



        //[HttpPost]
        //[Authorize]
        //public async Task<ActionResult> Create([FromBody] CreateDirectMessageDto input)
        //{
        //    //var command = input.Adapt<CreateDirectMessageCommand>();
        //    //command.FirstUserId = "Ahmad";
        //    var result = await _mediator.Send<ServiceResult<CreateDirectsMessageResultDto>>(command);
        //    return await result.AsyncResult();
        //}








        //[HttpPost]
        //public async Task<MessageResultDto> MakeingMessage(string id)
        //{
        //    var query = new GetMessageQuery(id);
        //    var result = await _mediator.Send(query);
        //    return result;
        //}


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetUserIdsFromToken()
        {
            var usrId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "userId");
            return Ok(usrId.ToString());
        }

    
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register([FromBody] RegisterInputDto input)
        {

            var command = input.Adapt<RegisterCommand>();
            var result = await _mediator.Send<ServiceResult<string>>(command);
            return await result.AsyncResult();
        }


        [HttpPost("Lpgin")]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginDto input)
        {


            var command = input.Adapt<LoginCommand>();
            var result = await _mediator.Send<ServiceResult<string>>(command);
            return await result.AsyncResult();




        }

    }
}
