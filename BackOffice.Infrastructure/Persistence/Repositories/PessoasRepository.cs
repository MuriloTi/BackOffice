using BackOffice.Core.DTOs;
using BackOffice.Core.Entities;
using BackOffice.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackOffice.Infrastructure.Persistence.Repositories
{
    public class PessoasRepository : IPessoasRepository
    {
        private readonly BackOfficeDbContext _backOfficeDbContext;

        public PessoasRepository(BackOfficeDbContext backOfficeDbContext)
        {
            _backOfficeDbContext = backOfficeDbContext;
        }

        public async Task AddAsync(Pessoa pessoa)
        {
            await _backOfficeDbContext.Pessoas.AddAsync(pessoa);
            await _backOfficeDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Pessoa pessoa)
        {
            _backOfficeDbContext.Pessoas.Remove(pessoa);
            await _backOfficeDbContext.SaveChangesAsync();
        }

        public async Task<List<PessoaDTO>> GetAllAsync(string query)
        {
            var pessoas = await _backOfficeDbContext.Pessoas
                .Where(p => p.Nome.Contains(query.ToUpper()) || p.Apelido.Contains(query.ToUpper()))
                .ToListAsync();
            var pessoasDTO = new List<PessoaDTO>();
            foreach (var pessoa in pessoas)
            {
                pessoasDTO.Add(GetDTO(pessoa));
            }
            return pessoasDTO;
        }

        public async Task<Pessoa> GetByCNPJAsync(string cnpj)
        {
            var pessoa = await _backOfficeDbContext.Pessoas
                .SingleOrDefaultAsync(p => p.CNPJ == cnpj);
            return pessoa;
        }

        public async Task<Pessoa> GetByCPFAsync(string cpf)
        {
            var pessoa = await _backOfficeDbContext.Pessoas
                .SingleOrDefaultAsync(p => p.CPF == cpf);
            return pessoa;
        }

        public async Task<Pessoa> GetByIdAsync(int id)
        {
            var pessoa = await _backOfficeDbContext.Pessoas
                .Include(p => p.Enderecos)
                .Include(p => p.Departamentos)
                .SingleOrDefaultAsync(p => p.Id == id);
            return pessoa;
        }

        public async Task<PessoaDTO> GetDTOByIdAsync(int id)
        {
            var pessoa = await _backOfficeDbContext.Pessoas
                .Include(p => p.Enderecos)
                .Include(p => p.Departamentos)
                .SingleOrDefaultAsync(p => p.Id == id);
            if (pessoa != null)
            {
                return GetDTO(pessoa);
            }
            return null;
        }

        public async Task SaveChangesAsync()
        {
            await _backOfficeDbContext.SaveChangesAsync();
        }

        private PessoaDTO GetDTO(Pessoa pessoa)
        {
            var enderecosDTO = new List<EnderecoDTO>();
            if (pessoa.Enderecos != null)
            {
                foreach (var endereco in pessoa.Enderecos)
                {
                    var enderecoDTO = new EnderecoDTO(endereco.Id, endereco.PessoaId, endereco.CEP, endereco.Estado, endereco.Cidade, endereco.Bairro, endereco.Logradouro, endereco.Numero, endereco.Complemento, endereco.DataCriacao, endereco.DataUltimaAlteracao, null);
                    enderecosDTO.Add(enderecoDTO);
                }
            }
            var departamentosDTO = new List<DepartamentoDTO>();
            if (pessoa.Departamentos != null)
            {
                foreach (var departamento in pessoa.Departamentos)
                {
                    var departamentoDTO = new DepartamentoDTO(departamento.Id, departamento.Titulo, departamento.ResponsavelId, departamento.DataCriacao, departamento.DataUltimaAlteracao, pessoa.Nome);
                    departamentosDTO.Add(departamentoDTO);
                }
            }
            return new PessoaDTO(pessoa.Id, pessoa.Tipo, pessoa.Nome, pessoa.Apelido, pessoa.CPF, pessoa.CNPJ, pessoa.Cliente, pessoa.Fornecedor, pessoa.Colaborador, pessoa.DataCriacao, pessoa.DataUltimaAlteracao, enderecosDTO, departamentosDTO);
        }
    }
}
