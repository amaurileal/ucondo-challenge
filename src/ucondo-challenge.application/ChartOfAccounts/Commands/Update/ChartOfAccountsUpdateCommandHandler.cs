using MediatR;
using Microsoft.Extensions.Logging;
using ucondo_challenge.application.ChartOfAccounts.Mapping;
using ucondo_challenge.business.Entities;
using ucondo_challenge.business.Exceptions;
using ucondo_challenge.business.Repositories;

namespace ucondo_challenge.application.ChartOfAccounts.Commands.Update
{
    public sealed class ChartOfAccountsUpdateCommandHandler(
            ILogger<ChartOfAccountsUpdateCommandHandler> logger,
            IChartOfAccountsRepository repository
            ) : IRequestHandler<ChartOfAccountsUpdateCommand>
    {
        public async Task Handle(ChartOfAccountsUpdateCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"TenantId:{request.TenantId} Creating Chart of Accounts with details: {request.ToString()}");

            var entity = await repository.GetByIdAsync(request.TenantId, request.Id, cancellationToken);

            if (entity == null)
                throw new NotFoundException(nameof(ChartOfAccountsEntity), request.Id.ToString());

            await Validations(request, entity, cancellationToken);

            await repository.UpdateAsync(entity.MapCommandUpdateToEntity(request), cancellationToken);
        }

        private async Task Validations(ChartOfAccountsUpdateCommand request, ChartOfAccountsEntity entity, CancellationToken cancellationToken)
        {
            await ValidateRequiredFieldsAsync(request, cancellationToken);
            await ValidateUniqueCodeAsync(request, entity, cancellationToken);
                        
            if (request.ParentId.HasValue)
            {
                await ValidateParentAsync(request, cancellationToken);
            }
        }

        private async Task ValidateRequiredFieldsAsync(ChartOfAccountsUpdateCommand request, CancellationToken cancellationToken)
        {
            var validator = new ChartOfAccountsUpdateCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var message = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new BadRequestException(message);
            }
        }

        private async Task ValidateUniqueCodeAsync(ChartOfAccountsUpdateCommand request, ChartOfAccountsEntity entity, CancellationToken cancellationToken)
        {
            if (request.Code != entity.Code)
            {
                var register = await repository.GetByCodeAsync(request.TenantId, request.Code, cancellationToken);
                if (register != null)
                {
                    throw new BadRequestException($"Code {request.Code} already provided for tenant {request.TenantId}");
                }
            }
        }

        private async Task ValidateParentAsync(ChartOfAccountsUpdateCommand request, CancellationToken cancellationToken)
        {
            var parentEntity = await repository.GetByIdAsync(request.TenantId, request.ParentId!.Value, cancellationToken);
            if (parentEntity == null)
            {
                throw new BadRequestException($"Parent not found: {request.ParentId.Value}");
            }

            if (!parentEntity.AllowEntries)
            {
                throw new BadRequestException($"Parent Chart of Accounts with ID {request.ParentId.Value} does not allow entries.");
            }

            if (parentEntity.Type != request.Type)
            {
                throw new BadRequestException($"Parent Chart of Accounts with ID {request.ParentId.Value} is of type {parentEntity.Type}, but the new account is of type {request.Type}.");
            }

            if (!request.Code.StartsWith(parentEntity.Code))
            {
                throw new BadRequestException($"Code {request.Code} is not a child of parent code {parentEntity.Code}.");
            }
        }
    }
}
