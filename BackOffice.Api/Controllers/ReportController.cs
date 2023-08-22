using BackOffice.Application.Contracts;
using BackOffice.Application.Features.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetFlowHistory(string flowId)
        {
            var request = new GetFlowHistoryQuery(flowId);
            var result = await _mediator.Send(request);
            return Ok(result);
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetStatusHistoryFlowActivity(string flowActivityUniqueId)
        {
            var request = new GetStatusHistoryFlowActivityQuery(flowActivityUniqueId);
            var result = await _mediator.Send(request);
            return Ok(result);
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetTop10FlowsWithStatus(int userId)
        {
            var request = new GetTop10FlowsWithStatusQuery(userId);
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
