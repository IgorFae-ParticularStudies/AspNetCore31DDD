using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Entities
{
    public class UfEntity : BaseEntity
    {
        [Required]
        [MaxLength(2)]
        public string Sigla { get; set; }

        [Required]
        [MaxLength(45)]
        public string Nome { get; set; }

        // Campo para ligação das entidades
        // Todo Uf possui muitos municipios
        public IEnumerable<MunicipioEntity> Municipios { get; set; }
    }
}
