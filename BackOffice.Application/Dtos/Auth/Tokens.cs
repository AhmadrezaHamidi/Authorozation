using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Application.Dtos.Auth
{
    public class Tokens
    {
        public string Access_Token { get; set; }
        public string Refresh_Token { get; set; }
    }
}
