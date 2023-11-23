using Core.Entities.Relacionamentos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Configurations.Relacionamentos
{
    public class UserToDoModelConfiguration : IEntityTypeConfiguration<UserToDo>
    {
        public void Configure(EntityTypeBuilder<UserToDo> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("usertodo");

            entityTypeBuilder.Property(e => e.DesignadoId)
                .IsRequired()
                .IsUnicode(true);

            entityTypeBuilder.Property(e => e.ToDoId)
                .IsRequired()
                .IsUnicode(true);

            entityTypeBuilder.HasOne(e => e.ToDo)
                .WithMany(e => e.UserToDo)
                .HasForeignKey(e => e.ToDoId);

            entityTypeBuilder.HasOne(e => e.Designado)
                .WithMany(e => e.UserToDo)
                .HasForeignKey(e => e.DesignadoId);
        }
    }
}