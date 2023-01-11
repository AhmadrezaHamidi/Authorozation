using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AhmadBase.Core.Dtos;
using AhmadBase.Inferastracter.Datas.Entities;
using AhmadBase.Web.Commands;
using AhmadBase.Web.Dtos;
using Mapster;

namespace AhmadBase.Web
{
    public class MapsterDtosConfigurations
    {
        static MapsterDtosConfigurations _instance;

        public static MapsterDtosConfigurations Instance =>
            _instance ?? (_instance = new MapsterDtosConfigurations());

        public void Initialize()
        {
        }

    }
}