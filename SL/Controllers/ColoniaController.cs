using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColoniaController : ControllerBase
    {
        private readonly BL.Colonia _colonia;

        public ColoniaController(BL.Colonia colonia)
        {
            _colonia = colonia;
        }

        [HttpGet]
        [Route("GetByIdMunicipio/{IdMunicipio}")]
        public IActionResult GetAll(int IdMunicipio)
        {
            ML.Result result = _colonia.GetByIdMunicipio(IdMunicipio);

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
