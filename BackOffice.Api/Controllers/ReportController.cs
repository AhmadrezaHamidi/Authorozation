using AutoMapper;
using Houshmand.Framework.Configuration;
using Houshmand.Framework.ExceptionHandler;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackOffice.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IConfigWriter _configWriter;

        public ReportController(IMediator mediator, IMapper mapper, IConfigWriter configWriter)
        {
            _mediator = mediator;
            _mapper = mapper;
            _configWriter = configWriter;
        }



        [HttpPost("{flowId}")]
        public async Task<IActionResult> GetFlowHistory([FromQuery] string flowId)
        {
            
            var request = _mapper.Map<GetCardBalanceQuery>(model);
            if (!request.Validate())
                throw new HoushmandBaseException(ExceptionCriteria.Api, ExceptionType.ValidationFailed, ExceptionLevel.Error, "خطا داده ورودی", request.Validations);
            var result = await _mediator.Send(request);

            return Ok(result);
        }
    }
}
