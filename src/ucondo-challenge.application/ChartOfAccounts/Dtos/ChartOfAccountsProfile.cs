using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ucondo_challenge.application.ChartOfAccounts.Commands.Create;
using ucondo_challenge.application.ChartOfAccounts.Commands.Update;
using ucondo_challenge.business.Entities;

namespace ucondo_challenge.application.ChartOfAccounts.Commands.Dtos
{
    public class ChartOfAccountsProfile : Profile
    {
        public ChartOfAccountsProfile()
        {
            CreateMap<ChartOfAccountsCreateCommand, ChartOfAccountsEntity>();
            CreateMap<ChartOfAccountsEntity, ChartOfAccountsDto>();
            CreateMap<ChartOfAccountsUpdateCommand, ChartOfAccountsEntity>();
        }
    }
}
