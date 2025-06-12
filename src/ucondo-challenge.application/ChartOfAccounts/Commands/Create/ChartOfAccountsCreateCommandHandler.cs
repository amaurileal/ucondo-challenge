using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text;
using ucondo_challenge.business.Entities;
using ucondo_challenge.business.Exceptions;
using ucondo_challenge.business.Repositories;

namespace ucondo_challenge.application.ChartOfAccounts.Commands.Create
{
    public sealed class ChartOfAccountsCreateCommandHandler(
            ILogger<ChartOfAccountsCreateCommandHandler> logger,
            IChartOfAccountsRepository repository,
            IMapper mapper
            ) : IRequestHandler<ChartOfAccountsCreateCommand, Guid>
    {
        public async Task<Guid> Handle(ChartOfAccountsCreateCommand request, CancellationToken cancellationToken)
        {                   
            logger.LogInformation($"TenantId:{request.TenantId}. Creating Chart of Accounts with details: {request.ToString()}");
            
            await Validations(request, cancellationToken);

            var entity = mapper.Map<ChartOfAccountsEntity>(request);

            var result = await repository.CreateAsync(entity, cancellationToken);

            return result;
        }

        private async Task Validations(ChartOfAccountsCreateCommand request, CancellationToken cancellationToken)
        {
            var requiredFieldValidator = new ChartOfAccountsCreateCommandValidator();
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

            var register = await repository.GetByCodeAsync(request.TenantId, request.Code!, cancellationToken);
            if (register != null)
                throw new BadRequestException($"Code {request.Code} already provided");


            // Parent Validations if provided
            if (request.ParentId.HasValue)
            {
                //Check if the parent exists
                var parentEntity = await repository.GetByIdAsync(request.TenantId, request.ParentId.Value, cancellationToken);
                if (parentEntity == null)                
                    throw new BadRequestException($"Parent not found: {request.ParentId.Value}");
                
                //check if allowed is set to false.
                if (parentEntity!.AllowEntries)
                    throw new BadRequestException($"Parent Chart of Accounts with ID {request.ParentId.Value} does not allow children.");

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
