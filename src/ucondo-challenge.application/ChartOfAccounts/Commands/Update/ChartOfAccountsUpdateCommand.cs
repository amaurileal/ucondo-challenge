using MediatR;
using System.Text.Json;
using System.Text.Json.Serialization;
using ucondo_challenge.business.Enum;

namespace ucondo_challenge.application.ChartOfAccounts.Commands.Update
{
    public sealed class ChartOfAccountsUpdateCommand : IRequest
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        [JsonIgnore] 
        public Guid TenantId { get; set; }
        public AccountType Type { get; set; } 
        public string? Name { get; set; }
        public bool AllowEntries { get; set; }
        public string? Code { get; set; }
        public Guid? ParentId { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
