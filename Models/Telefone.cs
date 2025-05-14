using System.ComponentModel.DataAnnotations;

namespace testeLar.Models
{
    public class Telefone
    {
        [Required(ErrorMessage = "O tipo de telefone é obrigatório.")]
        public string? Tipo { get; set; }

        [Required(ErrorMessage = "O número do telefone é obrigatório.")]
        public string? Numero { get; set; }
    }
}