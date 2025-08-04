using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TipoPolizaController : ControllerBase
    {
        private readonly BL.TipoPoliza _blTipoPoliza;

        public TipoPolizaController(BL.TipoPoliza blTipoPoliza)
        {
            _blTipoPoliza = blTipoPoliza;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            ML.Result result = _blTipoPoliza.GetAll();
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
