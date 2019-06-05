using System;
using System.Threading.Tasks;
using CustomerServiceApp.Domain.Commands;
using Framework.Shared.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace CustomerServiceApp.Host.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICommandBus _commandBus;

        public CustomersController(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateCustomerCommand command)
        {
            await _commandBus.SendAsync(command).ConfigureAwait(false);

            return Accepted(command.CommandId);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            return Ok();
        }
    }
}
