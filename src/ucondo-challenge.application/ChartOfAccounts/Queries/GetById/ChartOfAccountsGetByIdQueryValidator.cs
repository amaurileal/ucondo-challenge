using FluentValidation;

namespace ucondo_challenge.application.ChartOfAccounts.Queries.GetByParent;

public class ChartOfAccountsGetByIdQueryValidator : AbstractValidator<ChartOfAccountsGetByParentQuery>
{
    public ChartOfAccountsGetByIdQueryValidator()
    {
        RuleFor(request => request.TenantId)
            .NotEmpty().WithMessage("TenantId is required.");
        
        RuleFor(request => request.ParentId)
            .NotEmpty().WithMessage("ParentId is required.");
    }
}