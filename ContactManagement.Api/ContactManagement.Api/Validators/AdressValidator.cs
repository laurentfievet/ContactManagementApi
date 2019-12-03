using ContactManagement.Repo.Models;
using FluentValidation;

namespace ContactManagement.Api.Validators
{
    public class AdressValidator : AbstractValidator<AdressDTO>
    {
        public AdressValidator()
        {

            RuleFor(v => v.Name).NotEmpty().MaximumLength(50);
            RuleFor(v => v.City).NotEmpty().MaximumLength(150);
            RuleFor(v => v.Country).NotEmpty().MaximumLength(50);
            RuleFor(v => v.PostalCode).NotEmpty().MaximumLength(10);
            RuleFor(v => v.Street).NotEmpty().MaximumLength(250);
            RuleFor(v => v.StreetNumber).NotEmpty().MaximumLength(20);

        }
    }

    
}
