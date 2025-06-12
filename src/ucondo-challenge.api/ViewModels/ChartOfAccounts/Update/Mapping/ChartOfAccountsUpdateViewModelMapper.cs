using ucondo_challenge.application.ChartOfAccounts.Commands.Update;

namespace ucondo_challenge.api.ViewModels.ChartOfAccounts.Update.Mapping
{
    public static class ChartOfAccountsUpdateViewModelMapper
    {
        public static ChartOfAccountsUpdateCommand ToUpdateCommand(this ChartOfAccountsUpdateViewModel viewModel,Guid tenantId,Guid Id)
        {
            return new ChartOfAccountsUpdateCommand
            {
                Id = Id,
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
