using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly BL.Login _blLogin;
        public LoginController (BL.Login blLogin)
        {
            _blLogin = blLogin;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] ML.Login login)
        {
            ML.Result result = _blLogin.LoggIn(login);

            if (result.Correct)
            {
                ML.Usuario usuario = (ML.Usuario)result.Object;
                string token = GenerateToken(usuario);

                return Ok(new
                {
                    correct = true,
                    token = token
                });
            } else
            {
                return BadRequest(result);
            }
        }


        private string GenerateToken(ML.Usuario usuario)
        {
            var claims = new[]
            {
                new Claim("IdUsuario", usuario.IdUsuario.ToString()),
                new Claim("Nombre", usuario.NombreUsuario),
                new Claim("ApellidoPaterno", usuario.ApellidoPaterno),
                new Claim("ApellidoMaterno", usuario.ApellidoMaterno),
                new Claim(ClaimTypes.Role, usuario.Rol.NombreRol)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("S3cr3t_k3y!.123_S3cr3t_k3y!.123.Pass@word1"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "localhost",
                audience: "localhost",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
