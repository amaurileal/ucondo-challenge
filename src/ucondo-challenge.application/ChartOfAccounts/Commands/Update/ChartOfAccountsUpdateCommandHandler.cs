using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text;
using ucondo_challenge.application.ChartOfAccounts.Commands.Update;
using ucondo_challenge.business.Entities;
using ucondo_challenge.business.Exceptions;
using ucondo_challenge.business.Repositories;

namespace ucondo_challenge.application.ChartOfAccounts.Commands.Update
{
    public sealed class ChartOfAccountsUpdateCommandHandler(
            ILogger<ChartOfAccountsUpdateCommandHandler> logger,
            IChartOfAccountsRepository repository,
            IMapper mapper
            ) : IRequestHandler<ChartOfAccountsUpdateCommand>
    {
        public async Task Handle(ChartOfAccountsUpdateCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"TenantId:{request.TenantId} Creating Chart of Accounts with details: {request.ToString()}");

            var entity = await repository.GetByIdAsync(request.TenantId, request.Id, cancellationToken);

            if (entity == null)
                throw new NotFoundException(nameof(ChartOfAccountsEntity), request.Id.ToString());

            await Validations(request, entity, cancellationToken);

            mapper.Map(request, entity);

            await repository.UpdateAsync(entity, cancellationToken);
        }

        private async Task Validations(ChartOfAccountsUpdateCommand request, ChartOfAccountsEntity entity, CancellationToken cancellationToken)
        {            
            var requiredFieldValidator = new ChartOfAccountsUpdateCommandValidator();
            var validate = await requiredFieldValidator.ValidateAsync(request, cancellationToken);
            var message = new StringBuilder();
            if (validate.Errors.Any())
            {
                foreach (var erro in validate.Errors)
                {
                    message.AppendLine(erro.ErrorMessage);
                }
                throw new BadRequestException(message.ToString());
            }

            if (request.Code != entity.Code)
            {
                var register = await repository.GetByCodeAsync(request.TenantId, request.Code, cancellationToken);
                if (register != null)
                    throw new BadRequestException($"Code {request.Code} already provided");
            }

            if(request.AllowEntries != entity.AllowEntries)

            // Parent Validations if provided
            if (request.ParentId.HasValue)
            {
                //Check if the parent exists
                var parentEntity = await repository.GetByIdAsync(request.TenantId, request.ParentId.Value, cancellationToken);
                if (parentEntity == null)
                    throw new BadRequestException($"Parent not found: {request.ParentId.Value}");

                //check if allowed is set to false.
                if (!parentEntity!.AllowEntries)
                    throw new BadRequestException($"Parent Chart of Accounts with ID {request.ParentId.Value} does not allow entries.");

                //check if the parent type is compatible with the new account type
                if (parentEntity.Type != request.Type)
                    throw new BadRequestException($"Parent Chart of Accounts with ID {request.ParentId.Value} is of type {parentEntity.Type}, but the new account is of type {request.Type}.");

                //check if code is child of parent code
                if (!request.Code.StartsWith(parentEntity.Code))
                    throw new BadRequestException($"Code {request.Code} is not a child of parent code {parentEntity.Code}.");


            }

        }
    }
}
