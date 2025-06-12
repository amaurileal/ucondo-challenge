using MediatR;
using ucondo_challenge.application.ChartOfAccounts.Commands.Dtos;

namespace ucondo_challenge.application.ChartOfAccounts.Queries.GetByParent;

public class ChartOfAccountsGetByIdQuery(Guid tenantId, Guid id) : IRequest<ChartOfAccountsDto>
{
    public Guid TenantId { get; set; } = tenantId;
    public Guid Id { get; set; } = id;
}