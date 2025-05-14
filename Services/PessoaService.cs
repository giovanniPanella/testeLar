using MongoDB.Driver;
using Microsoft.Extensions.Options;
using testeLar.Data;
using MongoDB.Driver.Core.Servers; 
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

            // Criar índice único para CPF (só é criado uma vez)
            var indexKeys = Builders<Pessoa>.IndexKeys.Ascending(p => p.Cpf);
            var indexOptions = new CreateIndexOptions { Unique = true };
            var indexModel = new CreateIndexModel<Pessoa>(indexKeys, indexOptions);
            _pessoas.Indexes.CreateOne(indexModel);
        }

        public async Task<List<Pessoa>> GetAsync() =>
            await _pessoas.Find(p => p.Status).ToListAsync();

        public async Task<Pessoa> GetByIdAsync(string id) =>
            await _pessoas.Find(p => p.Id == id && p.Status).FirstOrDefaultAsync();

        public async Task CreateAsync(Pessoa pessoa)
        {
            try
            {
                await _pessoas.InsertOneAsync(pessoa);
            }
            catch (MongoWriteException ex) when (ex.WriteError.Category == ServerErrorCategory.DuplicateKey)
            {
                throw new Exception("CPF já cadastrado.");
            }
        }

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
