using AutoMapper;
using BackOffice.Application.Contracts;
using BackOffice.Application.Dtos;
using BackOffice.Domain.Entities;
using Houshmand.Framework.Configuration;
using Houshmand.Framework.Core.Common.Models;
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

namespace BackOffice.Application.Features.Queries
{
    public class GetFlowHistoryQuery : IRequest<List<GetFlowHistoryQueryDto>>
    {
        public GetFlowHistoryQuery(string flowId)
        {
            FlowId = flowId;
        }

        public string FlowId { get; set; }
    }
    public class GetFlowHistoryQueryHandler
        : IRequestHandler<GetFlowHistoryQuery, List<GetFlowHistoryQueryDto>>
    {
        private readonly IFlowService _flowService;
        private readonly ILogger<GetFlowHistoryQueryHandler> _logger;
        private readonly IFlowTracking _flowTracking;
        private readonly IConfigReader _config;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetFlowHistoryQueryHandler(IFlowService flowService,
            IFlowTracking flowTracking,
            IConfigReader config,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _logger = new LogBuilder()
                .WriteTo.Console()
                .CreateLogger<GetFlowHistoryQueryHandler>();
            _flowService = flowService;
            _flowTracking = flowTracking;
            _config = config;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetFlowHistoryQueryDto>> Handle(GetFlowHistoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var flowId = _flowTracking.CreateFlow(2, 1, new("0.0.11"));
                var flowStep = _flowTracking.GoToNextStep(flowId, 1);
                var repo = await _flowService.GetFlowHistory(request.FlowId);
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<FlowHistory, GetFlowHistoryQueryDto>()).CreateMapper();
                var result = mapper.Map<List<GetFlowHistoryQueryDto>>(repo);
                //var result = _mapper.Map<List<GetFlowHistoryQueryDto>>(repo);
                return result;
            }
            catch (HoushmandBaseException ex)
            {
                //flowTrackingInstance.SetStatus(flowStep, 4);

                throw ex;
            }
        }
    }
} 

