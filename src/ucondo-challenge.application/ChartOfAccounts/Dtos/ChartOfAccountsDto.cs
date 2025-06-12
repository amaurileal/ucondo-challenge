using ucondo_challenge.business.Entities;
using ucondo_challenge.business.Enum;

namespace ucondo_challenge.application.ChartOfAccounts.Commands.Dtos;

public sealed class ChartOfAccountsDto
{
    public AccountType Type { get; set; }
    public string Name { get; set; }
    public bool AllowEntries { get; set; }
    public string Code { get; set; }
    public Guid? ParentId { get; set; }
    public ChartOfAccountsEntity? Parent { get; set; }
}