using ApiRifaCasinoPIA.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiRifaCasinoPIA.Controllers
{
    [ApiController]
    [Route("api/cuentas")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentasController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<IdentityUser> signInManager;

        public CuentasController(UserManager<IdentityUser> userManager,
            IConfiguration configuration, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
        }

        [HttpPost("registrar")] //para poder registrar se hace un nuevo 
        [AllowAnonymous]
        public async Task<ActionResult<RespuestaAutenticacionDTO>> Register(CredencialesUsuarios credencialesUser)
        {
            var userCuenta = new IdentityUser { UserName = credencialesUser.email, Email = credencialesUser.email };
            var resultadoUsuers = await userManager.CreateAsync(userCuenta, credencialesUser.password);

            if (resultadoUsuers.Succeeded)//retorn el token
            {
                return await ConstruirToken(credencialesUser);
            }
            else
            {
                return BadRequest(resultadoUsuers.Errors);
            }
        }

        [HttpPost("login")] //checando login pq no funciona
        [AllowAnonymous]
        public async Task<ActionResult<RespuestaAutenticacionDTO>> Login(CredencialesUsuarios credencialesUsuario)
        {
            var resultadoUsuario = await signInManager.PasswordSignInAsync(credencialesUsuario.email,
                credencialesUsuario.password, isPersistent: false, lockoutOnFailure: false);

            if (resultadoUsuario.Succeeded)
            {
                return await ConstruirToken(credencialesUsuario);
            }
            else
            {
                return BadRequest("<<--El Login es Incorrecto-->");
            }
        }

        private async Task<RespuestaAutenticacionDTO> ConstruirToken(CredencialesUsuarios credencialesUsuarios)
        {
            var claims = new List<Claim>()
            {
                new Claim("email", credencialesUsuarios.email),
            };

            var usuersClaim  = await userManager.FindByEmailAsync(credencialesUsuarios.email);
            var claimsDB = await userManager.GetClaimsAsync(usuersClaim );

            claims.AddRange(claimsDB);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Keyjwt"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddDays(100);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expiracion, signingCredentials: creds);

            return new RespuestaAutenticacionDTO()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiracion = expiracion
            };
        }

        [HttpPost("HacerAdministrador")]

        public async Task<ActionResult> GiveAdmin(EditarAdminDTO editarAdminDTO)
        {
            var usuario = await userManager.FindByEmailAsync(editarAdminDTO.email);
            await userManager.AddClaimAsync(usuario, new Claim("esAdmin", "1"));
            return NoContent();
        }

        [HttpPost("RemoverAdministrador")]
        public async Task<ActionResult> RemoveAdmin(EditarAdminDTO editarAdminDTO)
        {
            var usuario = await userManager.FindByEmailAsync(editarAdminDTO.email);
            await userManager.RemoveClaimAsync(usuario, new Claim("esAdmin", "1"));
            return NoContent();
        }
    }
}