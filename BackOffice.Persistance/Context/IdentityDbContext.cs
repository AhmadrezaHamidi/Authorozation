using BackOffice.Application.Dtos.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BackOffice.Persistance.Context
{


    public class IdentityDbContext : IdentityDbContext<IdentityUser>
    {


        public virtual DbSet<UserRefreshTokens> UserRefreshToken { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("Local");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}