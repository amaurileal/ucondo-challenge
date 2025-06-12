using MediatR;
using Microsoft.Extensions.Logging;
using ucondo_challenge.business.Entities;
using ucondo_challenge.business.Exceptions;
using ucondo_challenge.business.Repositories;

namespace ucondo_challenge.application.ChartOfAccounts.Commands.Delete
{
    public sealed class ChartOfAccountsDeleteCommandHandler(
            ILogger<ChartOfAccountsDeleteCommandHandler> logger,
            IChartOfAccountsRepository repository
            ) : IRequestHandler<ChartOfAccountsDeleteCommand>
    {
        public async Task Handle(ChartOfAccountsDeleteCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"TenantId:{request.TenantId}. Deleting Chart of Accounts with Id: {{Id}}", request.Id);

            var entity = await repository.GetByIdAsync(request.TenantId, request.Id, cancellationToken);

            if (entity == null)
                    throw new NotFoundException(nameof(ChartOfAccountsEntity), request.Id.ToString());

            await repository.DeleteAsync(entity, cancellationToken);
        }
    }
}
