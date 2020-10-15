using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Entities
{
    public class MunicipioEntity : BaseEntity
    {
        [Required]
        [MaxLength(60)]
        public string Nome { get; set; }
        public int CodIBGE { get; set; }

        // Campo FK - Chave Estrangeira
        // Padrão adotado pelo uso do EF
        // Nome da Entidade + Campo Key = UfId
        [Required]
        public Guid UfId { get; set; }

        // Campo para ligação das entidades
        // Todo municipio possui uma Uf
        public UfEntity Uf { get; set; }

        // Campo para ligação das entidades
        // Todo Municipio possui muitos Ceps
        public IEnumerable<CepEntity> Ceps { get; set; }
    }
}
