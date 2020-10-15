using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class UfMap : IEntityTypeConfiguration<UfEntity>
    {
        public void Configure(EntityTypeBuilder<UfEntity> builder)
        {
            // Define o nome da tabela
            builder.ToTable("Uf");
            // Define a chave primária
            builder.HasKey(u => u.Id);
            // Define um indice e único
            builder.HasIndex(u => u.Sigla).IsUnique();
        }
    }
}
