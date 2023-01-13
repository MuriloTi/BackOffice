using BackOffice.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BackOffice.Infrastructure.Persistence
{
    public class BackOfficeDbContext : DbContext
    {
        public BackOfficeDbContext(DbContextOptions<BackOfficeDbContext> options) : base(options)
        {

        }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
