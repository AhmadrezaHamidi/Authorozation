using FluentValidation;
using Houshmand.Framework.Configuration.Files;
using Houshmand.Framework.DataAccess.Dapper;
using Houshmand.Framework.WorkFlow.Extensions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Application
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, string connection)
        {
            services.AddScoped<IUnitOfWork>(u => new UnitOfWork(connection));
            services.AddFileConfigurationServices("Sapp", new Version("0.0.0.11"), "/");
            services.AddFlow(connection);
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
