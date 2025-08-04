using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class EstatusController : ControllerBase
    {
        private readonly BL.Estatus _blEstatus;

        public EstatusController(BL.Estatus blEstatus)
        {
            _blEstatus = blEstatus;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            ML.Result result = _blEstatus.GetAll();
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet]
        [Route("CambiarEstatus/{numeroPoliza}/{idEstatus}")]
        public IActionResult CambiarEstatus(int numeroPoliza, int idEstatus)
        {
            ML.Result result = _blEstatus.CambiarEstatus(numeroPoliza, idEstatus);
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
