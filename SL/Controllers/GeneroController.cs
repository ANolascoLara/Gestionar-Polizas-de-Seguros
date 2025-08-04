using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class GeneroController : ControllerBase
    {
        private readonly BL.Genero _genero;
        public GeneroController(BL.Genero genero)
        {
            _genero = genero;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            ML.Result result = _genero.GetAll();

            if (result.Correct)
            {
                return Ok(result);
            } else
            {
                return BadRequest(result);
            }
        }
    }
}
