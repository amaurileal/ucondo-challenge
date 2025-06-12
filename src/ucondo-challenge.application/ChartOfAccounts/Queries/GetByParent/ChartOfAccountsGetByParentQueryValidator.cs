using FluentValidation;

namespace ucondo_challenge.application.ChartOfAccounts.Queries.GetByParent;

public class ChartOfAccountsGetByParentQueryValidator : AbstractValidator<ChartOfAccountsGetByParentQuery>
{
    public ChartOfAccountsGetByParentQueryValidator()
    {
        RuleFor(request => request.TenantId)
            .NotEmpty().WithMessage("TenantId is required.");
        
        RuleFor(request => request.ParentId)
            .NotEmpty().WithMessage("ParentId is required.");
    }
}