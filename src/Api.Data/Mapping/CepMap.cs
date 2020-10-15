using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class CepMap : IEntityTypeConfiguration<CepEntity>
    {
        public void Configure(EntityTypeBuilder<CepEntity> builder)
        {
            // Define o nome da tabela
            builder.ToTable("Cep");
            // Define a chave primária
            builder.HasKey(u => u.Id);
            // Define um indice
            builder.HasIndex(u => u.Cep);
            // Configuração da FK
            // Um CEP HasOne ou TemUma Municipio
            // Um Municipio WithMany ou ComVarios Ceps
            builder.HasOne(c => c.Municipio).WithMany(m => m.Ceps);
        }
    }
}
