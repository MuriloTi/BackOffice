using System.ComponentModel.DataAnnotations;

namespace BackOffice.Application.InputModels
{
    public class CreateDepartamentoInputModel
    {
        [Required(ErrorMessage = "Informe um Titulo")]
        public string Titulo { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "ResponsavelId deve ser um número maior que 0")]
        public int ResponsavelId { get; set; }
    }
}
