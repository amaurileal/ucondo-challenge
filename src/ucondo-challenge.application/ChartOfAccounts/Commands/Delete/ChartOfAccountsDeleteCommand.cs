using MediatR;

namespace ucondo_challenge.application.ChartOfAccounts.Commands.Delete
{
    public sealed class ChartOfAccountsDeleteCommand(Guid tenantId, Guid id) : IRequest
    {
        public Guid TenantId { get; set; } = tenantId;
        public Guid Id { get; set; } = id;
    }
}
