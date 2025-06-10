using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ucondo_challenge.business.Entities;
using ucondo_challenge.business.Repositories;

namespace ucondo_challenge.application.ChartOfAccounts.Commands.Create
{
    public sealed class ChartOfAccountsCreateCommandHandler(
            ILogger<ChartOfAccountsCreateCommandHandler> logger,
            IChartOfAccountsRepository repository,
            IMapper mapper
            ) : IRequestHandler<ChartOfAccountsCreateCommand, bool>
    {
        public 
            async Task<bool> Handle(ChartOfAccountsCreateCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating Chart of Accounts with details: {@Request}", request.ToString());

            var entity = mapper.Map<ChartOfAccountsEntity>(request);

            return await repository.CreateAsync(entity, cancellationToken) != Guid.Empty
                ? true
                : false;
        }
    }
}
