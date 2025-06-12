using MediatR;

namespace ucondo_challenge.application.ChartOfAccounts.Commands.Delete
{
    public sealed class ChartOfAccountsDeleteCommand(Guid TenantId, Guid id) : IRequest
    {
        public Guid TenantId { get; set; }
        public Guid Id { get; set; } = id;
    }
}
