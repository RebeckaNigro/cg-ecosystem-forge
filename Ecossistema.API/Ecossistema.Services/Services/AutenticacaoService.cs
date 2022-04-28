using Ecossistema.Data.Interfaces;
using Ecossistema.Domain.Entities;
using Ecossistema.Services.Dto;
using Ecossistema.Services.Interfaces;
using Ecossistema.Util.Const;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        //private readonly UsuarioCriacaoDto _usuarioCriacaoDto;

        public AutenticacaoService(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration, IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
            _emailService = emailService;

            //_usuarioCriacaoDto = usuarioCriacaoDto; 
        }

        /*public async Task<RespostaPadrao> Login(LoginDto model)
        {
            var resposta = new RespostaPadrao("Login efetuado com sucesso!");
            LoginSucessoDto login = new LoginSucessoDto();
            try
            {
                var user = await _userManager.FindByEmailAsync(model.UserName);
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
                var usuario = await _unitOfWork.Usuarios.FindAsync(x => x.AspNetUserId == user.Id);
                if(usuario != null)
                {
                    login.Id = usuario.Id;
                    login.UserName = user.UserName;
                    login.InstituicaoId = usuario.InstituicaoId;
                    login.Token = new JwtSecurityTokenHandler().WriteToken(token);
                    resposta.Retorno = login;
                }
                else
                {
                    resposta.SetErroInterno("Usuário não tem mais acesso, favor solicitar um recadastramento");
                }

                return resposta;
            }
            catch (Exception e)
            {
                resposta.SetErroInterno(e.Message + ". " + e.InnerException);
                return resposta;
            }

        }*/

        public async Task<RespostaPadrao> Login(LoginDto model)
        {
            var resposta = new RespostaPadrao("Login efetuado com sucesso!");
            LoginSucessoDto login = new LoginSucessoDto();
            try
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user == null)
                {
                    resposta.SetErroInterno("Usuário não existe");
                    return resposta;
                }
                var resultadoIdentity = await _signInManager
               .PasswordSignInAsync(model.UserName, model.Password, false, false);
                if (resultadoIdentity.Succeeded)
                {
                    var identityUser = _signInManager
                        .UserManager
                        .Users
                        .FirstOrDefault(usuario =>
                         usuario.NormalizedUserName == model.UserName.ToUpper());
                        var userRoles = await _userManager.GetRolesAsync(user);
                    
                   
                    var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }
                     var token = CreateToken(authClaims);

                    var usuario = await _unitOfWork.Usuarios.FindAsync(x => x.AspNetUserId == user.Id);
                    if (usuario != null)
                    {
                        login.Id = usuario.Id;
                        login.UserName = user.UserName;
                        login.InstituicaoId = usuario.InstituicaoId;
                        login.Token = new JwtSecurityTokenHandler().WriteToken(token);
                        resposta.Retorno = login;
                    }

                    else
                    {
                        resposta.SetErroInterno("Usuário não tem mais acesso, favor solicitar um recadastramento");
                    }
                    //resposta.Retorno = token;
                }
                else
                {
                    resposta.SetErroInterno("Usuário ou senha incorreta");
                }
                return resposta;
            }
            catch (Exception e)
            {
                resposta.SetErroInterno(e.Message + ". " + e.InnerException);
                return resposta;
            }

        }

        public async Task<RespostaPadrao> Logout()
        {
            var resposta = new RespostaPadrao("Logout efetuado com sucesso!");
            var logout =  _signInManager.SignOutAsync();
            if (logout.IsCompletedSuccessfully)
                return resposta;
            else {
                resposta.SetErroInterno("Logout falhou");
                return resposta;
            }    
        }

        public async Task<RespostaPadrao> RegistrarAdmin([FromBody] ResgistrarLoginDto model)
        {
            RespostaPadrao resposta = new RespostaPadrao("Usuário cadastrado com sucesso!");
            try
            {
                var emailExists = await _userManager.FindByEmailAsync(model.Email);
                var nameExists = await _userManager.FindByNameAsync(model.Username);
                var cargo = model.Cargo;
                var role = model.Role; ;
                var instituicao = await _unitOfWork.Instituicoes.FindAsync(x => x.Id == model.InstituicaoId);
                if(instituicao == null)
                {
                    resposta.SetErroInterno("Instituição inexistente.");
                    return resposta;
                }
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
                if(role <= 0 || role > 3)
                {
                    resposta.SetErroInterno("Nível de administrador deve ser de 1 a 3");
                    return resposta;
                }
                if(model.Password != model.ConfirmPassword)
                {
                    resposta.SetErroInterno("Senha e confirmação estão diferentes, digite novamente");
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
                if (role == 1)
                {
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
                }
                if(role == 2)
                {
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
                }
                if(role == 3)
                {
                    if (!await _roleManager.RoleExistsAsync(UserRolesDto.AdminParceiro))
                        await _roleManager.CreateAsync(new IdentityRole(UserRolesDto.AdminParceiro));

                    if (await _roleManager.RoleExistsAsync(UserRolesDto.AdminParceiro))
                    {
                        await _userManager.AddToRoleAsync(user, UserRolesDto.AdminParceiro);
                    }
                }
                var obj = new Usuario(1, model.InstituicaoId, user.Id, DateTime.Now, model.Cargo, 1, DateTime.Now);
                await _unitOfWork.Usuarios.AddAsync(obj);
                _unitOfWork.Complete();
                return resposta;
            }
            catch (Exception e)
            {
                resposta.SetErroInterno(e.Message + ". " + e.InnerException);
                return resposta;
            }
        }

        /*private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }*/

        public JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            
            /*Claim[] direitosUsuario = new Claim[]
            {
                new Claim("username", usuario.UserName),
                new Claim("id", usuario.Id.ToString()),
            };

            var chave = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("0asdjas09djsa09djasdjsadajsd09asjd09sajcnzxn")
                );*/
            var credenciais = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.UtcNow.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(credenciais, SecurityAlgorithms.HmacSha256)
                
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return token;
        }

        public async Task<RespostaPadrao> GerarCodigoRedefinirSenha(string email)
        {

            var resposta = new RespostaPadrao("Token criado com sucesso!");
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    resposta.SetErroInterno("Campo email não pode ser vazio");
                    return resposta;
                }

                // Get Identity User details user user manager
                var aspUser = await _userManager.FindByEmailAsync(email);
                if(aspUser == null)
                {
                    resposta.SetErroInterno("Email não econtrado");
                    return resposta;
                }
                var a = aspUser.Id;
               

                var user = await _unitOfWork.Usuarios.FindAsync(x => x.AspNetUserId == a);


                // Generate password reset token
                var token = await _userManager.GeneratePasswordResetTokenAsync(aspUser);

                // Generate OTP
                int otp = RandomNumberGeneartor.Generate(100000, 999999);

                user.OTP = otp.ToString();
                user.Token = token;
                user.DataCriacaoOTP = DateTime.UtcNow;
                _unitOfWork.Usuarios.Update(user);
                _unitOfWork.Complete();
                // Save data into db with OTP

                // to do: Send token in email
                var enviar = new List<string>();
                enviar.Add(email);
                var mensagem = new Mensagem(enviar);
                mensagem.SetEmailRedefinirSenha(email, aspUser.UserName, user.OTP);
                await _emailService.EnviarEmail(mensagem);

                return resposta;
            }


            catch (Exception e)
            {
                resposta.SetErroInterno(e.Message + ". " + e.InnerException);
                return resposta;
            }
        }

        public async Task<RespostaPadrao> RedefinirSenha([FromBody] RedefinirSenhaDto model)
        {
            var resposta = new RespostaPadrao("Senha redefinida com sucesso!");
            try
            {
                if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.NovaSenha))
                {
                    resposta.SetErroInterno("Email e senha não podem ser vazios");
                    return resposta;
                }

                // Get Identity User details user  manager
                var userLogin = await _userManager.FindByEmailAsync(model.Email);

                var user = await _unitOfWork.Usuarios.FindAsync(x => x.AspNetUserId == userLogin.Id);

                if(user.OTP != model.OTP)
                {
                    resposta.SetErroInterno("OTP inválido ou incorreto, preencha novamente");
                    return resposta;
                }
                if (model.NovaSenha != model.ConfirmaSenha)
                {
                    resposta.SetErroInterno("Os campos de senha e confirmação estão diferentes, digite novamente.");
                    return resposta;
                }


                DateTime dataOTP = user.DataCriacaoOTP.Value;
                var expirationDateTimeUtc = dataOTP.AddMinutes(5);


                if (expirationDateTimeUtc < DateTime.UtcNow)
                {
                    resposta.SetErroInterno("OTP expirado, solicite a redefinição de senha novamente");
                    return resposta;
                }

                var res = await _userManager.ResetPasswordAsync(userLogin
                    , user.Token, model.NovaSenha);

                return resposta;
            }
            catch (Exception ex)
            {
                resposta.SetErroInterno(ex.Message + ". " + ex.InnerException);
                return resposta;
            }

        }

    }
}
