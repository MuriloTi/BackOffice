using BackOffice.Core.DTOs;
using BackOffice.Core.Entities;
using BackOffice.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackOffice.Infrastructure.Persistence.Repositories
{
    public class DepartamentosRepository : IDepartamentosRepository
    {
        private readonly BackOfficeDbContext _backOfficeDbContext;

        public DepartamentosRepository(BackOfficeDbContext backOfficeDbContext)
        {
            _backOfficeDbContext = backOfficeDbContext;
        }

        public async Task AddAsync(Departamento departamento)
        {
            await _backOfficeDbContext.Departamentos.AddAsync(departamento);
            await _backOfficeDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Departamento departamento)
        {
            _backOfficeDbContext.Departamentos.Remove(departamento);
            await _backOfficeDbContext.SaveChangesAsync();
        }

        public async Task<List<DepartamentoDTO>> GetAllAsync(string query)
        {
            var departamentos = await _backOfficeDbContext.Departamentos
                .Include(d => d.Responsavel)
                .Where(d => d.Titulo.Contains(query))
                .ToListAsync();
            var departamentosDTO = new List<DepartamentoDTO>();
            foreach (var departamento in departamentos)
            {
                departamentosDTO.Add(GetDTO(departamento));
            }
            return departamentosDTO;
        }

        public async Task<Departamento> GetByIdAsync(int id)
        {
            var departamento = await _backOfficeDbContext.Departamentos
                .Include(d => d.Responsavel)
                .SingleOrDefaultAsync(d => d.Id == id);
            return departamento;
        }

        public async Task<List<Departamento>> GetByTituloAsync(string titulo)
        {
            var departamentos = await _backOfficeDbContext.Departamentos
                .Where(d => d.Titulo.Equals(titulo))
                .ToListAsync();
            return departamentos;
        }

        public async Task<DepartamentoDTO> GetDTOByIdAsync(int id)
        {
            var departamento = await _backOfficeDbContext.Departamentos
                .Include(d => d.Responsavel)
                .SingleOrDefaultAsync(d => d.Id == id);
            if (departamento != null)
            {
                return GetDTO(departamento);
            }
            return null;
        }

        public async Task SaveChangesAsync()
        {
            await _backOfficeDbContext.SaveChangesAsync();
        }

        private DepartamentoDTO GetDTO(Departamento departamento)
        {
            return new DepartamentoDTO(departamento.Id, departamento.Titulo, departamento.ResponsavelId, departamento.DataCriacao, departamento.DataUltimaAlteracao, departamento.Responsavel.Nome);
        }
    }
}
