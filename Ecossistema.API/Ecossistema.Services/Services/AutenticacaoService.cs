

using Ecossistema.Services.Dto;
using Ecossistema.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecossistema.Services.Services
{

    public class AutenticacaoService : IAutenticacaoService


    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        //private readonly UsuarioCriacaoDto _usuarioCriacaoDto;

        public AutenticacaoService(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _signInManager = signInManager;
            //_usuarioCriacaoDto = usuarioCriacaoDto; 
        }

        public async Task<RespostaPadrao> Login(LoginDto model)
        {
            var resposta = new RespostaPadrao("Login efetuado com sucesso!");
            LoginSucessoDto login = new LoginSucessoDto();
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    resposta.SetErroInterno("Usuário não existe");
                    return resposta;
                }
                if (!await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    resposta.SetErroInterno("Senha incorreta");
                    return resposta;
                }
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                login.Id = user.Id;
                login.Email = user.Email;
                login.Token = new JwtSecurityTokenHandler().WriteToken(token);
                resposta.Dado = login;
                return resposta;
            }
            catch (Exception e)
            {
                resposta.SetErroInterno(e.Message + ". " + e.InnerException);
                return resposta;
            }


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
        */
        
        /*public async Task<RespostaPadrao> RegistrarAdminParceiro([FromBody] ResgistrarLoginDto model)
        {
            try
            {
                var userExists = await _userManager.FindByEmailAsync(model.Email);
                RespostaPadrao resposta = new RespostaPadrao();
                if (userExists != null)
                {
                    resposta.SetChamadaInvalida("Usuário já existente");
                    return resposta;
                    //return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Usário já existente" });
                }



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
            catch (Exception)
            {

                throw;
            }
        }
        
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
        }*/

        public async Task<RespostaPadrao> RegistrarAdminMaster([FromBody] ResgistrarLoginDto model)
        {
            RespostaPadrao resposta = new RespostaPadrao("Usuário cadastrado com sucesso!");
            try
            {
                var emailExists = await _userManager.FindByEmailAsync(model.Email);
                var nameExists = await _userManager.FindByNameAsync(model.Username);
                if (emailExists != null)
                {
                    resposta.SetErroInterno("Email de usuário já cadastrado.");
                    return resposta;
                }
                if (nameExists != null)
                {
                    resposta.SetErroInterno("Nome de usuário já cadastrado.");
                    return resposta;
                }
                IdentityUser user = new()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Username
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    resposta.SetErroInterno("Não foi possível criar o usuário, tente novamente mais tarde.");
                    return resposta;
                }
                if (!await _roleManager.RoleExistsAsync(UserRolesDto.AdminMaster))
                    await _roleManager.CreateAsync(new IdentityRole(UserRolesDto.AdminMaster));
                if (!await _roleManager.RoleExistsAsync(UserRolesDto.AdminGeral))
                    await _roleManager.CreateAsync(new IdentityRole(UserRolesDto.AdminGeral));
                if (!await _roleManager.RoleExistsAsync(UserRolesDto.AdminParceiro))
                    await _roleManager.CreateAsync(new IdentityRole(UserRolesDto.AdminParceiro));

                if (await _roleManager.RoleExistsAsync(UserRolesDto.AdminMaster))
                {
                    await _userManager.AddToRoleAsync(user, UserRolesDto.AdminMaster);
                }
                if (await _roleManager.RoleExistsAsync(UserRolesDto.AdminMaster))
                {
                    await _userManager.AddToRoleAsync(user, UserRolesDto.AdminGeral);
                    await _userManager.AddToRoleAsync(user, UserRolesDto.AdminParceiro);
                }
                var teste = user.Id;
                return resposta;
            }
            catch (Exception e)
            {
                resposta.SetErroInterno(e.Message + ". " + e.InnerException);
                return resposta;
            }
        }
        
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
