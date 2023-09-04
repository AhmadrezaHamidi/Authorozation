using BackOffice.Application.Contracts;
using BackOffice.Application.Features.Queries;
using BackOffice.Domain.Entities.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BackOffice.Api.Controllers
{
    [ApiController]
    [Route("api/v1/Report")]
  //  [Authorize]
    public class ReportController : BaseController
    {
        private readonly IMediator _mediator;
        
        public ReportController(IMediator mediator)
        {
            _mediator = mediator;
        }



        [HttpGet("[action]")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetFlowHistory(string flowId)
        {
            ///Bearer token
            var userId = UserId;
            var request = new GetFlowHistoryQuery(flowId);
            var result = await _mediator.Send(request);
            return Ok(result);
        }


        [HttpGet("[action]")]
        [Authorize]
        public async Task<IActionResult> GetStatusHistoryFlowActivity(string flowActivityUniqueId)
        {
            var request = new GetStatusHistoryFlowActivityQuery(flowActivityUniqueId);
            var result = await _mediator.Send(request);
            return Ok(result);
        }


        [HttpGet("[action]")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetTop10FlowsWithStatus(int userId)
        {
            var request = new GetTop10FlowsWithStatusQuery(userId);
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
