using FluentValidation;

namespace ucondo_challenge.application.ChartOfAccounts.Queries.GetAllParents;

public class ChartOfAccountsGetAllParentsQueryValidator : AbstractValidator<ChartOfAccountsGetAllParentsQuery>
{
    public ChartOfAccountsGetAllParentsQueryValidator()
    {
        RuleFor(request => request.TenantId)
            .NotEmpty().WithMessage("TenantId is required.");
    }
}