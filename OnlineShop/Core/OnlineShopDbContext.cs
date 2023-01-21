using Core.Entities;
using Core.FluentAPIConfigurations;
using Microsoft.EntityFrameworkCore;

public class OnlineShopDbContext : DbContext
{
    public OnlineShopDbContext(DbContextOptions options) : base(options)
    {
    }

    //public DbSet<Product> Products {get; set;}
    public DbSet<Product> Products => Set<Product>();
    public DbSet<User> Users => Set<User>();
    public DbSet<UserRefreshToken> UserRefreshTokens => Set<UserRefreshToken>(); 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //    modelBuilder.Entity<Product>()
        //             .Property(s => s.ProductName)
        //             .HasColumnName("Title")
        //             .IsRequired();

        //    modelBuilder.Entity<Product>().ToTable("MyProduct");

        modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserRefreshTokenEntityConfiguration());

    }
}