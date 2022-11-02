using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AhmadBase.Web.Dtos
{
    public class RegisterInputDto
    {
        public string Email { get; set; }
        public string PassWord { get; set; }
        public string PassWordRepeat { get; set; }
        public string Phone { get; set; }

    }



}
}
