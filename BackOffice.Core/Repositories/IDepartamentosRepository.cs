using BackOffice.Core.DTOs;
using BackOffice.Core.Entities;

namespace BackOffice.Core.Repositories
{
    public interface IDepartamentosRepository
    {
        Task<List<DepartamentoDTO>> GetAllAsync(string query);
        Task<DepartamentoDTO> GetDTOByIdAsync(int id);
        Task<Departamento> GetByIdAsync(int id);
        Task<List<Departamento>> GetByTituloAsync(string titulo);
        Task AddAsync(Departamento departamento);
        Task DeleteAsync(Departamento departamento);
        Task SaveChangesAsync();
    }
}
