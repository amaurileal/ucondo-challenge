using MediatR;
using Microsoft.Extensions.Logging;
using ucondo_challenge.business.Entities;
using ucondo_challenge.business.Exceptions;
using ucondo_challenge.business.Repositories;

namespace ucondo_challenge.application.ChartOfAccounts.Queries.GetByParent;

public class ChartOfAccountsGetNextCodeByParentQueryHandler(
    ILogger<ChartOfAccountsGetByParentQuery> logger,
    IChartOfAccountsRepository repository
    ): IRequestHandler<ChartOfAccountsGetNextCodeByParentQuery, string>
{
    public  async Task<string> Handle(ChartOfAccountsGetNextCodeByParentQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"TenantId:{request.TenantId}. Getting next code for Chart of Accounts by parent with details: {request.ToString()}");

        //check if parent exists
        var parentEntity = await repository.GetByIdAsync(request.TenantId, request.ParentId, cancellationToken);

        if (parentEntity.AllowEntries)
            throw new BadRequestException($"TenantId:{request.TenantId}. Chart of Accounts with AllowEntries set to true cannot have children. {parentEntity.ToString()}");

        if (parentEntity == null)
            throw new NotFoundException(nameof(ChartOfAccountsEntity), request.ParentId.ToString());

        var registers = await  repository.GetAllByParentId(request.TenantId, request.ParentId, cancellationToken);

        string nextCode = await GetNextCodeByParentRecursive(request.TenantId, parentEntity.Code, registers.ToList(),cancellationToken);

        return nextCode;

    }

    private async Task<string> GetNextCodeByParentRecursive(Guid tenantId, string parentCode, List<ChartOfAccountsEntity> registers, CancellationToken cancellationToken, string result = null)
    {        
        var maiorNumero = registers
            .Select(x => x.Code.Split('.').Last())
            .Select(numStr => int.Parse(numStr))
            .Max();

            if (maiorNumero < 999)
        {
            result += $"Codigo:{parentCode}.{(maiorNumero + 1)}";
        }
        else
        {
            int lastDotIndex = parentCode.LastIndexOf('.');
            string newCode = (lastDotIndex!=-1)? parentCode.Substring(0, lastDotIndex) : (int.Parse(parentCode) - 1).ToString();

            result += $"Atingiu Max: {parentCode}.{maiorNumero}. Novo Pai deve ser: {newCode}. ";

            var newParentId = await repository.GetByCodeAsync(tenantId, newCode, cancellationToken);

            if (newParentId == null)
                throw new BadRequestException($"TenantId:{tenantId}. {result}. Esse codigo pai não existe!");

            var newRegisters = await repository.GetAllByParentId(tenantId, newParentId.Id,cancellationToken);
            return await GetNextCodeByParentRecursive(tenantId, newCode, newRegisters.ToList(), cancellationToken, result);
        }

        return result;
    }
}