using BackOffice.Application.InputModels;
using BackOffice.Core.Entities;
using BackOffice.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BackOffice.API.Controllers
{
    [Route("api/departamentos")]
    public class DepartamentosController : Controller
    {
        private readonly IPessoasRepository _pessoasRepository;
        private readonly IDepartamentosRepository _departamentosRepository;

        public DepartamentosController(IPessoasRepository pessoasRepository, IDepartamentosRepository departamentosRepository)
        {
            _pessoasRepository = pessoasRepository;
            _departamentosRepository = departamentosRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string? query)
        {
            query ??= "";
            query = query.Trim();
            var departamentos = await _departamentosRepository.GetAllAsync(query);
            return Ok(departamentos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var departamento = await _departamentosRepository.GetDTOByIdAsync(id);
            if (departamento == null)
            {
                return BadRequest("Departamento não encontrado");
            }
            return Ok(departamento);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateDepartamentoInputModel inputModel)
        {
            var pessoa = await _pessoasRepository.GetByIdAsync(inputModel.ResponsavelId);
            if (pessoa == null)
            {
                return BadRequest("Colaborador(a) não encontrado(a)");
            }
            if (!pessoa.Colaborador)
            {
                return BadRequest("A Pessoa informada não é Colaboradora");
            }
            var departamentoExistente = await _departamentosRepository.GetByTituloAsync(inputModel.Titulo.Trim().ToUpper());
            if (departamentoExistente.Any())
            {
                return BadRequest("Departamento já existente");
            }
            var departamento = new Departamento(inputModel.Titulo.Trim().ToUpper(), inputModel.ResponsavelId);
            await _departamentosRepository.AddAsync(departamento);
            return CreatedAtAction(nameof(GetById), new { departamento.Id }, inputModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateDepartamentoInputModel inputModel)
        {
            var departamento = await _departamentosRepository.GetByIdAsync(id);
            if (departamento == null)
            {
                return BadRequest("Departamento não encontrado");
            }
            if (departamento.ResponsavelId != inputModel.ResponsavelId)
            {
                var pessoa = await _pessoasRepository.GetByIdAsync(inputModel.ResponsavelId);
                if (pessoa == null)
                {
                    return BadRequest("Colaborador(a) não encontrado(a)");
                }
                if (!pessoa.Colaborador)
                {
                    return BadRequest("A Pessoa informada não é Colaboradora");
                }
            }
            if (departamento.Titulo != inputModel.Titulo.Trim().ToUpper())
            {
                var departamentoExistente = await _departamentosRepository.GetByTituloAsync(inputModel.Titulo.Trim().ToUpper());
                if (departamentoExistente.Any())
                {
                    return BadRequest("Departamento já existente");
                }
            }
            departamento.Update(inputModel.Titulo.Trim().ToUpper(), inputModel.ResponsavelId);
            await _departamentosRepository.SaveChangesAsync();
            return Accepted();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var departamento = await _departamentosRepository.GetByIdAsync(id);
            if (departamento == null)
            {
                return BadRequest("Departamento não encontrado");
            }
            await _departamentosRepository.DeleteAsync(departamento);
            return Accepted();
        }

    }
}
