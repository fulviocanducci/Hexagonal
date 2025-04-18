using Core.Interfaces;
using Core.Services;
using Infrastructure.Data;
using Infrastructure.Repositories;
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
         services.AddScoped<IUnitOfWork, UnitOfWork>();
         return services;
      }
      
      internal static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
      {
         services.AddScoped<ICustomerRepository, CustomerRepository>();
         return services;
      }
      
      internal static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
      {
         services.AddScoped<ICustomerService, CustomerService>();
         return services;
      }

      public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
      {
         services.AddDbContextDefault(configuration);
         services.AddRepositories(configuration);
         services.AddServices(configuration);
         return services;
      }
   }
}
