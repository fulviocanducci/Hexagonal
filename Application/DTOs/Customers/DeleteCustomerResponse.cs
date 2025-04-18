namespace Application.DTOs.Customers
{
   public record class DeleteCustomerResponse
      (
         CustomerResponse Customer, 
         string Message = "deleted"
      );
  
}
