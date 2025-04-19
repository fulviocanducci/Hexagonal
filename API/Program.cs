using Application;
using Infrastructure;
using Mapster;
using MapsterMapper;

namespace API
{
   public class Program
   {
      public static void Main(string[] args)
      {
         // Add WebApplicationBuilder
         WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
         ConfigurationManager configurationManager = builder.Configuration;
         
         // Services
         builder.Services.AddControllers();
         builder.Services.AddFluentValidationDefault();
         builder.Services.AddEndpointsApiExplorer();
         builder.Services.AddSwaggerGen();
         builder.Services.AddInfrastructure(configurationManager);
         builder.Services.AddApplicationDefault();
         builder.Services.AddMapster();
         builder.Services.AddMapsters();

         // Add WebApplication
         WebApplication app = builder.Build();

         // Uses
         if (app.Environment.IsDevelopment())
         {
            app.UseSwagger();
            app.UseSwaggerUI();
         }
         app.UseCors();
         app.UseHttpsRedirection();
         app.UseAuthorization();
         app.MapControllers();
         app.Run();
      }
   }
}