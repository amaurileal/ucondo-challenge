using System.ComponentModel;
using System.Text.Json.Serialization;
using ucondo_challenge.business.Enum;

namespace ucondo_challenge.api.ViewModels.ChartOfAccounts.Update
{
    public class ChartOfAccountsUpdateViewModel
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AccountType Type { get; set; }
        public string? Name { get; set; }
        public bool AllowEntries { get; set; }

        [DefaultValue("1")]
        public string? Code { get; set; }
        public Guid? ParentId { get; set; }
    }
}
