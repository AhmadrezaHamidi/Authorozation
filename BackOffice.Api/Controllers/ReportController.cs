using AutoMapper;
using BackOffice.Application.Features.Queries;
using Houshmand.Framework.Configuration;
using Houshmand.Framework.ExceptionHandler;
using Houshmand.Framework.WorkFlow.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackOffice.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportController(IMediator mediator)
        //,IMapper mapper, IConfigWriter configWriter) 
        {
            _mediator = mediator;
            //_mapper = mapper;
            //_configWriter = configWriter;
        }



        [HttpGet("{flowId}")]
        [Authorize]
        public async Task<IActionResult> GetFlowHistory(string flowId)
        {
            var request = new GetFlowHistoryQuery(flowId);
            var result = await _mediator.Send(request);
            return Ok(result);
        }


        [HttpGet("{flowActivityUniqueId}")]
        [Authorize]
        public async Task<IActionResult> GetStatusHistoryFlowActivity(string flowActivityUniqueId)
        {
            var request = new GetStatusHistoryFlowActivityQuery(flowActivityUniqueId);
            var result = await _mediator.Send(request);
            return Ok(result);
        }


        [HttpGet("{userId}")]
        [Authorize]
        public async Task<IActionResult> GetTop10FlowsWithStatus(int userId)
        {
            var request = new GetTop10FlowsWithStatusQuery(userId);
            var result = await _mediator.Send(request);
            return Ok(result);
        }



    }
}
