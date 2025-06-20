using Microsoft.EntityFrameworkCore;
using Ecommerce.Order.Domain.Entities;

namespace Ecommerce.Order.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<OrderInfo> Orders => Set<OrderInfo>();
    public DbSet<OrderItem> OrderItem => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderInfo>()
            .HasKey(o => o.Id);

        modelBuilder.Entity<OrderInfo>()
            .HasMany(o => o.Items)
            .WithOne(oi => oi.OrderInfo)
            .HasForeignKey(oi => oi.OrderInfoId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderItem>()
            .Property(oi => oi.Price)
            .HasPrecision(18, 2);
    }

}

