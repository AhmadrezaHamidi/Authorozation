using BackOffice.Application.Contracts;
using BackOffice.Domain.Entities;
using Houshmand.Framework.DataAccess.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Persistance.Repositories
{
    public class FlowService : IFlowService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FlowService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<FlowHistory>> GetFlowHistory(string flowId)
        {
            var query = await _unitOfWork.
                ExecuteStoredProcedureAsync<FlowHistory>("GetFlowHistory", new { @flowId = flowId });
            var res = query.ToList();
            return res;
        }

        public async Task<List<StatusHistory>> GetStatusHistoryFlowActivity(string flowActivityUniqueId)
        {
            var query = await _unitOfWork.
          ExecuteStoredProcedureAsync<StatusHistory>
          ("GetStatusHistoryFlowActivity", new { @flowActivityUniqueId = flowActivityUniqueId });

            var res = query.ToList();
            return res;
        }

        public async Task<List<Top10FlowsWithStatus>> GetTop10FlowsWithStatus(int userId)
        {
            var query = await _unitOfWork.
          ExecuteStoredProcedureAsync<Top10FlowsWithStatus>
          ("GetTop10FlowsWithStatus", new { @userId = userId });

            var res = query.ToList();
            return res;
        }
    }
}
