using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Pessoa
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; } // pode ser nulo inicialmente

    public string? Nome { get; set; }

    public string? Cpf { get; set; }

    public DateTime DataDeNascimento { get; set; }

    public bool Status { get; set; }

    public List<Telefone>? Telefones { get; set; }
}
