using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ucondo_challenge.business.Entities;
using ucondo_challenge.business.Exceptions;
using ucondo_challenge.business.Repositories;

namespace ucondo_challenge.application.ChartOfAccounts.Queries.GetByParent;

public class ChartOfAccountsGetNextCodeByParentQueryHandler(
    ILogger<ChartOfAccountsGetByParentQuery> logger,
    IChartOfAccountsRepository repository,
    IMapper mapper
    ): IRequestHandler<ChartOfAccountsGetNextCodeByParentQuery, string>
{
    public  async Task<string> Handle(ChartOfAccountsGetNextCodeByParentQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"TenantId:{request.TenantId}. Getting next code for Chart of Accounts by parent with details: {request.ToString()}");

        //check if parent exists
        var parentEntity = await repository.GetByIdAsync(request.TenantId, request.ParentId, cancellationToken);
        if (parentEntity == null)
            throw new NotFoundException(nameof(ChartOfAccountsEntity), request.ParentId.ToString());

        var registers = await  repository.GetAllByParentId(request.TenantId, request.ParentId, cancellationToken);

        string nextCode = await GetNextCodeByParentRecursive(request.TenantId, parentEntity.Code, registers.ToList(),cancellationToken);

        return nextCode;

    }

    private async Task<string> GetNextCodeByParentRecursive(Guid tenantId, string parentCode, List<ChartOfAccountsEntity> registers, CancellationToken cancellationToken)
    {
        var maiorNumero = registers
            .Select(x => x.Code.Split('.').Last())
            .Select(numStr => int.Parse(numStr))
            .Max();

        if (maiorNumero < 999)
        {
            return parentCode + $".{(maiorNumero + 1)}";
        }
        else
        {
            int lastDotIndex = parentCode.LastIndexOf('.');
            string newCode = (lastDotIndex!=-1)? parentCode.Substring(0, lastDotIndex) : throw new Exception("Codigo indisponivel para o seguinte pai");

            var newParentId = registers.FirstOrDefault(x => x.Code == newCode);

            if (newParentId == null)
                throw new NotFoundException(nameof(ChartOfAccountsEntity), newCode);

            var newRegisters = await repository.GetAllByParentId(tenantId, newParentId.Id,cancellationToken);
            return await GetNextCodeByParentRecursive(tenantId, newCode, newRegisters.ToList(), cancellationToken);
        }

        return null;
    }
}