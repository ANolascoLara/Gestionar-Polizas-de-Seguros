using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PL_MVC.Controllers
{
    [Authorize(Roles ="Admin")]
    public class UsuarioController : Controller
    {

        private readonly IConfiguration _configuration;

        public UsuarioController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetAll()
        {

            //CRUD Usuario
            ViewBag.UsuarioGetAllEndPoint = _configuration["EndPointsUsuario:UsuarioGetAllEndPoint"];
            ViewBag.UsuarioGetByIdEndPoint = _configuration["EndPointsUsuario:UsuarioGetByIdEndPoint"];
            ViewBag.UsuarioAddEndPoint = _configuration["EndPointsUsuario:UsuarioAddEndPoint"];
            ViewBag.UsuarioUpdateEndPoint = _configuration["EndPointsUsuario:UsuarioUpdateEndPoint"];
            ViewBag.UsuarioDeleteEndPoint = _configuration["EndPointsUsuario:UsuarioDeleteEndPoint"];
            ViewBag.RolGetAllEndPoint = _configuration["EndPointsUsuario:RolGetAllEndPoint"];


            //Direccion

            ViewBag.PaisGetAllEndPoint = _configuration["EndPointsDireccion:PaisGetAllEndPoint"];
            ViewBag.EstadoGetByIdPaisEndPoint = _configuration["EndPointsDireccion:EstadoGetByIdPaisEndPoint"];
            ViewBag.MunicipioGetByIdEstado = _configuration["EndPointsDireccion:MunicipioGetByIdEstado"];
            ViewBag.ColoniaGetByIdMunicipio = _configuration["EndPointsDireccion:ColoniaGetByIdMunicipio"];
            return View();
        }

    }
}
