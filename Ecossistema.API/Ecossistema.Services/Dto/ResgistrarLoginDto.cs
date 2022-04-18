using System.ComponentModel.DataAnnotations;

namespace Ecossistema.Services.Dto
{
    public class ResgistrarLoginDto
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Cargo é obrigatório")]
        public string? Cargo { get; set; }
        [Required(ErrorMessage = "Nível de administrador é obrigatório")]
        public int Role { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
