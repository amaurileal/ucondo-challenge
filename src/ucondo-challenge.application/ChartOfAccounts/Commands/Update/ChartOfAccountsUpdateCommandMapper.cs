using ucondo_challenge.business.Entities;

namespace ucondo_challenge.application.ChartOfAccounts.Commands.Update.Mapping
{
    public static class ChartOfAccountsUpdateCommandMapper
    {
        public static ChartOfAccountsEntity MapCommandUpdateToEntity(this ChartOfAccountsEntity entity, ChartOfAccountsUpdateCommand command)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            entity.Name = command.Name;
            entity.Type = command.Type;
            entity.AllowEntries = command.AllowEntries;
            entity.Code = command.Code;
            entity.ParentId = command.ParentId;
            return entity;
        }
    }
}
