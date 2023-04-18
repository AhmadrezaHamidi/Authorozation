using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Domain.Common
{
    public abstract class BaseAuditableEntity:BaseEntity
    {
        public DateTime Created { get; set; }

        public DateTime? LastModified { get; set; }

    }
}
