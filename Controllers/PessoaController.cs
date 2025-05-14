using Microsoft.AspNetCore.Mvc;
using testeLar.Models;
using testeLar.Services;
using testeLar.Models.DTOs;
using Microsoft.Extensions.Logging;

namespace testeLar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly PessoaService _service;
        private readonly ILogger<PessoaController> _logger;

        public PessoaController(PessoaService service, ILogger<PessoaController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pessoa>>> Get()
        {
            _logger.LogInformation("Requisição GET: listando pessoas ativas");
            return await _service.GetAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pessoa>> Get(string id)
        {
            _logger.LogInformation("Requisição GET: buscando pessoa com ID: {Id}", id);
            var pessoa = await _service.GetByIdAsync(id);
            if (pessoa == null)
            {
                _logger.LogWarning("Pessoa com ID {Id} não encontrada", id);
                return NotFound();
            }
            return Ok(pessoa);
        }

       [HttpPost]
        public async Task<IActionResult> Post(CreatePessoaDTO dto)
        {
            _logger.LogInformation("Requisição POST: criando nova pessoa: {Nome}", dto.Nome);

            var novaPessoa = new Pessoa
            {
                Nome = dto.Nome,
                Cpf = dto.Cpf,
                DataDeNascimento = dto.DataDeNascimento,
                Status = dto.Status,
                Telefones = dto.Telefones
            };

            try
            {
                await _service.CreateAsync(novaPessoa);
                _logger.LogInformation("Pessoa criada com sucesso. ID: {Id}", novaPessoa.Id);

                return CreatedAtAction(nameof(Get), new { id = novaPessoa.Id }, novaPessoa);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao criar pessoa: {Mensagem}", ex.Message);

                if (ex.Message.Contains("CPF já cadastrado"))
                {
                    return BadRequest(new { mensagem = ex.Message });
                }

                return StatusCode(500, new { mensagem = "Erro interno ao criar pessoa." });
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Pessoa pessoa)
        {
            _logger.LogInformation("Requisição PUT: atualizando pessoa com ID: {Id}", id);

            var existing = await _service.GetByIdAsync(id);
            if (existing == null)
            {
                _logger.LogWarning("Pessoa com ID {Id} não encontrada para atualização", id);
                return NotFound();
            }

            pessoa.Id = existing.Id;

            try
            {
                await _service.UpdateAsync(id, pessoa);
                _logger.LogInformation("Pessoa com ID {Id} atualizada com sucesso", id);
                return Ok(new { mensagem = "Dados atualizados com sucesso." });
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao atualizar pessoa: {Mensagem}", ex.Message);

                if (ex.Message.Contains("CPF"))
                    return BadRequest(new { mensagem = ex.Message });

                return StatusCode(500, new { mensagem = "Erro interno ao atualizar pessoa." });
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            _logger.LogInformation("Requisição DELETE: desativando pessoa com ID: {Id}", id);

            var pessoa = await _service.GetByIdAsync(id);
            if (pessoa == null)
            {
                _logger.LogWarning("Pessoa com ID {Id} não encontrada para exclusão", id);
                return NotFound();
            }

            await _service.SoftDeleteAsync(id);
            _logger.LogInformation("Pessoa com ID {Id} desativada com sucesso", id);

            return NoContent();
        }
    }
}
