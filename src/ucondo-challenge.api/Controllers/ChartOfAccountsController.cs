using MediatR;
using Microsoft.AspNetCore.Mvc;
using ucondo_challenge.application.ChartOfAccounts.Commands.Create;

namespace ucondo_challenge.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartOfAccountsController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(ChartOfAccountsCreateCommand command)
        {
            var result = await mediator.Send(command);
            if (result)
            {
                return Ok("Chart of Accounts created successfully.");
            }
            else
            {
                return BadRequest("Failed to create Chart of Accounts.");
            }

        }
    }
}
