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

namespace BackOffice.Application.Features.Queries
{
    public class GetTop10FlowsWithStatusQuery : IRequest<List<GetTop10FlowsWithStatusQueryDto>>
    {
        public GetTop10FlowsWithStatusQuery(int flowActivityUniqueId)
        {
            this.flowActivityUniqueId = flowActivityUniqueId;
        }
        public int flowActivityUniqueId { get; set; }
    }


    public class GetTop10FlowsWithStatusQueryHandler
        : IRequestHandler<GetTop10FlowsWithStatusQuery, List<GetTop10FlowsWithStatusQueryDto>>
    {
        private readonly IFlowService _flowService;
        private readonly ILogger<GetTop10FlowsWithStatusQueryHandler> _logger;
        private readonly IFlowTracking _flowTracking;
        private readonly IConfigReader _config;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTop10FlowsWithStatusQueryHandler(IFlowService flowService,
            IFlowTracking flowTracking,
            IConfigReader config,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _logger = new LogBuilder()
                .WriteTo.Console()
                .CreateLogger<GetTop10FlowsWithStatusQueryHandler>();
            _flowService = flowService;
            _flowTracking = flowTracking;
            _config = config;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        async Task<List<GetTop10FlowsWithStatusQueryDto>> IRequestHandler<GetTop10FlowsWithStatusQuery, List<GetTop10FlowsWithStatusQueryDto>>.Handle(GetTop10FlowsWithStatusQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var flowId = _flowTracking.CreateFlow(2, 1, new("0.0.11"));
                var flowStep = _flowTracking.GoToNextStep(flowId, 1);
                var repo = await _flowService.GetTop10FlowsWithStatus(request.flowActivityUniqueId);
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Top10FlowsWithStatus, GetTop10FlowsWithStatusQueryDto>()).CreateMapper();
                var result = mapper.Map<List<GetTop10FlowsWithStatusQueryDto>>(repo);
                return result;
            }
            catch (HoushmandBaseException ex)
            {
                throw ex;
            }
        }
    }
}
