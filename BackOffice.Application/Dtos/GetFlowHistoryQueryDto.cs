using Houshmand.Framework.WorkFlow.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Application.Dtos
{
    public  class GetFlowHistoryQueryDto
    {
        public string FlowActivityTypeTitle { get; set; }
        public string StatusTypeTitle { get; set; }
        public int statusId { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
