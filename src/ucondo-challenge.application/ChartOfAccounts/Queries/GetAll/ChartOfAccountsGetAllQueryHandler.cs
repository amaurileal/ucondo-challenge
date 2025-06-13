using MediatR;
using Microsoft.Extensions.Logging;
using ucondo_challenge.application.ChartOfAccounts.Commands.Dtos;
using ucondo_challenge.application.ChartOfAccounts.Mapping;
using ucondo_challenge.business.Repositories;

namespace ucondo_challenge.application.ChartOfAccounts.Queries.GetAll;

public class ChartOfAccountsGetAllQueryHandler(
    ILogger<ChartOfAccountsGetAllQueryHandler> logger,
    IChartOfAccountsRepository repository
    ): IRequestHandler<ChartOfAccountsGetAllQuery,IEnumerable<ChartOfAccountsDto>>
{
    public async Task<IEnumerable<ChartOfAccountsDto>> Handle(ChartOfAccountsGetAllQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"TenantId:{request.TenantId}. Getting all Chart of Accounts");
        var entities = await repository.GetAllAsync(request.TenantId, cancellationToken);
        var dtos = entities.Select(x => x.MapEntityToDto());
        return dtos;
    }
}