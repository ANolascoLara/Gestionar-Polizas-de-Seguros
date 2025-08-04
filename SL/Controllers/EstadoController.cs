using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class EstadoController : ControllerBase
    {
        private readonly BL.Estado _estado;

        public EstadoController(BL.Estado estado)
        {
            _estado = estado;
        }

        [HttpGet]
        [Route("GetByIdPais/{IdPais}")]
        public IActionResult GetAll(int IdPais)
        {
            ML.Result result = _estado.GetByIdPais(IdPais);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
