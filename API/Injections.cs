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
}
