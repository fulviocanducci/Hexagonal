using Application.DTOs.Customers;
using Core.Entities;

namespace API.Extensions
{
   public static class ModelToResponse
   {
      public static CustomerResponse? ToResponse(this Customer customer)
      {
         if (customer == null)
         {
            return null;
         }
         return new CustomerResponse(customer.Id, customer.Name, customer.DateOfBirth);
      }

      public static Customer ToModel(this AddCustomerRequest request)
      {
         return new Customer
         {
            Name = request.Name,
            DateOfBirth = request.DateOfBirth
         };
      }

      public static Customer ToModel(this UpdateCustomerRequest request)
      {
         return new Customer
         {
            Id = request.Id,
            Name = request.Name,
            DateOfBirth = request.DateOfBirth
         };
      }

      public static void ToModel(this UpdateCustomerRequest request, Customer customer)
      {         
         customer.Name = request.Name;
         customer.DateOfBirth = request.DateOfBirth;
      }
   }
}
