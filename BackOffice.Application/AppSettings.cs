using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Application
{
    internal class ConfigurationManager
    {
        public static T GetSection<T>(string sectionName)
        {
            var environment = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                //.AddUserSecrets<Program>()
                .AddEnvironmentVariables();

            var configurationRoot = builder.Build();

            return configurationRoot.GetSection(sectionName).Get<T>();
        }
    }

    internal static class AppSettings
    {
        public static string ConnectionString => ConfigurationManager.GetSection<string>("ConnectionStrings:production");
        public static string LocalConnectionString => ConfigurationManager.GetSection<string>("ConnectionStrings:Local");
        public static string Server => ConfigurationManager.GetSection<string>("ConnectionString:server");
    }
}
