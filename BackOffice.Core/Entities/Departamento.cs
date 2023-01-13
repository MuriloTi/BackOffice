namespace BackOffice.Core.Entities
{
    public class Departamento
    {
        public Departamento(string titulo, int responsavelId)
        {
            Titulo = titulo;
            ResponsavelId = responsavelId;
            DataCriacao = DateTime.Now;
        }

        public int Id { get; private set; }
        public string Titulo { get; private set; }
        public int ResponsavelId { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime? DataUltimaAlteracao { get; private set; }

        public virtual Pessoa Responsavel { get; set; }

        public void Update(string titulo, int responsavelId)
        {
            Titulo = titulo;
            ResponsavelId = responsavelId;
            DataUltimaAlteracao = DateTime.Now;
        }
    }
}
