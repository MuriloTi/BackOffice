using BackOffice.Core.DTOs;
using BackOffice.Core.Entities;
using BackOffice.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackOffice.Infrastructure.Persistence.Repositories
{
    public class EnderecosRepository : IEnderecosRepository
    {
        private readonly BackOfficeDbContext _backOfficeDbContext;

        public EnderecosRepository(BackOfficeDbContext backOfficeDbContext)
        {
            _backOfficeDbContext = backOfficeDbContext;
        }

        public async Task AddAsync(Endereco endereco)
        {
            await _backOfficeDbContext.Enderecos.AddAsync(endereco);
            await _backOfficeDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Endereco endereco)
        {
            _backOfficeDbContext.Enderecos.Remove(endereco);
            await _backOfficeDbContext.SaveChangesAsync();
        }

        public async Task<Endereco> GetByIdAsync(int id)
        {
            var endereco = await _backOfficeDbContext.Enderecos
                .SingleOrDefaultAsync(e => e.Id == id);
            return endereco;
        }

        public async Task<EnderecoDTO> GetDTOByIdAsync(int id)
        {
            var endereco = await _backOfficeDbContext.Enderecos
                .Include(e => e.Pessoa)
                .SingleOrDefaultAsync(e => e.Id == id);
            if (endereco != null)
            {
                return GetDTO(endereco);
            }
            return null;
        }

        public async Task SaveChangesAsync()
        {
            await _backOfficeDbContext.SaveChangesAsync();
        }

        private EnderecoDTO GetDTO(Endereco endereco)
        {
            var pessoaDTO = new PessoaDTO(endereco.Pessoa.Id, endereco.Pessoa.Tipo, endereco.Pessoa.Nome, endereco.Pessoa.Apelido, endereco.Pessoa.CPF, endereco.Pessoa.CNPJ, endereco.Pessoa.Cliente, endereco.Pessoa.Fornecedor, endereco.Pessoa.Colaborador, endereco.Pessoa.DataCriacao, endereco.Pessoa.DataUltimaAlteracao, null, null);
            var enderecoDTO = new EnderecoDTO(endereco.Id, endereco.PessoaId, endereco.CEP, endereco.Estado, endereco.Cidade, endereco.Bairro, endereco.Logradouro, endereco.Numero, endereco.Complemento, endereco.DataCriacao, endereco.DataUltimaAlteracao, pessoaDTO);
            return enderecoDTO;
        }
    }
}
