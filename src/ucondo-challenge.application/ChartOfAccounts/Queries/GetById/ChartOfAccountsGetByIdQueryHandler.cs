using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ucondo_challenge.application.ChartOfAccounts.Commands.Dtos;
using ucondo_challenge.application.ChartOfAccounts.Queries.GetByParent;
using ucondo_challenge.business.Entities;
using ucondo_challenge.business.Exceptions;
using ucondo_challenge.business.Repositories;

namespace ucondo_challenge.application.ChartOfAccounts.Queries.GetById;

public class ChartOfAccountsGetByIdQueryHandler(
    ILogger<ChartOfAccountsGetByIdQuery> logger,
    IChartOfAccountsRepository repository,
    IMapper mapper
    ): IRequestHandler<ChartOfAccountsGetByIdQuery,ChartOfAccountsDto>
{
    public async Task<ChartOfAccountsDto> Handle(ChartOfAccountsGetByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"TenantId:{request.TenantId}. Getting Chart of Accounts by Id: {request.Id}");
        var entity = await repository.GetByIdAsync(request.TenantId, request.Id, cancellationToken);
        if (entity == null)
            throw new NotFoundException(nameof(ChartOfAccountsEntity),$"Chart of Accounts with ID {request.Id} not found.");
        return  mapper.Map<ChartOfAccountsDto>(entity);
    }
}