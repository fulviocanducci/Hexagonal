using System;
namespace Core.Entities
{
   public class Customer
   {
      public long Id { get; set; } = 0;      
      public string Name { get; set; } = string.Empty;      
      public DateTime? DateOfBirth { get; set; }
   }
}