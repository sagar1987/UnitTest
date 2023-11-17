using Lakeshore.SpecialOrderPickupStatus.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace Lakeshore.SpecialOrderPickupStatus.Infrastructure.EntityModelConfiguration;

public class SpecialOrderDbContext : DbContext
{
    public SpecialOrderDbContext(DbContextOptions<SpecialOrderDbContext> options)
        : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }

   
    public virtual DbSet<OrderShipping> OrderShipping { get; set; }
    public virtual DbSet<OrderLine> OrderLine { get; set; }

}
