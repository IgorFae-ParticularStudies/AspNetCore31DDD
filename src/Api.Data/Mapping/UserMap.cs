using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            // Define o nome da tabela
            builder.ToTable("User");
            // Define a chave primária
            builder.HasKey(u => u.Id);
            // Define um indice e único
            builder.HasIndex(u => u.Email).IsUnique();
            // Campo Name com tamanho máximo de 60 e not null    
            builder.Property(u => u.Name).HasMaxLength(60).IsRequired();
            // Campo Email com tamanho máximo de 100
            builder.Property(u => u.Email).HasMaxLength(100);

        }
    }
}
