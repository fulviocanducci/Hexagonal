using System;
namespace Core.Entities
{
   public class Customer
   {
      public Customer() { }

      public Customer(string name, DateTime dateOfBirth)
      {
         Name = name;
         DateOfBirth = dateOfBirth;
      }

      public Customer(long id, string name, DateTime dateOfBirth)
      {
         Id = id;
         Name = name;
         DateOfBirth = dateOfBirth;
      }

      public Customer SetName(string name)
      {
         if (string.IsNullOrWhiteSpace(name))
         {
            throw new ArgumentException("Name cannot be null or empty", nameof(name));
         }
         Name = name;
         return this;
      }

      public Customer SetDateOfBirth(DateTime dateOfBirth)
      {         
         DateOfBirth = dateOfBirth;
         return this;
      }

      public long Id { get; set; } = 0;
      public string Name { get; set; } = string.Empty;      
      public DateTime DateOfBirth { get; set; }
   }
}