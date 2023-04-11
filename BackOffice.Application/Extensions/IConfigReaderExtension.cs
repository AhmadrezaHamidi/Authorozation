using BackOffice.Application.Dtos.ConfigDtos;
using Houshmand.Framework.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Application.Extensions
{
    public static class IConfigReaderExtension
    {
        public static BackOfficeConfig GetChannelConfig(this IConfigReader configReader)
        {
            return configReader.Get<BackOfficeConfig>(nameof(BackOfficeConfig)) ?? new BackOfficeConfig();
        }
    }
}
