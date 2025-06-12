using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ucondo_challenge.application.ChartOfAccounts.Commands.Dtos;
using ucondo_challenge.business.Repositories;

namespace ucondo_challenge.application.ChartOfAccounts.Queries.GetAllParents;

public class ChartOfAccountsGetAllParentsQueryHandler(
    ILogger<ChartOfAccountsGetAllParentsQueryHandler> logger,
    IChartOfAccountsRepository repository,
    IMapper mapper
    ): IRequestHandler<ChartOfAccountsGetAllParentsQuery,IEnumerable<ChartOfAccountsDto>>
{
    public async Task<IEnumerable<ChartOfAccountsDto>> Handle(ChartOfAccountsGetAllParentsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"TenantId:{request.TenantId}. Getting all parent Chart of Accounts");
        var entities = await repository.GetAllParentsAsync(request.TenantId, cancellationToken);
        var dtos = mapper.Map<IEnumerable<ChartOfAccountsDto>>(entities);
        return dtos;
    }
}