using Application.CQRS.AuthenticateCommandQuery.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticateController:ControllerBase
    {
        private readonly IMediator mediator;

        public AuthenticateController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Post(LoginCommand loginCommand)
        {
            var result = await mediator.Send(loginCommand);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommand registerCommand)
        {
            var result = await mediator.Send(registerCommand);
            return Ok(result);
        }

          [HttpPost("GenerateToken")]
        public async Task<IActionResult> GenerateNewToken(GenerateNewTokenCommand generateNewTokenCommand)
        {
            var result = await mediator.Send(generateNewTokenCommand);
            return Ok(result);
        }
    }
}
