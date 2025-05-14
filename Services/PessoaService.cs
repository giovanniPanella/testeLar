using MongoDB.Driver;
using Microsoft.Extensions.Options;
using testeLar.Models;
using testeLar.Data;

namespace testeLar.Services
{
    public class PessoaService
    {
        private readonly IMongoCollection<Pessoa> _pessoas;

        public PessoaService(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _pessoas = database.GetCollection<Pessoa>(settings.Value.PessoaCollectionName);
        }

        public async Task<List<Pessoa>> GetAsync() =>
            await _pessoas.Find(p => p.Status).ToListAsync();

        public async Task<Pessoa> GetByIdAsync(string id) =>
            await _pessoas.Find(p => p.Id == id && p.Status).FirstOrDefaultAsync();

        public async Task CreateAsync(Pessoa pessoa) =>
            await _pessoas.InsertOneAsync(pessoa);

        public async Task UpdateAsync(string id, Pessoa pessoa) =>
            await _pessoas.ReplaceOneAsync(p => p.Id == id, pessoa);

        public async Task SoftDeleteAsync(string id)
        {
            var pessoa = await GetByIdAsync(id);
            if (pessoa != null)
            {
                pessoa.Status = false;
                await _pessoas.ReplaceOneAsync(p => p.Id == id, pessoa);
            }
        }
    }
}
