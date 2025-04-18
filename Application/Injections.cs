using Application.Validators.Customers;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
namespace Application
{
   public static class Injections
   {
      public static IServiceCollection AddFluentValidationDefault(this IServiceCollection services)
      {
         services.AddFluentValidationAutoValidation();
         services.AddValidatorsFromAssemblyContaining<AddCustomerRequestValidator>();
         return services;
      }
   }
}
