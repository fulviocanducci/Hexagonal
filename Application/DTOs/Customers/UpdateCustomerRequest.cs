namespace Application.DTOs.Customers
{
   public record UpdateCustomerRequest(int Id, string Name, DateTime DateOfBirth);
}
