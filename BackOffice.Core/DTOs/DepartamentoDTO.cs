namespace BackOffice.Core.DTOs
{
    public class DepartamentoDTO
    {
        public DepartamentoDTO(int id, string titulo, int responsavelId, DateTime dataCriacao, DateTime? dataUltimaAlteracao, string nomeResponsavel)
        {
            Id = id;
            Titulo = titulo;
            ResponsavelId = responsavelId;
            DataCriacao = dataCriacao;
            DataUltimaAlteracao = dataUltimaAlteracao;
            NomeResponsavel = nomeResponsavel;
        }

        public int Id { get; private set; }
        public string Titulo { get; private set; }
        public int ResponsavelId { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime? DataUltimaAlteracao { get; private set; }

        public string NomeResponsavel { get; private set; }
    }
}
