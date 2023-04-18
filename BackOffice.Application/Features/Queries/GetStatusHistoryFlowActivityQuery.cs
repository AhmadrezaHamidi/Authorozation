using AutoMapper;
using BackOffice.Application.Contracts;
using BackOffice.Application.Dtos;
using BackOffice.Domain.Entities;
using Houshmand.Framework.Configuration;
using Houshmand.Framework.DataAccess.Dapper;
using Houshmand.Framework.ExceptionHandler;
using Houshmand.Framework.Logging;
using Houshmand.Framework.Logging.Console.Extensions;
using Houshmand.Framework.WorkFlow;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Application.Features.Queries;

public class GetStatusHistoryFlowActivityQuery : IRequest<List<GetStatusHistoryQueryDto>>
{
    public GetStatusHistoryFlowActivityQuery(string flowActivityUniqueId)
    {
        this.flowActivityUniqueId = flowActivityUniqueId;
    }
    public string flowActivityUniqueId { get; set; }
}


public class GetStatusHistoryFlowActivityQueryHandler
    : IRequestHandler<GetStatusHistoryFlowActivityQuery, List<GetStatusHistoryQueryDto>>
{
    private readonly IFlowService _flowService;
    private readonly ILogger<GetStatusHistoryFlowActivityQueryHandler> _logger;
    private readonly IFlowTracking _flowTracking;
    private readonly IConfigReader _config;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetStatusHistoryFlowActivityQueryHandler(IFlowService flowService,
        IFlowTracking flowTracking,
        IConfigReader config,
        IUnitOfWork unitOfWork)
    {
        _logger = new LogBuilder()
            .WriteTo.Console()
            .CreateLogger<GetStatusHistoryFlowActivityQueryHandler>();
        _flowService = flowService;
        _flowTracking = flowTracking;
        _config = config;
        _unitOfWork = unitOfWork;
    }

    async Task<List<GetStatusHistoryQueryDto>> IRequestHandler<GetStatusHistoryFlowActivityQuery,
        List<GetStatusHistoryQueryDto>>.Handle(GetStatusHistoryFlowActivityQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var flowId = _flowTracking.CreateFlow(2, 1, new("0.0.11"));
            var flowStep = _flowTracking.GoToNextStep(flowId, 1);
            var repo = await _flowService.GetStatusHistoryFlowActivity(request.flowActivityUniqueId);

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<StatusHistory, GetStatusHistoryQueryDto>()).CreateMapper();
            var result = mapper.Map<List<GetStatusHistoryQueryDto>>(repo);
            return result;
        }
        catch (HoushmandBaseException ex)
        {
            throw ex;
        }
    }
}
