using AutoMapper.Configuration;
using BackOffice.Application.Contracts;
using BackOffice.Domain.Entities.Users;
using BackOffice.Persistance.Context;
using BackOffice.Persistance.Identity.Manager;
using BackOffice.Persistance.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using BackOffice.Application.Dtos.ConfigDtos;
using BackOffice.Persistance.Repositories;

namespace BackOffice.Persistance
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            services.AddDbContext<IdentityDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Local"));
                //options.UseInMemoryDatabase("TicketingDb");
            });
            //services.AddEntityFrameworkSqlServer()
            //   .AddDbContext<IdentityDbContext>();


            //services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            //services.AddScoped<ITicketService, TicketService>();
            //services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFlowService, FlowService>();

            services.AddIdentity<User, Role>(option => option.SignIn.RequireConfirmedAccount = false)
                        .AddEntityFrameworkStores<IdentityDbContext>()
                                   .AddDefaultTokenProviders();
            ///TODO NIMIDonam Kodomo bezaram ya mele khodam ya 


            services.AddScoped<ITokenInfoService, TokenInfoService>();
            

            var jwtSettings = new JwtSettings();
            configuration.Bind(key: nameof(jwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);

            services.Configure<IdentityOptions>(options =>
            {
                // Default Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;

                options.User.RequireUniqueEmail = false;
                options.Lockout.AllowedForNewUsers = false;
                options.SignIn.RequireConfirmedEmail = false;
            });


            services.AddScoped<AppUserManager>();
            services.AddScoped<AppSignInManager>();
            services.AddScoped<AppRoleManager>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                      JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme =
                      JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options =>
           {
               options.RequireHttpsMetadata = false;
               options.SaveToken = true;
               options.ClaimsIssuer = configuration["Authentication:JwtIssuer"];

               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidIssuer = configuration["Authentication:JwtIssuer"],

                   ValidateAudience = true,
                   ValidAudience = configuration["Authentication:JwtAudience"],

                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:JwtKey"])),
                   RequireExpirationTime = true,
                   ValidateLifetime = true,
                   ClockSkew = TimeSpan.Zero
               };
           });


            services.Configure<DataProtectionTokenProviderOptions>(opt => opt.TokenLifespan = TimeSpan.FromDays(3));


            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;


        }



    }

}
