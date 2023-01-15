using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class Configs
    {
        public string TokenKey { get; set; }
        public int TokenTimeout { get; set; }
        public int RefreshTokenTimeout { get; set; }
    }
}
