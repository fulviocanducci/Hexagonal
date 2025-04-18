using Core.Entities;
using Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Data
{
   public class AppDbContext: DbContext
   {
      public DbSet<Customer> Customers { get; set; }
      public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         base.OnModelCreating(modelBuilder);
         modelBuilder.ApplyConfiguration(CustomerMapping.Instance);
      }
   }
}
