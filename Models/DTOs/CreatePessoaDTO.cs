using System.ComponentModel.DataAnnotations;

namespace testeLar.Models.DTOs
{
    public class CreatePessoaDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        public string? Cpf { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        public DateTime DataDeNascimento { get; set; }

        [Required(ErrorMessage = "O status é obrigatório.")]
        public bool Status { get; set; }

        [Required(ErrorMessage = "É necessário informar ao menos um telefone.")]
        public List<Telefone>? Telefones { get; set; }
    }
}
