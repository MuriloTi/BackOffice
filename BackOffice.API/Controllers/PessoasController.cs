using BackOffice.Application.InputModels;
using BackOffice.Core.Entities;
using BackOffice.Core.Enums;
using BackOffice.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BackOffice.API.Controllers
{
    [Route("api/pessoas")]
    public class PessoasController : Controller
    {
        private readonly IPessoasRepository _pessoasRepository;

        public PessoasController(IPessoasRepository pessoasRepository)
        {
            _pessoasRepository = pessoasRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string? query)
        {
            query ??= "";
            query = query.Trim();
            var pessoas = await _pessoasRepository.GetAllAsync(query);
            return Ok(pessoas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pessoa = await _pessoasRepository.GetDTOByIdAsync(id);
            if (pessoa == null)
            {
                return BadRequest("Pessoa não encontrada");
            }
            return Ok(pessoa);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreatePessoaInputModel inputModel)
        {
            if (inputModel.Tipo == TipoPessoaEnum.Fisica)
            {
                if (string.IsNullOrEmpty(inputModel.CPF))
                {
                    return BadRequest("É necessário informar o CPF para Pessoa Física");
                }
                var pessoaExistente = await _pessoasRepository.GetByCPFAsync(inputModel.CPF);
                if (pessoaExistente != null)
                {
                    return BadRequest("CPF já cadastrado");
                }
            }
            else if (inputModel.Tipo == TipoPessoaEnum.Juridica)
            {
                if (string.IsNullOrEmpty(inputModel.CNPJ))
                {
                    return BadRequest("É necessário informar o CNPJ para Pessoa Jurídica");
                }
                var pessoaExistente = await _pessoasRepository.GetByCNPJAsync(inputModel.CNPJ);
                if (pessoaExistente != null)
                {
                    return BadRequest("CNPJ já cadastrado");
                }
            }
            else
            {
                return BadRequest("É necessário informar o Tipo de Pessoa corretamente (Física(1), ou Jurídica(2))");
            }
            if (!inputModel.Cliente && !inputModel.Fornecedor && !inputModel.Colaborador)
            {
                return BadRequest("É necessário informar pelo menos uma qualificação de Pessoa");
            }
            var pessoa = new Pessoa(inputModel.Tipo, inputModel.Nome, inputModel.Apelido, inputModel.CPF, inputModel.CNPJ, inputModel.Cliente, inputModel.Fornecedor, inputModel.Colaborador);
            await _pessoasRepository.AddAsync(pessoa);
            return CreatedAtAction(nameof(GetById), new { pessoa.Id }, pessoa);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdatePessoaInputModel inputModel)
        {
            var pessoa = await _pessoasRepository.GetByIdAsync(id);
            if (pessoa == null)
            {
                return BadRequest("Pessoa não encontrada");
            }
            if (pessoa.Tipo != inputModel.Tipo)
            {
                if (inputModel.Tipo == TipoPessoaEnum.Fisica)
                {
                    if (string.IsNullOrEmpty(inputModel.CPF))
                    {
                        return BadRequest("É necessário informar o CPF para Pessoa Física");
                    }
                    if (pessoa.CPF != inputModel.CPF)
                    {
                        var pessoaExistente = await _pessoasRepository.GetByCPFAsync(inputModel.CPF);
                        if (pessoaExistente != null)
                        {
                            return BadRequest("CPF já cadastrado");
                        }
                    }
                }
                else if (inputModel.Tipo == TipoPessoaEnum.Juridica)
                {
                    if (string.IsNullOrEmpty(inputModel.CNPJ))
                    {
                        return BadRequest("É necessário informar o CNPJ para Pessoa Jurídica");
                    }
                    if (pessoa.CNPJ != inputModel.CNPJ)
                    {
                        var pessoaExistente = await _pessoasRepository.GetByCNPJAsync(inputModel.CNPJ);
                        if (pessoaExistente != null)
                        {
                            return BadRequest("CNPJ já cadastrado");
                        }
                    }
                }
                else
                {
                    return BadRequest("É necessário informar o Tipo de Pessoa corretamente (Física(1), ou Jurídica(2))");
                }
            }
            if (!string.IsNullOrEmpty(inputModel.CPF) && pessoa.CPF != inputModel.CPF)
            {
                var pessoaExistente = await _pessoasRepository.GetByCPFAsync(inputModel.CPF);
                if (pessoaExistente != null)
                {
                    return BadRequest("CPF já cadastrado");
                }
            }
            if (!string.IsNullOrEmpty(inputModel.CNPJ) && pessoa.CNPJ != inputModel.CNPJ)
            {
                var pessoaExistente = await _pessoasRepository.GetByCNPJAsync(inputModel.CNPJ);
                if (pessoaExistente != null)
                {
                    return BadRequest("CNPJ já cadastrado");
                }
            }
            if (!inputModel.Cliente && !inputModel.Fornecedor && !inputModel.Colaborador)
            {
                return BadRequest("É necessário informar pelo menos uma qualificação de Pessoa");
            }
            if (pessoa.Colaborador && !inputModel.Colaborador && pessoa.Departamentos.Any())
            {
                return BadRequest("Não é possível remover a Qualificação de Colaborador, visto que é Responsável por algum Departamento");
            }
            pessoa.Update(inputModel.Tipo, inputModel.Nome, inputModel.Apelido, inputModel.CPF, inputModel.CNPJ, inputModel.Cliente, inputModel.Fornecedor, inputModel.Colaborador);
            await _pessoasRepository.SaveChangesAsync();
            return Accepted();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var pessoa = await _pessoasRepository.GetByIdAsync(id);
            if (pessoa == null)
            {
                return BadRequest("Pessoa não encontrada");
            }
            if (pessoa.Departamentos.Any())
            {
                return BadRequest("Antes de remover a Pessoa, é necessário substituir o Reponsável pelos Departamentos de sua responsabilidade");
            }
            if (pessoa.Enderecos.Any())
            {
                return BadRequest("Antes de remover a Pessoa, é necessário remover os endereços primeiro");
            }
            await _pessoasRepository.DeleteAsync(pessoa);
            return Accepted();
        }
        
    }
}
