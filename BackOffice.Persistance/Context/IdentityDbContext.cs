using BackOffice.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Persistance.Context
{
    public class IdentityDbContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public IdentityDbContext(DbContextOptions options):base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }


    //public class TicketingDbContext : IdentityDbContext<>
    //{
    //    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;
    //    public TicketingDbContext(DbContextOptions options,
    //        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) : base(options)
    //    {
    //        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    //    }
    //    protected override void OnModelCreating(ModelBuilder builder)
    //    {
    //        base.OnModelCreating(builder);
    //        var entitiesAssembly = typeof(IEntity).Assembly;
    //        builder.RegisterAllEntities<IEntity>(entitiesAssembly);
    //        builder.ApplyConfigurationsFromAssembly(typeof(TicketingDbContext).Assembly);
    //    }
    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    {
    //        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    //    }
    //}

}
