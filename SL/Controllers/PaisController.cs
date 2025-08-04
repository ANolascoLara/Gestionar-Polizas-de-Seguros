using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PaisController : ControllerBase
    {
        private readonly BL.Pais _pais;

        public PaisController(BL.Pais pais)
        {
            _pais = pais;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            ML.Result result = _pais.GetAll();

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
