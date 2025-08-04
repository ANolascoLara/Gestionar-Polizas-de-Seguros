using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class RolController : ControllerBase
    {
        private readonly BL.Rol _blRol;
        public RolController(BL.Rol blRol)
        {
            _blRol = blRol;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            ML.Result result = _blRol.GetAll();
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
