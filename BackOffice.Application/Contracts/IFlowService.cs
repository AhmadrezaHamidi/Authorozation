using BackOffice.Application.Dtos;
using BackOffice.Application.Dtos.Auth;
using BackOffice.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Application.Contracts
{
    public interface IFlowService
    {
        Task<List<FlowHistory>> GetFlowHistory(string flowId);
        Task<List<StatusHistory>> GetStatusHistoryFlowActivity(string flowActivityUniqueId);
        Task<List<Top10FlowsWithStatus>> GetTop10FlowsWithStatus(int userId);
    }

    
    
}
