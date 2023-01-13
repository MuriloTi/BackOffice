using BackOffice.Core.DTOs;
using BackOffice.Core.Entities;

namespace BackOffice.Core.Repositories
{
    public interface IEnderecosRepository
    {
        Task<EnderecoDTO> GetDTOByIdAsync(int id);
        Task<Endereco> GetByIdAsync(int id);
        Task AddAsync(Endereco endereco);
        Task DeleteAsync(Endereco endereco);
        Task SaveChangesAsync();
    }
}
