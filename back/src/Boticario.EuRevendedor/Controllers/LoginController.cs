using Boticario.EuRevendedor.Models.ViewModels;
using Boticario.EuRevendedor.Service.Handlers.ResellerHandlers.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Boticario.EuRevendedor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ApiBaseController
    {
        public LoginController(IMediator mediator) : base(mediator) { }

        [HttpPost, AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserViewModel>> PostAsync([FromBody] LoginCommand command)
        {
            return await OkResponse(command);
        }
    }
}
