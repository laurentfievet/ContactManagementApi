using ContactManagement.Repo.Models;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManagement.Api.Validators
{
    public class EnterpriseValidator : AbstractValidator<EnterpriseDTOFull>
    {
        public EnterpriseValidator()
        {

            RuleFor(v => v.Name).NotEmpty().MaximumLength(50);
            RuleFor(v => v.TVANumber).NotEmpty().MaximumLength(20);
            RuleFor(v => v.Adresses).Must(list => list.Count >= 1);
            RuleFor(x => x.Adresses).SetValidator(new UniqueInnerCollectionValidator());
            RuleForEach(v => v.Adresses).SetValidator(new AdressValidator());



        }

        public class EnterpriseAdressValidator : AbstractValidator<EnterpriseAdresListDTO>
        {
            public EnterpriseAdressValidator()
            {

                RuleForEach(v => v.enterpriseAdresses).SetValidator(new AdressValidator());
                RuleFor(x => x.enterpriseAdresses).SetValidator(new UniqueInnerCollectionValidator());


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
                var listOfCollection = context.PropertyValue as List<EnterpriseAdressDTO>;
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
