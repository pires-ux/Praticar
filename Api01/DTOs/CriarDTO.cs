using System.ComponentModel.DataAnnotations;
using Api01.Validação;

namespace Api01.DTOs
{
    public class CriarDTO
    {
        public int Id { get; set; }
        [Required]
        [NaoPossuiNumero("Nome")]
        public string Nome { get; set; }
        [Required]
        [StringLength(maximumLength: 20, MinimumLength =3)]
        public string Usuario { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(maximumLength:20, MinimumLength = 5, ErrorMessage = "Senha deve conter no minimo 5 caracteres e o maximo de 20")]
        public string Senha { get; set; }
        
    }
}
