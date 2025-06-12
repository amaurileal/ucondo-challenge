using MediatR;
using ucondo_challenge.application.ChartOfAccounts.Commands.Dtos;

namespace ucondo_challenge.application.ChartOfAccounts.Queries.GetAllParents;

public class ChartOfAccountsGetAllParentsQuery(Guid tenantId) : IRequest<IEnumerable<ChartOfAccountsDto>>
{
    public Guid TenantId { get; set; } = tenantId;
}