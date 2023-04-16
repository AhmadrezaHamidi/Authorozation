using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Domain.Entities
{
    public class StatusHistory
    {
        public DateTime CreatedAt { get; set; }
        public string StatusTypeTitle { get; set; }
        public string StatusTypeName { get; set; }
        public string FlowActivityUniqueId { get; set; }
    }

}
