using FluentValidation;

namespace ucondo_challenge.application.ChartOfAccounts.Queries.GetAll;

public class ChartOfAccountsGetAllQueryValidator : AbstractValidator<ChartOfAccountsGetAllQuery>
{
    public ChartOfAccountsGetAllQueryValidator()
    {
        RuleFor(request => request.TenantId)
            .NotEmpty().WithMessage("TenantId is required.");
    }
}