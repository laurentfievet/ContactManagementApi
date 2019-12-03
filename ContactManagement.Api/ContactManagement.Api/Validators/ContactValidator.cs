using ContactManagement.Repo.Models;
using FluentValidation;

namespace ContactManagement.Api.Validators
{
    public class ContactValidator : AbstractValidator<ContactDTO>
    {
        public ContactValidator()
        {

            RuleFor(v => v.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(v => v.LastName).NotEmpty().MaximumLength(50);
            RuleFor(v => v.GSMNumber).NotEmpty().MaximumLength(20);
            RuleFor(v => v.IsFreelance).NotNull();
            RuleFor(v => v.VATNumber).NotEmpty().MaximumLength(20).When(x => x.IsFreelance == true);
            RuleFor(v => v.Address).SetValidator(new AddressValidator());



        }
    }
}
