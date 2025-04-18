using Application.DTOs.Customers;
using Core.Entities;

namespace API.Extensions
{
   public static class CustomerExtension
   {
      public static CustomerResponse? ToCustomerResponse(this Customer customer)
      {
         if (customer == null)
         {
            return null;
         }
         return new CustomerResponse(customer.Id, customer.Name, customer.DateOfBirth);
      }

      public static Customer ToCustomer(this AddCustomerRequest request)
      {
         return new Customer
         {
            Name = request.Name,
            DateOfBirth = request.DateOfBirth
         };
      }

      public static Customer ToCustomer(this UpdateCustomerRequest request)
      {
         return new Customer
         {
            Id = request.Id,
            Name = request.Name,
            DateOfBirth = request.DateOfBirth
         };
      }

      public static void ToCustomerUpdate(this UpdateCustomerRequest request, Customer customer)
      {
         customer.Name = request.Name;
         customer.DateOfBirth = request.DateOfBirth;
      }

      public static DeleteCustomerResponse ToCustomerDelete(this Customer customer)
      {
         return new DeleteCustomerResponse(customer.ToCustomerResponse());         
      }
   }
}
