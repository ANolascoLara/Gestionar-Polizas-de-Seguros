using BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuario _usuario;

        public UsuarioController(IUsuario usuario)
        {
            _usuario = usuario;
        }

        [HttpGet]
        [Route("UsuarioGetAll")]
        public IActionResult UsuarioGetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            var result = _usuario.GetAll(usuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("UsuarioGetById/{IdUsuario}")]
        public IActionResult UsuarioGetById(int IdUsuario)
        {
            var result = _usuario.GetById(IdUsuario);

            if (result.Correct)
            {
                return Ok(result);
            }
                
            else
            {
                return BadRequest();
            }
                
        }
    }
}
