using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.User
{
    public class UserDto
    {        
        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        [StringLength(60, ErrorMessage = "Nome deve ter no máximo {1} caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo e-mail é obrigatório para Login.")]
        [EmailAddress(ErrorMessage = "O formato do e-mail é inválido.")]
        [StringLength(100, ErrorMessage = "E-mail deve ter no máximo {1} caracteres")]
        public string Email { get; set; }
    }
}
