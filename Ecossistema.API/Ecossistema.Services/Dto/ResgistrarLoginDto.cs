using System.ComponentModel.DataAnnotations;

namespace Ecossistema.Services.Dto
{
    public class ResgistrarLoginDto
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Cargo é obrigatório")]
        public string? Cargo { get; set; }
        [Required(ErrorMessage = "Instituição é obrigatória")]
        public int InstituicaoId { get; set; }
        [Required(ErrorMessage = "Nível de administrador é obrigatório")]
        public int Role { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email é obrigatório")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        public string? Password { get; set; }
    }
}
