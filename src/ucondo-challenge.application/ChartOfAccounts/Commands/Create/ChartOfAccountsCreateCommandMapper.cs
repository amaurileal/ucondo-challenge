using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ucondo_challenge.business.Entities;

namespace ucondo_challenge.application.ChartOfAccounts.Commands.Create
{
    public static class ChartOfAccountsUpdateCommandMapper
    {
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
    }
}
