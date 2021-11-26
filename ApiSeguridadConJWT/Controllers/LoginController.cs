using ApiSeguridadConJWT.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;


namespace ApiSeguridadConJWT.Controllers
{
    public class LoginController : ApiController
    {
        private practica_seguridadEntities db = new practica_seguridadEntities();

        // POST: api/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> LoginAsync(UsuarioLogin usuarioLogin)
        {
            if (usuarioLogin == null)
                return BadRequest("Usuario y Contraseña requeridos.");

            var _userInfo = await AutenticarUsuarioAsync(usuarioLogin.Email, usuarioLogin.Password);
            if (_userInfo != null)
            {
                return Ok(new { token = GenerarTokenJWT(_userInfo) });
            }
            else
            {
                return Unauthorized();
            }
        }

        //CONSULTAR SI EL USUARIO EXISTE Y SI SUS CREDENCIALES SON CORRECTAS.
        private async Task<usuario> AutenticarUsuarioAsync(string _email, string _password)
        {
            //LÓGICA DE AUTENTICACIÓN //

            //Se valida que el usuario este en la BD
            var usuarioSearch = db.usuario.Where(c => c.correo_electronico.Equals(_email)).FirstOrDefault();

            if (usuarioSearch == null)
            {
                return null;
            }

            //Se desencripta la contrasena y compara.
            if (!(Security.Decrypted.DesencriptarBase64(usuarioSearch.contrasena) == _password))
            {
                return null;
            }

            return new usuario()
            {
                // Id del Usuario en el Sistema de Información (BD)
                id_usuario = usuarioSearch.id_usuario,
                nombre_usuario = usuarioSearch.nombre_usuario,
                primer_apellido = usuarioSearch.primer_apellido,
                correo_electronico = usuarioSearch.correo_electronico,
            };
        }

        // GENERAMOS EL TOKEN CON LA INFORMACIÓN DEL USUARIO
        private string GenerarTokenJWT(usuario usuarioInfo)
        {
            // RECUPERAMOS LAS VARIABLES DE CONFIGURACIÓN
            var _ClaveSecreta = ConfigurationManager.AppSettings["ClaveSecreta"];
            var _Issuer = ConfigurationManager.AppSettings["Issuer"];
            var _Audience = ConfigurationManager.AppSettings["Audience"];
            if (!Int32.TryParse(ConfigurationManager.AppSettings["Expires"], out int _Expires))
                _Expires = 24;


            // CREAMOS EL HEADER //
            var _symmetricSecurityKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_ClaveSecreta));
            var _signingCredentials = new SigningCredentials(
                    _symmetricSecurityKey, SecurityAlgorithms.HmacSha256
                );
            var _Header = new JwtHeader(_signingCredentials);

            // CREAMOS LOS CLAIMS //
            var _Claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, usuarioInfo.id_usuario.ToString()),
                new Claim("nombre", usuarioInfo.nombre_usuario),
                new Claim("apellidos", usuarioInfo.primer_apellido),
                new Claim(JwtRegisteredClaimNames.Email, usuarioInfo.correo_electronico),
            };

            // CREAMOS EL PAYLOAD //
            var _Payload = new JwtPayload(
                    issuer: _Issuer,
                    audience: _Audience,
                    claims: _Claims,
                    notBefore: DateTime.UtcNow,
                    // Exipra a la 24 horas.
                    expires: DateTime.UtcNow.AddHours(_Expires)
                );

            // GENERAMOS EL TOKEN //
            var _Token = new JwtSecurityToken(
                    _Header,
                    _Payload
                );

            return new JwtSecurityTokenHandler().WriteToken(_Token);
        }
    }
}