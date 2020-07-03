using Boticario.EuRevendedor.Core.Extensions;
using Boticario.EuRevendedor.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Boticario.EuRevendedor.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class CashbackController : ControllerBase
    {
        private readonly IBoticarioApiService boticarioApiService;

        public CashbackController(IBoticarioApiService boticarioApiService)
        {
            this.boticarioApiService = boticarioApiService;
        }

        [HttpGet("{cpf}/acumulado")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<decimal>> GetAsync(string cpf)
        {
            cpf = cpf?.Trim().NumberOnly();
            var credit = await boticarioApiService.Cashback(cpf);
            if (credit != null)
            {
                return Ok(credit);
            }
            return NoContent();
        }
    }
}
