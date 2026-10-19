using System.ComponentModel.DataAnnotations;

namespace Api01.DTOs
{
    public class CriarDTO
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Usuario { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
        
    }
}
