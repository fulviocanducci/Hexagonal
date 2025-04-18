using Application.DTOs.Customers;
using FluentValidation;
namespace Application.Validators.Customers
{
   public class AddCustomerRequestValidator : AbstractValidator<AddCustomerRequest>
   {
      public AddCustomerRequestValidator()
      {
         RuleFor(x => x.Name)
            .Configure(x => x.PropertyName = "name")
            .NotEmpty().WithMessage("Name is required.")
            .Length(3, 100).WithMessage("Name must be between 3 and 100 characters.");

         RuleFor(x => x.DateOfBirth)
            .Configure(x => x.PropertyName = "dateOfBirth")
            .NotEmpty().WithMessage("Date of birth is required.")
            .LessThan(DateTime.Today).WithMessage("Date of birth must be in the past.");
      }
   }
}