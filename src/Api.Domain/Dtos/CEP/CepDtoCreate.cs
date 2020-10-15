using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.CEP
{
    public class CepDtoCreate
    {
        [Required(ErrorMessage = "CEP é campo obrigatório.")]
        public string  Cep { get; set; }

        [Required(ErrorMessage = "Logradouro é campo obrigatório.")]
        public string Logradouro { get; set; }
        public string  Numero { get; set; }

        [Required(ErrorMessage = "Município é campo obrigatório.")]
        public Guid MunicipioId { get; set; }
    }
}
