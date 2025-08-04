using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PolizaController : ControllerBase
    {
        private readonly BL.Poliza _blPoliza;

        public PolizaController (BL.Poliza blPoliza)
        {
            _blPoliza = blPoliza;
        }

        [HttpGet]
        [Route("GetAllByIdUsuario/{IdUsuario}")]
        public IActionResult GetAll(int? IdUsuario)
        {
            ML.Result result = _blPoliza.GetAllByIdUsuario(IdUsuario.Value);
            if (result.Correct)
            {
                return Ok(result);
            } else
            {
                return BadRequest(result);
            }
        }

        [HttpGet]
        [Route("GetById/{IdPoliza}")]
        public IActionResult GetById(int IdPoliza)
        {
            ML.Result result = _blPoliza.GetById(IdPoliza);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpDelete]
        [Route("Delete/{IdPoliza}")]
        public IActionResult Delete(int IdPoliza)
        {
            ML.Result result = _blPoliza.Delete(IdPoliza);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody]ML.Poliza poliza)
        {
            ML.Result result = _blPoliza.Add(poliza);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPut]
        [Route("Update")]
        public IActionResult Update([FromBody] ML.Poliza poliza)
        {
            ML.Result result = _blPoliza.Update(poliza);
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
        [Route("ObtenerNumeroPoliza")]
        public IActionResult ObtenerNumeroPoliza()
        {
            ML.Result result = _blPoliza.ObtenerNumeroPoliza();
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
