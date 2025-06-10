using ucondo_challenge.business.Enum;

namespace ucondo_challenge.business.Entities
{
    public sealed class ChartOfAccountsEntity : EntityBase
    {
        public Guid TenantId { get; set; }
        public AccountType Type { get; set; }
        public string Name { get; set; }
        public bool AllowEntries { get; set; }
        public string Code { get; set; }
        public Guid? ParentId { get; set; }
        public ChartOfAccountsEntity? Parent { get; set; }
    }
}
