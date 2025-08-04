using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace PL_MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;
        
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Form()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Form(ML.Login login)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    string endpoint = _configuration["EndPointLogin:LoginEndPoint"];
                    httpClient.BaseAddress = new Uri(endpoint);

                    var responseTask = httpClient.PostAsJsonAsync<ML.Login>("", login);
                    responseTask.Wait();

                    var response = responseTask.Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var readTask = response.Content.ReadAsStringAsync();
                        readTask.Wait();

                        string responseJson = readTask.Result;
                        dynamic json = JsonConvert.DeserializeObject<dynamic>(responseJson);
                        bool correct = json.correct;

                        if (correct)
                        {
                            string token = json.token;

                            // Decodificar token para extraer claims
                            var handler = new JwtSecurityTokenHandler();
                            var jwtToken = handler.ReadJwtToken(token);

                            var rol = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                            var idUsuarioStr = jwtToken.Claims.FirstOrDefault(c => c.Type == "IdUsuario")?.Value;

                            int.TryParse(idUsuarioStr, out int idUsuario);

                            Response.Cookies.Append("session", token, new CookieOptions
                            {
                                HttpOnly = true,
                                Secure = true,
                                SameSite = SameSiteMode.Strict,
                                Expires = DateTimeOffset.UtcNow.AddMinutes(10)
                            });

                            HttpContext.Session.SetString("Rol", rol);

                            if (rol == "Admin" || rol == "Broker")
                            {
                                return RedirectToAction("GetAll", "Usuario");
                            }
                            else if (rol == "Cliente")
                            {
                                return RedirectToAction("GetAllByUsuario", "Poliza", new { idUsuario = idUsuario });
                            }
                            else
                            {
                                // Para Broker u otros roles
                                return RedirectToAction("Index", "Home");
                            }
                        }
                        else
                        {
                            ViewBag.Error = "Credenciales incorrectas.";
                            return View();
                        }
                    }
                    else
                    {
                        var readTask = response.Content.ReadAsStringAsync();
                        readTask.Wait();
                        string errorJson = readTask.Result;

                        dynamic errorObj = JsonConvert.DeserializeObject<dynamic>(errorJson);
                        string errorMessage = errorObj?.errorMessage ?? "Correo o contraseña incorrectos";

                        ViewBag.Error = $"Error al iniciar sesión: {errorMessage}";
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Ocurrió un error: {ex.Message}";
                return View("Form");
            }
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult CerrarSession()
        {
            // Borra la cookie "session"
            if (Request.Cookies["session"] != null)
            {
                Response.Cookies.Delete("session");
            }

            // Opcional: limpiar sesión si usas HttpContext.Session
            HttpContext.Session.Clear();

            // Redirigir a la página de login u otra vista
            return RedirectToAction("Form", "Login");
        }


    }
}
