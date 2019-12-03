using ContactManagement.Repo.Models;
using FluentValidation;
using FluentValidation.Validators;
using System.Collections.Generic;
using System.Linq;

namespace ContactManagement.Api.Validators
{
    public class EnterpriseValidator : AbstractValidator<EnterpriseDTOFull>
    {
        public EnterpriseValidator()
        {
            RuleFor(v => v.Name).NotEmpty().MaximumLength(50);
            RuleFor(v => v.VATNumber).NotEmpty().MaximumLength(20);
            RuleFor(v => v.Addresses).Must(list => list.Count >= 1);
            RuleFor(x => x.Addresses).SetValidator(new UniqueInnerCollectionValidator());
            RuleForEach(v => v.Addresses).SetValidator(new AddressValidator());
        }

        public class EnterpriseAddressValidator : AbstractValidator<EnterpriseAddresListDTO>
        {
            public EnterpriseAddressValidator()
            {
                RuleForEach(v => v.enterpriseAddresses).SetValidator(new AddressValidator());
                RuleFor(x => x.enterpriseAddresses).SetValidator(new UniqueInnerCollectionValidator());
            }
        }

        public class UniqueInnerCollectionValidator : PropertyValidator
        {
            public UniqueInnerCollectionValidator()
                : base("Only one HeadQuarter must be set to: {symbols}")
            {

            }

            protected override bool IsValid(PropertyValidatorContext context)
            {
                var listOfCollection = context.PropertyValue as List<EnterpriseAddressDTO>;
                if (listOfCollection == null)
                {
                    return true;
                }

                var nonUniqueKeys = listOfCollection.Where(x => x.HeadOffice == true).GroupBy(x => x.HeadOffice).Where(x => x.Count() > 1).Select(x => x.Key).ToList();

                if (nonUniqueKeys.Count > 0)
                {
                    string failedItems = string.Join(", ", nonUniqueKeys.ToArray());
                    context.MessageFormatter.AppendArgument("symbols", failedItems);
                    return false;
                }

                return true;
            }
        }


    }
}
