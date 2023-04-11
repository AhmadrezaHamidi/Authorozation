using FluentValidation;
using Houshmand.Framework.Configuration.Files;
using Houshmand.Framework.WorkFlow.Extensions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BackOffice.Application
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, string connection)
        {
            services.AddFileConfigurationServices("Sapp", new Version("0.0.0.11"), "/");
            services.AddFlow(connection);
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}