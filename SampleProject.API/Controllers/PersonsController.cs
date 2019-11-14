using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SampleProject.API.Attributes;
using SampleProject.BusinessLogicLayer.Mediators.PersonMediator.Commands;
using SampleProject.BusinessLogicLayer.Mediators.PersonMediator.Queries;
using SampleProject.DataTransferObject.BindingModels;

namespace SampleProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("")]
        public async Task<IActionResult> List()
        {
            var query = new List.Query();
            query.SetUserName(User.Identity.Name);
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPost("")]
        [ValidateModel]
        public async Task<IActionResult> Add([FromBody] PersonBindingModel bindingModel)
        {
            var command = new Add.Command
            {
                FirstName = bindingModel.FirstName,
                LastName = bindingModel.LastName,
                Gender = bindingModel.Gender
            };
            command.SetUserName(User.Identity.Name);

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPut("{personId}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] string personId, [FromBody] PersonBindingModel bindingModel)
        {
            var command = new Update.Command(personId)
            {
                FirstName = bindingModel.FirstName,
                LastName = bindingModel.LastName,
                Gender = bindingModel.Gender
            };
            command.SetUserName(User.Identity.Name);

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPatch("{personId}")]
        public async Task<IActionResult> Toggle([FromRoute] string personId)
        {
            var command = new Toggle.Command(personId);
            command.SetUserName(User.Identity.Name);

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpDelete("{personId}")]
        public async Task<IActionResult> Delete([FromRoute] string personId)
        {
            var command = new Delete.Command(personId);
            command.SetUserName(User.Identity.Name);

            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}