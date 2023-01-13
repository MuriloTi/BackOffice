using BackOffice.Application.InputModels;
using BackOffice.Core.Entities;
using BackOffice.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BackOffice.API.Controllers
{
    [Route("api/enderecos")]
    public class EnderecosController : Controller
    {
        private readonly IEnderecosRepository _enderecosRepository;
        private readonly IPessoasRepository _pessoasRepository;

        public EnderecosController(IEnderecosRepository enderecosRepository, IPessoasRepository pessoasRepository)
        {
            _enderecosRepository = enderecosRepository;
            _pessoasRepository = pessoasRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var endereco = await _enderecosRepository.GetDTOByIdAsync(id);
            if (endereco == null)
            {
                return BadRequest("Endereço não encontrado");
            }
            return Ok(endereco);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateEnderecoInputModel inputModel)
        {
            var pessoa = await _pessoasRepository.GetByIdAsync(inputModel.PessoaId);
            if (pessoa == null)
            {
                return BadRequest("Pessoa não encontrada");
            }
            var endereco = new Endereco(inputModel.PessoaId, inputModel.CEP, inputModel.Estado, inputModel.Cidade, inputModel.Bairro, inputModel.Logradouro, inputModel.Numero, inputModel.Complemento);
            await _enderecosRepository.AddAsync(endereco);
            return CreatedAtAction(nameof(GetById), new { endereco.Id }, inputModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateEnderecoInputModel inputModel)
        {
            var endereco = await _enderecosRepository.GetByIdAsync(id);
            if (endereco == null)
            {
                return BadRequest("Endereço não encontrado");
            }
            endereco.Update(inputModel.CEP, inputModel.Estado, inputModel.Cidade, inputModel.Bairro, inputModel.Logradouro, inputModel.Numero, inputModel.Complemento);
            await _enderecosRepository.SaveChangesAsync();
            return Accepted();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var endereco = await _enderecosRepository.GetByIdAsync(id);
            if (endereco == null)
            {
                return BadRequest("Endereço não encontrado");
            }
            await _enderecosRepository.DeleteAsync(endereco);
            return Accepted();
        }

    }
}
