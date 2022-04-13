

using Ecossistema.Services.Dto;
using Ecossistema.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecossistema.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IAutenticacaoService _autenticacaoService;

        public AutenticacaoController(IAutenticacaoService autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<RespostaPadrao> Login([FromBody] LoginDto model)
        {
            return await _autenticacaoService.Login(model);
        }

        /*[HttpPost]
        [Route("logout")]
        public IActionResult Logout()
        {
            var logout =  _signInManager.SignOutAsync();
            if (logout.IsCompletedSuccessfully)
                return Ok(new Response { Status = "Success", Message = "Logout realizado com sucesso!" });
            else { return BadRequest(logout); }    

        }*/
        /*
        [HttpGet]
        [Route("token")]
        public IActionResult teste()
        {
           
          var logout = HttpContext.Request.Headers["Authorization"];
            return Ok(new Response { Status = "Success", Message = "teste realizado com sucesso!" });

        }


        [HttpPost]
        [Route("registrar-admin-parceiro")]
        public async Task<IActionResult> RegistrarAdminParceiro([FromBody] ResgistrarLoginDto model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Usário já existente" });

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!await _roleManager.RoleExistsAsync(UserRolesDto.AdminParceiro))
                await _roleManager.CreateAsync(new IdentityRole(UserRolesDto.AdminParceiro));

            if (await _roleManager.RoleExistsAsync(UserRolesDto.AdminParceiro))
            {
                await _userManager.AddToRoleAsync(user, UserRolesDto.AdminParceiro);
            }
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again. " + result.Errors.FirstOrDefault().Description });

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("registrar-admin-geral")]
        public async Task<IActionResult> RegistrarAdminGeral([FromBody] ResgistrarLoginDto model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Usuário já existente!" });

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again. " + result.Errors.FirstOrDefault().Description });

            if (!await _roleManager.RoleExistsAsync(UserRolesDto.AdminGeral))
                await _roleManager.CreateAsync(new IdentityRole(UserRolesDto.AdminGeral));
            if (!await _roleManager.RoleExistsAsync(UserRolesDto.AdminParceiro))
                await _roleManager.CreateAsync(new IdentityRole(UserRolesDto.AdminParceiro));

            if (await _roleManager.RoleExistsAsync(UserRolesDto.AdminGeral))
            {
                await _userManager.AddToRoleAsync(user, UserRolesDto.AdminGeral);
            }
            if (await _roleManager.RoleExistsAsync(UserRolesDto.AdminGeral))
            {
                await _userManager.AddToRoleAsync(user, UserRolesDto.AdminParceiro);
            }
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }
        */
        [HttpPost]
        [Route("registrar-admin-master")]
        public async Task<RespostaPadrao> RegistrarAdminMaster([FromBody] ResgistrarLoginDto model)
        {
            return await _autenticacaoService.RegistrarAdminMaster(model);
        }
        
    }
}
