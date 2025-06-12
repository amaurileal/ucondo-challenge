using MediatR;
using ucondo_challenge.application.ChartOfAccounts.Commands.Dtos;

namespace ucondo_challenge.application.ChartOfAccounts.Queries.GetByParent;

public class ChartOfAccountsGetNextCodeByParentQuery(Guid tenantId, Guid parentId) : IRequest<string>
{
    public Guid TenantId { get; set; } = tenantId;
    public Guid ParentId { get; set; } = parentId;
}