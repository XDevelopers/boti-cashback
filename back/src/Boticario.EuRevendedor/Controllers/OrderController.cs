using Boticario.EuRevendedor.Models;
using Boticario.EuRevendedor.Service.Handlers.OrderHandlers.Commands;
using Boticario.EuRevendedor.Service.Handlers.OrderHandlers.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Boticario.EuRevendedor.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ApiBaseController
    {
        public OrderController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Order>>> GetAsync([FromQuery] GetOrdersQuery query)
        {
            return await OkResponse(query);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Order>> PostAsync([FromBody] InsertOrderCommand command)
        {
            return await CreatedResponse(command);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Order>> PutAsync(string id, [FromBody] UpdateOrderCommand command)
        {
            return await OkResponse(command);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            var command = new DeleteOrderCommand(id);
            return await NoContentResponse(command);
        }
    }
}
