using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ucondo_challenge.business.Entities;
using ucondo_challenge.business.Repositories;
using ucondo_challenge.infrastructure.Persistence;

namespace ucondo_challenge.infrastructure.Seeders
{
    internal class UCondoChallengeSeeder(
        UCondoChallengeDbContext dbContext,
        IChartOfAccountsRepository chartOfAccountsRepository
        ) : IUCondoChallengeSeeder
    {
        public async Task Seed()
        {
            if (dbContext.Database.GetPendingMigrations().Any())
            {
                await dbContext.Database.MigrateAsync();
            }

            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.ChartOfAccounts.Any())
                {
                    var chartOfAccounts = GetChartOfAccounts();
                    foreach (var coa in chartOfAccounts)
                    {
                        await chartOfAccountsRepository.CreateAsync(coa, default);
                    }
                }
            }
        }

        private IList<ChartOfAccountsEntity> GetChartOfAccounts()
        {
            var list = new List<ChartOfAccountsEntity>();
            var tenantId = Guid.NewGuid();
            
            var Receitas = new ChartOfAccountsEntity
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                Name = "Receitas",
                Code = "1",
                AllowEntries = false,
                ParentId = null,
                Type = business.Enum.AccountType.Receita
            };

            list.Add(Receitas);

            var Despesas = new ChartOfAccountsEntity
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                Name = "Despesas",
                Code = "2",
                AllowEntries = false,
                ParentId = null,
                Type = business.Enum.AccountType.Despesa
            };

            list.Add(Despesas);

            var DespesasComPessoal = new ChartOfAccountsEntity
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                Name = "Com Pessoal",
                Code = "2.1",
                AllowEntries = false,
                ParentId = Despesas.Id,
                Type = business.Enum.AccountType.Despesa
            };

            list.Add(DespesasComPessoal);

            var DespesasMensais = new ChartOfAccountsEntity
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                Name = "Mensais",
                Code = "2.2",
                AllowEntries = false,
                ParentId = Despesas.Id,
                Type = business.Enum.AccountType.Despesa
            };

            list.Add(DespesasMensais);

            var DespesasManutencao = new ChartOfAccountsEntity
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                Name = "Manutenção",
                Code = "2.3",
                AllowEntries = false,
                ParentId = Despesas.Id,
                Type = business.Enum.AccountType.Despesa
            };

            list.Add(DespesasManutencao);

            var DespesasDiversas = new ChartOfAccountsEntity
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                Name = "Diversas",
                Code = "2.4",
                AllowEntries = false,
                ParentId = Despesas.Id,
                Type = business.Enum.AccountType.Despesa
            };

            list.Add(DespesasDiversas);

            var DespesasBancarias = new ChartOfAccountsEntity
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                Name = "Despesas Bancarias",
                Code = "3",
                AllowEntries = false,
                ParentId = null,
                Type = business.Enum.AccountType.Despesa
            };

            list.Add(DespesasBancarias);

            var OutrasReceitas = new ChartOfAccountsEntity
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                Name = "Outras Receitas",
                Code = "4",
                AllowEntries = false,
                ParentId = null,
                Type = business.Enum.AccountType.Despesa
            };

            list.Add(OutrasReceitas);


            var items = new[]
 {
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Taxa condominial", Code = "1.1", AllowEntries = true, ParentId = Receitas.Id, Type = business.Enum.AccountType.Receita },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Reserva de dependência", Code = "1.2", AllowEntries = true, ParentId = Receitas.Id, Type = business.Enum.AccountType.Receita },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Multas", Code = "1.3", AllowEntries = true, ParentId = Receitas.Id, Type = business.Enum.AccountType.Receita },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Juros", Code = "1.4", AllowEntries = true, ParentId = Receitas.Id, Type = business.Enum.AccountType.Receita },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Multa condominial", Code = "1.5", AllowEntries = true, ParentId = Receitas.Id, Type = business.Enum.AccountType.Receita },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Água", Code = "1.6", AllowEntries = true, ParentId = Receitas.Id, Type = business.Enum.AccountType.Receita },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Gás", Code = "1.7", AllowEntries = true, ParentId = Receitas.Id, Type = business.Enum.AccountType.Receita },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Luz e energia", Code = "1.8", AllowEntries = true, ParentId = Receitas.Id, Type = business.Enum.AccountType.Receita },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Fundo de reserva", Code = "1.9", AllowEntries = true, ParentId = Receitas.Id, Type = business.Enum.AccountType.Receita },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Fundo de obras", Code = "1.10", AllowEntries = true, ParentId = Receitas.Id, Type = business.Enum.AccountType.Receita },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Correção monetária", Code = "1.11", AllowEntries = true, ParentId = Receitas.Id, Type = business.Enum.AccountType.Receita },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Transferência entre contas", Code = "1.12", AllowEntries = true, ParentId = Receitas.Id, Type = business.Enum.AccountType.Receita },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Pagamento duplicado", Code = "1.13", AllowEntries = true, ParentId = Receitas.Id, Type = business.Enum.AccountType.Receita },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Cobrança", Code = "1.14", AllowEntries = true, ParentId = Receitas.Id, Type = business.Enum.AccountType.Receita },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Crédito", Code = "1.15", AllowEntries = true, ParentId = Receitas.Id, Type = business.Enum.AccountType.Receita },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Água mineral", Code = "1.16", AllowEntries = true, ParentId = Receitas.Id, Type = business.Enum.AccountType.Receita },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Estorno taxa de resgate", Code = "1.17", AllowEntries = true, ParentId = Receitas.Id, Type = business.Enum.AccountType.Receita },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Acordo", Code = "1.18", AllowEntries = true, ParentId = Receitas.Id, Type = business.Enum.AccountType.Receita },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Honorários", Code = "1.19", AllowEntries = true, ParentId = Receitas.Id, Type = business.Enum.AccountType.Receita },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Salário", Code = "2.1.1", AllowEntries = true, ParentId = DespesasComPessoal.Id, Type = business.Enum.AccountType.Despesa },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Adiantamento salarial", Code = "2.1.2", AllowEntries = true, ParentId = DespesasComPessoal.Id, Type = business.Enum.AccountType.Despesa },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Hora extra", Code = "2.1.3", AllowEntries = true, ParentId = DespesasComPessoal.Id, Type = business.Enum.AccountType.Despesa },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Férias", Code = "2.1.4", AllowEntries = true, ParentId = DespesasComPessoal.Id, Type = business.Enum.AccountType.Despesa },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "13º salário", Code = "2.1.5", AllowEntries = true, ParentId = DespesasComPessoal.Id, Type = business.Enum.AccountType.Despesa },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Adiantamento 13º salário", Code = "2.1.6", AllowEntries = true, ParentId = DespesasComPessoal.Id, Type = business.Enum.AccountType.Despesa },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Adicional de função", Code = "2.1.7", AllowEntries = true, ParentId = DespesasComPessoal.Id, Type = business.Enum.AccountType.Despesa },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Aviso prévio", Code = "2.1.8", AllowEntries = true, ParentId = DespesasComPessoal.Id, Type = business.Enum.AccountType.Despesa },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "INSS", Code = "2.1.9", AllowEntries = true, ParentId = DespesasComPessoal.Id, Type = business.Enum.AccountType.Despesa },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "FGTS", Code = "2.1.10", AllowEntries = true, ParentId = DespesasComPessoal.Id, Type = business.Enum.AccountType.Despesa },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "PIS", Code = "2.1.11", AllowEntries = true, ParentId = DespesasComPessoal.Id, Type = business.Enum.AccountType.Despesa },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Vale refeição", Code = "2.1.12", AllowEntries = true, ParentId = DespesasComPessoal.Id, Type = business.Enum.AccountType.Despesa },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Vale transporte", Code = "2.1.13", AllowEntries = true, ParentId = DespesasComPessoal.Id, Type = business.Enum.AccountType.Despesa },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Cesta básica", Code = "2.1.14", AllowEntries = true, ParentId = DespesasComPessoal.Id, Type = business.Enum.AccountType.Despesa },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Acordo trabalhista", Code = "2.1.15", AllowEntries = true, ParentId = DespesasComPessoal.Id, Type = business.Enum.AccountType.Despesa },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Energia elétrica", Code = "2.2.1", AllowEntries = true, ParentId = DespesasMensais.Id, Type = business.Enum.AccountType.Despesa },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Água e esgoto", Code = "2.2.2", AllowEntries = true, ParentId = DespesasMensais.Id, Type = business.Enum.AccountType.Despesa },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Taxa de administração", Code = "2.2.3", AllowEntries = true, ParentId = DespesasMensais.Id, Type = business.Enum.AccountType.Despesa },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Gás", Code = "2.2.4", AllowEntries = true, ParentId = DespesasMensais.Id, Type = business.Enum.AccountType.Despesa },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Seguro obrigatório", Code = "2.2.5", AllowEntries = true, ParentId = DespesasMensais.Id, Type = business.Enum.AccountType.Despesa },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Telefone", Code = "2.2.6", AllowEntries = true, ParentId = DespesasMensais.Id, Type = business.Enum.AccountType.Despesa },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Softwares e aplicativos", Code = "2.2.7", AllowEntries = true, ParentId = DespesasMensais.Id, Type = business.Enum.AccountType.Despesa },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Elevador", Code = "2.3.1", AllowEntries = true, ParentId = DespesasManutencao.Id, Type = business.Enum.AccountType.Despesa },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Limpeza e conservação", Code = "2.3.2", AllowEntries = true, ParentId = DespesasManutencao.Id, Type = business.Enum.AccountType.Despesa },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Jardinagem", Code = "2.3.3", AllowEntries = true, ParentId = DespesasManutencao.Id, Type = business.Enum.AccountType.Despesa },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Honorários de advogado", Code = "2.4.1", AllowEntries = true, ParentId = DespesasDiversas.Id, Type = business.Enum.AccountType.Despesa },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Xerox", Code = "2.4.2", AllowEntries = true, ParentId = DespesasDiversas.Id, Type = business.Enum.AccountType.Despesa },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Correios", Code = "2.4.3", AllowEntries = true, ParentId = DespesasDiversas.Id, Type = business.Enum.AccountType.Despesa },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Despesas judiciais", Code = "2.4.4", AllowEntries = true, ParentId = DespesasDiversas.Id, Type = business.Enum.AccountType.Despesa },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Multas", Code = "2.4.5", AllowEntries = true, ParentId = DespesasDiversas.Id, Type = business.Enum.AccountType.Despesa },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Juros", Code = "2.4.6", AllowEntries = true, ParentId = DespesasDiversas.Id, Type = business.Enum.AccountType.Despesa },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Transferência entre contas", Code = "2.4.7", AllowEntries = true, ParentId = DespesasDiversas.Id, Type = business.Enum.AccountType.Despesa },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Registro de boletos", Code = "3.1", AllowEntries = true, ParentId = DespesasBancarias.Id, Type = business.Enum.AccountType.Despesa },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Processamento de boletos", Code = "3.2", AllowEntries = true, ParentId = DespesasBancarias.Id, Type = business.Enum.AccountType.Despesa },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Registro e processamento de boletos", Code = "3.3", AllowEntries = true, ParentId = DespesasBancarias.Id, Type = business.Enum.AccountType.Despesa },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Resgates", Code = "3.4", AllowEntries = true, ParentId = DespesasBancarias.Id, Type = business.Enum.AccountType.Despesa },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Rendimento de poupança", Code = "4.1", AllowEntries = true, ParentId = OutrasReceitas.Id, Type = business.Enum.AccountType.Receita },
    new { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Rendimento de investimentos", Code = "4.2", AllowEntries = true, ParentId = OutrasReceitas.Id, Type = business.Enum.AccountType.Receita }

};


            foreach (var item in items)
            {
                list.Add(
                    new ChartOfAccountsEntity
                    {
                        Id = item.Id,
                        TenantId = tenantId,
                        Name = item.Name,
                        Code = item.Code,
                        AllowEntries = true,
                        ParentId = item.ParentId
                    });
            }

            return list;

        }
    }
}
