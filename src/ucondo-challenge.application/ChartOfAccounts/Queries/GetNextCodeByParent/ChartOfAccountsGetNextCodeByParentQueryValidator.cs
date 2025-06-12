using FluentValidation;

namespace ucondo_challenge.application.ChartOfAccounts.Queries.GetByParent;

public class ChartOfAccountsGetNextCodeByParentQueryValidator : AbstractValidator<ChartOfAccountsGetByParentQuery>
{
    public ChartOfAccountsGetNextCodeByParentQueryValidator()
    {
        RuleFor(request => request.TenantId)
            .NotEmpty().WithMessage("TenantId is required.");
        
        RuleFor(request => request.ParentId)
            .NotEmpty().WithMessage("ParentId is required.");
    }
}