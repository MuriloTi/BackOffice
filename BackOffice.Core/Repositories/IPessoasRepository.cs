using BackOffice.Core.DTOs;
using BackOffice.Core.Entities;

namespace BackOffice.Core.Repositories
{
    public interface IPessoasRepository
    {
        Task<List<PessoaDTO>> GetAllAsync(string query);
        Task<PessoaDTO> GetDTOByIdAsync(int id);
        Task<Pessoa> GetByIdAsync(int id);
        Task<Pessoa> GetByCPFAsync(string cpf);
        Task<Pessoa> GetByCNPJAsync(string cnpj);
        Task AddAsync(Pessoa pessoa);
        Task DeleteAsync(Pessoa pessoa);
        Task SaveChangesAsync();
    }
}
