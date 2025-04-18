namespace Application.DTOs.Customers
{
   public record UpdateCustomerRequest(long Id, string Name, DateTime DateOfBirth);
}
