using MediatR;
using Microsoft.Extensions.Logging;

namespace ucondo_challenge.application.ChartOfAccounts.Commands.Create
{
    public sealed class ChartOfAccountsCreateCommandHandler(
            ILogger<ChartOfAccountsCreateCommandHandler> logger
            ) : IRequestHandler<ChartOfAccountsCreateCommand, bool>
    {
        public 
            Task<bool> Handle(ChartOfAccountsCreateCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating Chart of Accounts with details: {@Request}", request.ToString());

            //Task.Delay(1000, cancellationToken).Wait(cancellationToken); // Simulate a delay for the operation

            return Task.FromResult(true);
        }
    }
}
