using MediatR;
using Microsoft.Extensions.Logging;
using ucondo_challenge.application.ChartOfAccounts.Commands.Dtos;
using ucondo_challenge.application.ChartOfAccounts.Mapping;
using ucondo_challenge.business.Repositories;

namespace ucondo_challenge.application.ChartOfAccounts.Queries.GetByParent;

public class ChartOfAccountsGetByParentQueryHandler(
    ILogger<ChartOfAccountsGetByParentQuery> logger,
    IChartOfAccountsRepository repository
    ): IRequestHandler<ChartOfAccountsGetByParentQuery,IEnumerable<ChartOfAccountsDto>>
{
    public async Task<IEnumerable<ChartOfAccountsDto>> Handle(ChartOfAccountsGetByParentQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"TenantId:{request.TenantId}. Getting all Charts of Accounts by parent with details: {request.ToString()}");
        var entities = await repository.GetAllByParentId(request.TenantId, request.ParentId, cancellationToken);
        var dtos = entities.Select(x => x.MapEntityToDto());
        return dtos;
    }
}