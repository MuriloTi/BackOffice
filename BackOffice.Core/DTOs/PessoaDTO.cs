using BackOffice.Core.Enums;

namespace BackOffice.Core.DTOs
{
    public class PessoaDTO
    {
        public PessoaDTO(int id, TipoPessoaEnum tipo, string nome, string apelido, string? cPF, string? cNPJ, bool cliente, bool fornecedor, bool colaborador, DateTime dataCriacao, DateTime? dataUltimaAlteracao, List<EnderecoDTO>? enderecos, List<DepartamentoDTO>? departamentos)
        {
            Id = id;
            Tipo = tipo;
            Nome = nome;
            Apelido = apelido;
            CPF = cPF;
            CNPJ = cNPJ;
            Cliente = cliente;
            Fornecedor = fornecedor;
            Colaborador = colaborador;
            DataCriacao = dataCriacao;
            DataUltimaAlteracao = dataUltimaAlteracao;
            Enderecos = enderecos;
            Departamentos = departamentos;
        }

        public int Id { get; private set; }
        public TipoPessoaEnum Tipo { get; private set; }
        public string Nome { get; private set; }
        public string Apelido { get; private set; }
        public string? CPF { get; private set; }
        public string? CNPJ { get; private set; }
        public bool Cliente { get; private set; }
        public bool Fornecedor { get; private set; }
        public bool Colaborador { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime? DataUltimaAlteracao { get; private set; }

        public List<EnderecoDTO>? Enderecos { get; private set; }
        public List<DepartamentoDTO>? Departamentos { get; private set; }
    }
}
