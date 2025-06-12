using ucondo_challenge.application.ChartOfAccounts.Commands.Create;

namespace ucondo_challenge.api.ViewModels.ChartOfAccounts.Create.Mapping
{
    public static class ChartOfAccountsCreateViewModelMapper
    {
        public static ChartOfAccountsCreateCommand ToCreateCommand(this ChartOfAccountsCreateViewModel viewModel,Guid tenantId)
        {
            return new ChartOfAccountsCreateCommand
            {
                TenantId = tenantId,
                Type = viewModel.Type,
                Name = viewModel.Name,
                AllowEntries = viewModel.AllowEntries,
                Code = viewModel.Code,
                ParentId = viewModel.ParentId
            };
        }
    }
}
