﻿using MediatR;
using System.Text.Json;
using System.Text.Json.Serialization;
using ucondo_challenge.business.Enum;

namespace ucondo_challenge.application.ChartOfAccounts.Commands.Create
{
    public sealed class ChartOfAccountsCreateCommand : IRequest<Guid>
    {        
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
