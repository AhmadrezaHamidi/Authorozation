using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Domain.Entities
{
    public class FlowHistory
    {
        public string FlowActivityTypeTitle { get; set; }
        public string StatusTypeTitle { get; set; }
        public int statusId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
