using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Entities
{
    public class CepEntity : BaseEntity
    {
        [Required]
        [MaxLength(10)]
        public string Cep { get; set; }

        [Required]
        [MaxLength(60)]
        public string Logradouro { get; set; }

        [MaxLength(10)]
        public string Numero { get; set; }

        // Campo FK - Chave Estrangeira
        // Padrão adotado pelo uso do EF
        // Nome da Entidade + Campo Key = MunicipioId
        [Required]
        public Guid MunicipioId { get; set; }

        // Campo para ligação das entidades
        // Todo Cep possui um Municipio
        public MunicipioEntity Municipio { get; set; }
    }
}
