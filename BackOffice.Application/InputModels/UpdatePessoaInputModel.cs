using BackOffice.Application.Validators;
using BackOffice.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace BackOffice.Application.InputModels
{
    public class UpdatePessoaInputModel
    {
        [Range(1, 2, ErrorMessage = "Tipo deve ser 1 para Pessoa Física ou 2 para Pessoa Jurídica")]
        public TipoPessoaEnum Tipo { get; set; }

        [Required(ErrorMessage = "Informe um Nome/Razao Social")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe um Apelido/Nome Fantasia")]
        public string Apelido { get; set; }

        [CPFValidator(ErrorMessage = "Invalid CPF. CPF must have 11 only numbers exacly")]
        public string? CPF { get; set; }

        [CNPJValidator(ErrorMessage = "Invalid CNPJ. CNPJ must have 14 only numbers exacly")]
        public string? CNPJ { get; set; }

        public bool Cliente { get; set; }
        public bool Fornecedor { get; set; }
        public bool Colaborador { get; set; }
    }
}
