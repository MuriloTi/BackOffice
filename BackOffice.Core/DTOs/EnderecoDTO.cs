namespace BackOffice.Core.DTOs
{
    public class EnderecoDTO
    {
        public EnderecoDTO(int id, int pessoaId, string cEP, string estado, string cidade, string bairro, string logradouro, string? numero, string? complemento, DateTime dataCriacao, DateTime? dataUltimaAlteracao, PessoaDTO? pessoa)
        {
            Id = id;
            PessoaId = pessoaId;
            CEP = cEP;
            Estado = estado;
            Cidade = cidade;
            Bairro = bairro;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            DataCriacao = dataCriacao;
            DataUltimaAlteracao = dataUltimaAlteracao;
            Pessoa = pessoa;
        }

        public int Id { get; private set; }
        public int PessoaId { get; private set; }
        public string CEP { get; private set; }
        public string Estado { get; private set; }
        public string Cidade { get; private set; }
        public string Bairro { get; private set; }
        public string Logradouro { get; private set; }
        public string? Numero { get; private set; }
        public string? Complemento { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime? DataUltimaAlteracao { get; private set; }

        public PessoaDTO? Pessoa { get; private set; }
    }
}
