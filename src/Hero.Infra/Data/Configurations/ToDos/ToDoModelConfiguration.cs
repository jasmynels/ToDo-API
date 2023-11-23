using Core.Entities.ToDos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Configurations.ToDos
{
    public class ToDoModelConfiguration : IEntityTypeConfiguration<ToDo>
    {
        public void Configure(EntityTypeBuilder<ToDo> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("todo");

            entityTypeBuilder.Property(e => e.Nome)
                .IsRequired()
                .IsUnicode(true);

            entityTypeBuilder.Property(e => e.Descricao)
                .IsUnicode(true);

            entityTypeBuilder.Property(e => e.Status)
               .IsRequired()
               .IsUnicode(true);
        }
    }
}