using MediatR;
using ucondo_challenge.application.ChartOfAccounts.Commands.Dtos;

namespace ucondo_challenge.application.ChartOfAccounts.Queries.GetByParent;

public class ChartOfAccountsGetByParentQuery(Guid tenantId, Guid parentId) : IRequest<IEnumerable<ChartOfAccountsDto>>
{
    public Guid TenantId { get; set; } = tenantId;
    public Guid ParentId { get; set; } = parentId;
}