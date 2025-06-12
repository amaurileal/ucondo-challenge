using MediatR;
using Microsoft.Extensions.Logging;
using ucondo_challenge.application.ChartOfAccounts.Mapping;
using ucondo_challenge.business.Exceptions;
using ucondo_challenge.business.Repositories;

namespace ucondo_challenge.application.ChartOfAccounts.Commands.Create
{
    public sealed class ChartOfAccountsCreateCommandHandler(
            ILogger<ChartOfAccountsCreateCommandHandler> logger,
            IChartOfAccountsRepository repository
            ) : IRequestHandler<ChartOfAccountsCreateCommand, Guid>
    {
        public async Task<Guid> Handle(ChartOfAccountsCreateCommand request, CancellationToken cancellationToken)
        {                   
            logger.LogInformation($"TenantId:{request.TenantId}. Creating Chart of Accounts with details: {request.ToString()}");
            
            await Validations(request, cancellationToken);

            var entity = request.MapCommandCreateToEntity();

            var result = await repository.CreateAsync(entity, cancellationToken);

            return result;
        }

        private async Task Validations(ChartOfAccountsCreateCommand request, CancellationToken cancellationToken)
        {
            await ValidateRequiredFieldsAsync(request, cancellationToken);
            await ValidateUniqueCodeAsync(request, cancellationToken);
            if (request.ParentId.HasValue)
            {
                await ValidateParentAsync(request, cancellationToken);
            }
        }

        private async Task ValidateRequiredFieldsAsync(ChartOfAccountsCreateCommand request, CancellationToken cancellationToken)
        {
            var validator = new ChartOfAccountsCreateCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var message = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new BadRequestException(message);
            }
        }

        private async Task ValidateUniqueCodeAsync(ChartOfAccountsCreateCommand request, CancellationToken cancellationToken)
        {
            var register = await repository.GetByCodeAsync(request.TenantId, request.Code!, cancellationToken);
            if (register != null)
            {
                throw new BadRequestException($"Code {request.Code} already provided for tenant {request.TenantId}");
            }
        }

        private async Task ValidateParentAsync(ChartOfAccountsCreateCommand request, CancellationToken cancellationToken)
        {
            var parentEntity = await repository.GetByIdAsync(request.TenantId, request.ParentId!.Value, cancellationToken);
            if (parentEntity == null)
            {
                throw new BadRequestException($"Parent not found: {request.ParentId.Value}");
            }

            if (parentEntity.AllowEntries)
            {
                throw new BadRequestException($"Parent Chart of Accounts with ID {request.ParentId.Value} does not allow children.");
            }

            if (parentEntity.Type != request.Type)
            {
                throw new BadRequestException($"Parent Chart of Accounts with ID {request.ParentId.Value} is of type {parentEntity.Type}, but the new account is of type {request.Type}.");
            }

            if (!request.Code!.StartsWith(parentEntity.Code))
            {
                throw new BadRequestException($"Code {request.Code} is not a child of parent code {parentEntity.Code}.");
            }
        }
    }
}
