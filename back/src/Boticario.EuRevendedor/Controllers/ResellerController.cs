using Boticario.EuRevendedor.Models.ViewModels;
using Boticario.EuRevendedor.Service.Handlers.ResellerHandlers.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Boticario.EuRevendedor.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    //[Authorize] // TODO: Passo 2 - Comentar [Authorize] para permitir a Criação do usuário Administrador
    public class ResellerController : ApiBaseController
    {
        public ResellerController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserViewModel>> PostAsync([FromBody] InsertResellerCommand command)
        {
            return await CreatedResponse(command);
        }
    }
}
