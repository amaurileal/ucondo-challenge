using MediatR;
using ucondo_challenge.application.ChartOfAccounts.Commands.Dtos;

namespace ucondo_challenge.application.ChartOfAccounts.Queries.GetAll;

public class ChartOfAccountsGetAllQuery(Guid tenantId) : IRequest<IEnumerable<ChartOfAccountsDto>>
{
    public Guid TenantId { get; set; } = tenantId;
}