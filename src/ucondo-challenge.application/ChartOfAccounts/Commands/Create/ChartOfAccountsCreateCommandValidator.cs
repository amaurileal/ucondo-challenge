using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ucondo_challenge.application.ChartOfAccounts.Commands.Create
{
    public class ChartOfAccountsCreateCommandValidator : AbstractValidator<ChartOfAccountsCreateCommand>
    {
        public ChartOfAccountsCreateCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            //TODO AJUSTAR PARA QUE NÃO PERMITA MAIS QUE 999 E QUE NÃO PERMINA VALOR DIFERENTE DA MASKARA
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Code is required.")
                .MaximumLength(50).WithMessage("Code must not exceed 50 characters.");


            RuleFor(x => x.TenantId)
                .NotEmpty().WithMessage("TenantId is required.");

            //TODO VERIFICAR POR ENUM
            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Type must be a valid AccountType enum value.");
        }
    }
}
