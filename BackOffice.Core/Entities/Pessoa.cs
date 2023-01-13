using BackOffice.Core.Enums;

namespace BackOffice.Core.Entities
{
    public class Pessoa
    {
        public Pessoa(TipoPessoaEnum tipo, string nome, string apelido, string? cPF, string? cNPJ, bool cliente, bool fornecedor, bool colaborador)
        {
            Tipo = tipo;
            Nome = nome;
            Apelido = apelido;
            CPF = cPF;
            CNPJ = cNPJ;
            Cliente = cliente;
            Fornecedor = fornecedor;
            Colaborador = colaborador;
            DataCriacao = DateTime.Now;
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

        public virtual List<Endereco> Enderecos { get; set; }
        public virtual List<Departamento> Departamentos { get; set; }

        public void Update(TipoPessoaEnum tipo, string nome, string apelido, string? cPF, string? cNPJ, bool cliente, bool fornecedor, bool colaborador)
        {
            Tipo = tipo;
            Nome = nome;
            Apelido = apelido;
            CPF = cPF;
            CNPJ = cNPJ;
            Cliente = cliente;
            Fornecedor = fornecedor;
            Colaborador = colaborador;
            DataUltimaAlteracao = DateTime.Now;
        }
    }
}
