using System.ComponentModel.DataAnnotations;

namespace Ecossistema.Services.Dto
{
    public class ResgistrarUsuarioComumDto
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string? NomeCompleto { get; set; }
        [Required(ErrorMessage = "Cpf é obrigatório")]
        public string? Cpf { get; set; }
        [Required(ErrorMessage = "Data de nascimento é obrigatório")]
        public DateTime DataNascimento { get; set; }
        public string? Telefone { get; set; }
        [Required(ErrorMessage = "Cargo é obrigatório")]
        public string? Cargo { get; set; }
        [Required(ErrorMessage = "Instituição é obrigatória")]
        public int InstituicaoId { get; set; }
        public int Role { get; set; } = 4;
        [Required(ErrorMessage = "Uf é obrigatória")]
        public string? Uf { get; set; }
        [Required(ErrorMessage = "Cidade é obrigatória")]
        public string? Cidade { get; set; }
        [Required(ErrorMessage = "Logradouro é obrigatório")]
        public string? Logradouro { get; set; }
        public string? Bairro { get; set; }
        public string? Numero { get; set; }
        public string? Cep { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email é obrigatório")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Confirmação de senha é obrigatória")]
        public string? ConfirmPassword { get; set; }
    }
}
