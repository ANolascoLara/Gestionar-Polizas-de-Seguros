using Microsoft.AspNetCore.Mvc;

namespace PL_MVC.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly BL.IUsuario _usuarioService;

        public UsuarioController(BL.IUsuario usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();

            ML.Result result = _usuarioService.GetAll(usuario); 

            if (result.Correct && result.Objects != null)
            {
                usuario.Usuarios = result.Objects.ToList(); 
            }
            else
            {
                usuario.Usuarios = new List<object>();
                ViewBag.ErrorMessage = result.ErrorMessage;
                ViewBag.Excepcion = result.Ex?.Message;
            }

            return View(usuario);
        }

        [HttpGet]
        public IActionResult Delete(int IdUsuario)
        {
            ML.Result result = _usuarioService.Delete(IdUsuario);

            if (result.Correct)
            {
                ViewBag.Message = "Usuario eliminado correctamente.";
            }
            else
            {
                ViewBag.ErrorMessage = result.ErrorMessage;
                ViewBag.Excepcion = result.Ex?.Message;
            }

            return RedirectToAction("GetAll");
        }

    }
}
