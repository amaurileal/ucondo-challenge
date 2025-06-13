using MediatR;
using Microsoft.AspNetCore.Mvc;
using ucondo_challenge.api.ViewModels.ChartOfAccounts.Create;
using ucondo_challenge.api.ViewModels.ChartOfAccounts.Create.Mapping;
using ucondo_challenge.api.ViewModels.ChartOfAccounts.Update;
using ucondo_challenge.api.ViewModels.ChartOfAccounts.Update.Mapping;
using ucondo_challenge.application.ChartOfAccounts.Commands.Create;
using ucondo_challenge.application.ChartOfAccounts.Commands.Delete;
using ucondo_challenge.application.ChartOfAccounts.Commands.Dtos;
using ucondo_challenge.application.ChartOfAccounts.Commands.Update;
using ucondo_challenge.application.ChartOfAccounts.Queries.GetAll;
using ucondo_challenge.application.ChartOfAccounts.Queries.GetAllParents;
using ucondo_challenge.application.ChartOfAccounts.Queries.GetByParent;

namespace ucondo_challenge.api.Controllers
{
    [Route("api/chart-of-accounts")]
    [ApiController]
    public class ChartOfAccountsController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromHeader]Guid tenantId, [FromBody]ChartOfAccountsCreateViewModel viewModel)
        {   
            var result = await mediator.Send(viewModel.ToCreateCommand(tenantId));
            return Ok(result);

        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromHeader]Guid tenantId, Guid id, [FromBody] ChartOfAccountsUpdateViewModel viewModel)
        {
            await mediator.Send(viewModel.ToUpdateCommand(tenantId,id));
            return NoContent();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<ChartOfAccountsDto?>> GetById([FromHeader]Guid tenantId, Guid id)
        {
            var dto = await mediator.Send(new ChartOfAccountsGetByIdQuery(tenantId, id));
            return Ok(dto);
        }

        [HttpGet]
        public async Task<ActionResult<ChartOfAccountsDto?>> GetAll([FromHeader] Guid tenantId)
        {
            var dto = await mediator.Send(new ChartOfAccountsGetAllQuery(tenantId));
            return Ok(dto);
        }

        [HttpGet("all-by-parent/{parentId}")]
        public async Task<ActionResult<IEnumerable<ChartOfAccountsDto>>> GetByParent([FromHeader] Guid tenantId, Guid parentId)
        {
            var dtos = await mediator.Send(new ChartOfAccountsGetByParentQuery(tenantId, parentId));

            return Ok(dtos);
        }

        [HttpGet("parents")]
        public async Task<ActionResult<IEnumerable<ChartOfAccountsDto>>> GetParents([FromHeader] Guid tenantId)
        {

            var dtos = await mediator.Send(new ChartOfAccountsGetAllParentsQuery(tenantId));

            return Ok(dtos);
        }

        [HttpGet("next-code-by-parent/{parentId}")]
        public async Task<ActionResult<string>> GetNextCodeByParent([FromHeader] Guid tenantId, Guid parentId)
        {
            var nextCode = await mediator.Send(new ChartOfAccountsGetNextCodeByParentQuery(tenantId, parentId));
            return Ok(nextCode);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<string>> Delete([FromHeader] Guid tenantId, Guid Id)
        {
            await mediator.Send(new ChartOfAccountsDeleteCommand(tenantId, Id));
            return NoContent();
        }
    }
}
