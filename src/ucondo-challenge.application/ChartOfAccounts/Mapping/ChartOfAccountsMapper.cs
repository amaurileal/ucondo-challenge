using ucondo_challenge.application.ChartOfAccounts.Commands.Create;
using ucondo_challenge.application.ChartOfAccounts.Commands.Dtos;
using ucondo_challenge.application.ChartOfAccounts.Commands.Update;
using ucondo_challenge.business.Entities;

namespace ucondo_challenge.application.ChartOfAccounts.Mapping
{
    public static class ChartOfAccountsMapper
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

        public static ChartOfAccountsEntity MapCommandCreateToEntity(this ChartOfAccountsCreateCommand command)
        {
            return new ChartOfAccountsEntity
            {
                TenantId = command.TenantId,
                Type = command.Type,
                Name = command.Name,
                AllowEntries = command.AllowEntries,
                Code = command.Code,
                ParentId = command.ParentId
            };
        }

        public static ChartOfAccountsDto MapEntityToDto(this ChartOfAccountsEntity entity)
        {
            return new ChartOfAccountsDto
            {
                Id = entity.Id,
                AllowEntries = entity.AllowEntries,
                Code = entity.Code,
                ParentId = entity.ParentId,
                Name = entity.Name,
                Parent = entity.Parent,
                Type = entity.Type
            };
        }
    }
}
