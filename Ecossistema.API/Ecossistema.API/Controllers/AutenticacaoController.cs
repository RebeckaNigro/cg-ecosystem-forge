/*namespace Ecossistema.API.Controllers
{
    public class AutenticacaoController
    {
    }
}
*/
//using Ecossistema.API;

using Ecossistema.Services.Dto;
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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AutenticacaoController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _signInManager = signInManager;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
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

                return Ok(new
                {
                    id = user.Id,
                    email = user.Email,
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                    //expiration = token.ValidTo
                });
            }
            return Unauthorized();
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
        [HttpGet]
        [Route("token")]
        public IActionResult teste()
        {
           
          var logout = HttpContext.Request.Headers["Authorization"];
            return Ok(new Response { Status = "Success", Message = "teste realizado com sucesso!" });

        }


        [HttpPost]
        [Route("register-admin-parceiro")]
        public async Task<IActionResult> RegisterAdminParceiro([FromBody] RegisterModel model)
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

            if (!await _roleManager.RoleExistsAsync(UserRoles.AdminParceiro))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.AdminParceiro));

            if (await _roleManager.RoleExistsAsync(UserRoles.AdminParceiro))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.AdminParceiro);
            }
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again. " + result.Errors.FirstOrDefault().Description });

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("register-admin-geral")]
        public async Task<IActionResult> RegisterAdminGeral([FromBody] RegisterModel model)
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

            if (!await _roleManager.RoleExistsAsync(UserRoles.AdminGeral))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.AdminGeral));
            if (!await _roleManager.RoleExistsAsync(UserRoles.AdminParceiro))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.AdminParceiro));

            if (await _roleManager.RoleExistsAsync(UserRoles.AdminGeral))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.AdminGeral);
            }
            if (await _roleManager.RoleExistsAsync(UserRoles.AdminGeral))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.AdminParceiro);
            }
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("register-admin-master")]
        public async Task<IActionResult> RegisterAdminMaster([FromBody] RegisterModel model)
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

            if (!await _roleManager.RoleExistsAsync(UserRoles.AdminMaster))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.AdminMaster));
            if (!await _roleManager.RoleExistsAsync(UserRoles.AdminGeral))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.AdminGeral));
            if (!await _roleManager.RoleExistsAsync(UserRoles.AdminParceiro))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.AdminParceiro));

            if (await _roleManager.RoleExistsAsync(UserRoles.AdminMaster))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.AdminMaster);
            }
            if (await _roleManager.RoleExistsAsync(UserRoles.AdminMaster))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.AdminGeral);
                await _userManager.AddToRoleAsync(user, UserRoles.AdminParceiro);
            }
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
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
