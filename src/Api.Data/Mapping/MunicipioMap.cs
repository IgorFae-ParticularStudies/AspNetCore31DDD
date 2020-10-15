using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class MunicipioMap : IEntityTypeConfiguration<MunicipioEntity>
    {
        public void Configure(EntityTypeBuilder<MunicipioEntity> builder)
        {
            // Define o nome da tabela
            builder.ToTable("Municipio");
            // Define a chave primária
            builder.HasKey(u => u.Id);
            // Define um indice
            builder.HasIndex(u => u.CodIBGE);

            // Configuração da FK
            // Um municipio HasOne ou TemUma UF
            // Uma Uf WithMany ou ComVarios Municipios
            builder.HasOne(m => m.Uf).WithMany(u => u.Municipios);
        }
    }
}
