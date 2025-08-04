using BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly BL.Usuario _usuario;

        public UsuarioController(BL.Usuario usuario)
        {
            _usuario = usuario;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            ML.Result result = _usuario.GetAll();
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
        [Route("GetById/{IdUsuario}")]
        public IActionResult GetById(int IdUsuario)
        {
            ML.Result result = _usuario.GetById(IdUsuario);
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
        [Route("Delete/{IdUsuario}")]
        public IActionResult Delete(int IdUsuario)
        {
            ML.Result result = _usuario.Delete(IdUsuario);
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
        public IActionResult Add([FromBody] ML.Usuario usuario)
        {

            if (!string.IsNullOrEmpty(usuario.ImagenBase64))
            {
                usuario.Imagen = ConvertirBase64ABytes(usuario.ImagenBase64);
                usuario.ImagenBase64 = null;
            }
            

            ML.Result result = _usuario.Add(usuario);
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
        public IActionResult Update([FromBody] ML.Usuario usuario)
        {
            if (!string.IsNullOrEmpty(usuario.ImagenBase64))
            {
                usuario.Imagen = ConvertirBase64ABytes(usuario.ImagenBase64);
                usuario.ImagenBase64 = null;
            }

            ML.Result result = _usuario.Update(usuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        private byte[] ConvertirBase64ABytes(string base64)
        {
            if (string.IsNullOrEmpty(base64))
                return null;

            return Convert.FromBase64String(base64);
        }

    }
}
