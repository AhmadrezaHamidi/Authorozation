using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Application.Dtos.ConfigDtos
{
    public class JwtSettings
    {
        public TimeSpan TokenLifetime { get; set; }
        public string Secret { get; set; }
    }
}
