using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
   public static class Injections
   {
      internal static IServiceCollection AddDbContextDefault(this IServiceCollection services, IConfiguration configuration)
      {
         services.AddDbContext<AppDbContext>(options =>
         {
            options.UseMySql
            (
               configuration.GetConnectionString("DefaultConnection"),
               new MySqlServerVersion(new System.Version(8, 0, 42)),
               options => { }
            );
         });
         return services;
      }
      public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
      {
         services.AddDbContextDefault(configuration);
         return services;
      }
   }
}
