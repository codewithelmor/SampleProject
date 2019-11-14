using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SampleProject.API.Attributes;
using SampleProject.BusinessLogicLayer.Mediators.PersonMediator.Commands;
using SampleProject.BusinessLogicLayer.Mediators.PersonMediator.Queries;
using SampleProject.DataTransferObject.BindingModels;
using Serilog;

namespace SampleProject.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class PersonsController : BaseController
    {
        private readonly IMediator _mediator;

        public PersonsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// api/v1.0/persons
        /// GET
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public async Task<IActionResult> List()
        {
            var query = new List.Query();
            query.SetUserName(User.Identity.Name);
            var result = await _mediator.Send(query);

            // Will create a separate log file for the specific date and category, if you want to categorized the logs
            //Log.Information($"List API endpoint was called from {this.GetType().Name} by {User.Identity.Name}" + ",{Name}", this.GetType().Name);
            Log.Information($"List API endpoint was called from {this.GetType().Name} by {User.Identity.Name}" + " | {Name}", this.GetType().Name);

            return Ok(result);
        }

        /// <summary>
        /// api/v1.0/persons
        /// POST
        /// { "firstName": "Elmor", "lastName": "Cabalfin", "gender": "0" }
        /// </summary>
        /// <param name="bindingModel"></param>
        /// <returns></returns>
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

            // Will create a separate log file for the specific date and category, if you want to categorized the logs
            //Log.Information($"Add API endpoint was called from {this.GetType().Name} by {User.Identity.Name}" + ",{Name}", this.GetType().Name);
            Log.Information($"Add API endpoint was called from {this.GetType().Name} by {User.Identity.Name}" + " | {Name}", this.GetType().Name);

            return Ok(result);
        }

        /// <summary>
        /// api/v1.0/persons/{personId}
        /// PUT
        /// { "firstName": "Elmor", "lastName": "Cabalfin", "gender": "0" }
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="bindingModel"></param>
        /// <returns></returns>
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

            // Will create a separate log file for the specific date and category, if you want to categorized the logs
            //Log.Information($"Update API endpoint was called from {this.GetType().Name} by {User.Identity.Name}" + ",{Name}", this.GetType().Name);
            Log.Information($"Update API endpoint was called from {this.GetType().Name} by {User.Identity.Name}" + " | {Name}", this.GetType().Name);

            return Ok(result);
        }

        /// <summary>
        /// api/v1.0/persons/{personId}
        /// PATCH
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpPatch("{personId}")]
        public async Task<IActionResult> Toggle([FromRoute] string personId)
        {
            var command = new Toggle.Command(personId);
            command.SetUserName(User.Identity.Name);
            var result = await _mediator.Send(command);

            // Will create a separate log file for the specific date and category, if you want to categorized the logs
            //Log.Information($"Toggle API endpoint was called from {this.GetType().Name} by {User.Identity.Name}" + ",{Name}", this.GetType().Name);
            Log.Information($"Toggle API endpoint was called from {this.GetType().Name} by {User.Identity.Name}" + " | {Name}", this.GetType().Name);

            return Ok(result);
        }

        /// <summary>
        /// api/v1.0/persons/{personId}
        /// DELETE
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpDelete("{personId}")]
        public async Task<IActionResult> Delete([FromRoute] string personId)
        {
            var command = new Delete.Command(personId);
            command.SetUserName(User.Identity.Name);
            var result = await _mediator.Send(command);

            // Will create a separate log file for the specific date and category, if you want to categorized the logs
            //Log.Information($"Delete API endpoint was called from {this.GetType().Name} by {User.Identity.Name}" + ",{Name}", this.GetType().Name);
            Log.Information($"Delete API endpoint was called from {this.GetType().Name} by {User.Identity.Name}" + " | {Name}", this.GetType().Name);

            return Ok(result);
        }
    }
}