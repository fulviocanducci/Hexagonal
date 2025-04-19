using Application.DTOs.Customers;
using Canducci.Pagination;
using Core.Entities;
using Mapster;

namespace API
{
   public static class Injections
   {
      public static IServiceCollection AddApplicationDefault(this IServiceCollection services)
      {
         services.Configure<RouteOptions>(options =>
         {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
         });
         services.AddCors(options =>
         {
            options.AddDefaultPolicy(
               builder =>
               {
                  builder.AllowAnyOrigin()
                         .AllowAnyMethod()
                         .AllowAnyHeader();
               });
         });
         return services;
      }
   }
   public static class MapsterInjections
   {
      public static IServiceCollection AddMapsters(this IServiceCollection services)
      {
         TypeAdapterConfig<Customer, CustomerResponse>
            .NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.DateOfBirth, src => src.DateOfBirth);

         //TypeAdapterConfig.GlobalSettings.ForType(typeof(PaginatedRest<>), typeof(PaginatedRest<>))
         //   .Map("Current", "Current")
         //   .Map("Total", "Total")
         //   .Map("Items", "Items")
         //   .Map("PageCount", "PageCount")
         //   .Map("HasNextPage", "HasNextPage")
         //   .Map(".HasPreviousPage", "HasPreviousPage")
         //   .MapToConstructor(true);

         //TypeAdapterConfig<PaginatedRest<Customer>, PaginatedRest<CustomerResponse>>
         //   .NewConfig()
         //   .Map("Current", "Current")
         //   .Map("Total", "Total")
         //   .Map("Items", "Items")
         //   .Map("PageCount", "PageCount")
         //   .Map("HasNextPage", "HasNextPage")
         //   .Map(".HasPreviousPage", "HasPreviousPage")
         //   .MapToConstructor(false);        

         return services;
      }
   }
}
