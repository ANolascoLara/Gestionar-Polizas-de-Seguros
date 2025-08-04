using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class MunicipioController : ControllerBase
    {
        private readonly BL.Municipio _municipio;

        public MunicipioController(BL.Municipio municipio)
        {
            _municipio = municipio;
        }

        [HttpGet]
        [Route("GetByIdEstado/{IdEstado}")]
        public IActionResult GetAll(int IdEstado)
        {
            ML.Result result = _municipio.GetByIdEstado(IdEstado);

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
