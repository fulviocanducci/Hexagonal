using Microsoft.AspNetCore.Mvc.ModelBinding;
namespace API.Extensions
{
   public static class APIExtensions
   {
      public static bool IsProblem(this ModelStateDictionary dic)
      {
         return dic.IsValid == false;
      }
   }
}