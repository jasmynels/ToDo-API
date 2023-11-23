using Core.Entities.Usuarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Configurations.Usuarios
{
    public class UsuarioModelConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("usuarios");

            entityTypeBuilder.Property(e => e.Nome)
                .IsRequired()
                .IsUnicode(true);

            entityTypeBuilder.Property(e => e.Email)
               .IsRequired()
               .IsUnicode(true);

            entityTypeBuilder.Property(e => e.Senha)
               .IsRequired()
               .IsUnicode(true);
        }
    }
}