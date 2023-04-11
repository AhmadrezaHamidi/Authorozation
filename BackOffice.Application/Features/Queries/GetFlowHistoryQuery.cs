using Houshmand.Framework.Configuration;
using Houshmand.Framework.Core.Common.Models;
using Houshmand.Framework.ExceptionHandler;
using Houshmand.Framework.Logging;
using Houshmand.Framework.WorkFlow;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Application.Features.Queries
{
    internal class GetFlowHistoryQuery
    {
    }


    //public class GetCardBalanceQuery : CardAuthenticationModel, IRequest<GetCardBalanceResponseDto>
    //{
    //    public string ExpireMonth { get; set; }
    //    public string ExpireYear { get; set; }
    //}

    //public class GetCardBalanceHandler : IRequestHandler<GetCardBalanceQuery, GetCardBalanceResponseDto>
    //{
    //    private readonly ICardBalanceService _cardBalanceService;

    //    private readonly ILogger<GetCardBalanceQuery> _logger;
    //    private readonly IFlowTracking _flowTracking;
    //    private readonly IConfigReader _config;

    //    public GetCardBalanceHandler(ICardBalanceService cardBalanceService, IFlowTracking flowTracking, IConfigReader config)
    //    {
    //        _logger = new LogBuilder()
    //            .WriteTo.Console()
    //            .CreateLogger<GetCardBalanceQuery>();
    //        _cardBalanceService = cardBalanceService;
    //        _flowTracking = flowTracking;
    //        _config = config;
    //    }

    //    public async Task<GetCardBalanceResponseDto> Handle(GetCardBalanceQuery request, CancellationToken cancellationToken)
    //    {
    //        try
    //        {
    //            #region Create Flow

    //            var flowId = _flowTracking.CreateFlow(2, 1, new("0.0.11"));

    //            #endregion Create Flow

    //            //TODO:Add Ip Address Financial Transaction Info Key

    //            #region Send Request To Core

    //            var flowStep = _flowTracking.GoToNextStep(flowId, 1);
    //            var requestModel = new CoreRequestDto<CardBalanceInfoDto>
    //            {
    //                //TODO:Get User Name And Pass From Config
    //                Username = _config.GetChannelConfig().UserName ?? "",
    //                Password = _config.GetChannelConfig().Password ?? "",
    //                RequestId = "TODO",
    //                RequestData = new CardBalanceInfoDto
    //                {
    //                    Pin2 = request.Pin2,
    //                    Pan = request.CardNumber,
    //                    PhoneNumber = "TODO"
    //                },
    //                UserId = "TODO",
    //                TransactionId = flowId
    //            };
    //            CardbalanceResponseDto coreResult = new();
    //            try
    //            {
    //                coreResult = await _cardBalanceService.GetCardbalanceAsync(requestModel);
    //                if (!string.IsNullOrEmpty(coreResult.ErrorMessage) ||
    //                    !string.IsNullOrWhiteSpace(coreResult.ErrorMessage))
    //                {
    //                    _flowTracking.SetStatus(flowStep, 4);
    //                    throw new HoushmandBaseException(ExceptionCriteria.Application, ExceptionType.ValidationFailed, ExceptionLevel.Error, coreResult.ErrorMessage);
    //                }
    //                _flowTracking.SetStatus(flowStep, 3);
    //            }
    //            catch
    //            {
    //                _flowTracking.SetStatus(flowStep, 4);

    //                throw new HoushmandBaseException(ExceptionCriteria.Infrastructure, ExceptionType.ExternalServiceFailed, ExceptionLevel.Error, "خطای ارتباط با سرویس خارجی");
    //            }

    //            #endregion Send Request To Core

    //            #region UpdateFlow

    //            var finalFlowStep = _flowTracking.GoToNextStep(flowId, 1);
    //            _flowTracking.SetStatus(finalFlowStep, 3);

    //            #endregion UpdateFlow

    //            return new GetCardBalanceResponseDto
    //            {
    //                BalanceDate = coreResult.CardBalanceDate,
    //                AvailableBalance = coreResult.CurrentBalance,
    //                ConsumableBalance = coreResult.ReceivableBalance
    //            };
    //        }
    //        catch (HoushmandBaseException ex)
    //        {
    //            //flowTrackingInstance.SetStatus(flowStep, 4);

    //            throw ex;
    //        }
    //    }
    //}

    //public static class FlowTypeName
    //{
    //    public static string CardBalance => nameof(CardBalance);
    //}
}
