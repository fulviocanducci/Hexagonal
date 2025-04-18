using Application;
using Infrastructure;

namespace API
{
   public class Program
   {
      public static void Main(string[] args)
      {
         // CreateHostBuilder(args).Build().Run();
         WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
         builder.Services.AddControllers();
         builder.Services.AddFluentValidationDefault();
         builder.Services.AddEndpointsApiExplorer();
         builder.Services.AddSwaggerGen();
         builder.Services.AddInfrastructure(builder.Configuration);    
         builder.Services.Configure<RouteOptions>(options =>
         {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
         });
         builder.Services.AddCors(options =>
         {
            options.AddDefaultPolicy(
               builder =>
               {
                  builder.AllowAnyOrigin()
                         .AllowAnyMethod()
                         .AllowAnyHeader();
               });
         });
         // Add services to the container.
         WebApplication app = builder.Build();
         app.UseCors();
         if (app.Environment.IsDevelopment())
         {
            app.UseSwagger();
            app.UseSwaggerUI();
         }
         app.UseHttpsRedirection();
         app.UseAuthorization();
         app.MapControllers();
         app.Run();
      }
   }
}