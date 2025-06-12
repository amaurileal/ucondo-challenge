using FluentValidation;

namespace ucondo_challenge.application.ChartOfAccounts.Commands.Delete
{
    public class ChartOfAccountsDeleteCommandValidator : AbstractValidator<ChartOfAccountsDeleteCommand>
    {
        public ChartOfAccountsDeleteCommandValidator()
        {
            RuleFor(x => x.TenantId)
                .NotEmpty().WithMessage("TenantId is required.");
            
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");
        }
    }
}
