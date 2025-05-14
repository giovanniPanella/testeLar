namespace testeLar.Models.DTOs
{
    public class CreatePessoaDTO
    {
        public string? Nome { get; set; }
        public string? Cpf { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public bool Status { get; set; }
        public List<Telefone>? Telefones { get; set; }
    }
}