﻿// <auto-generated />
using System;
using Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infra.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Core.Entities.Relacionamentos.UserToDo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Ativo")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Deletado")
                        .HasColumnType("boolean");

                    b.Property<Guid>("DesignadoId")
                        .IsUnicode(true)
                        .HasColumnType("uuid");

                    b.Property<Guid>("ToDoId")
                        .IsUnicode(true)
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("DesignadoId");

                    b.HasIndex("ToDoId");

                    b.ToTable("usertodo", "public");
                });

            modelBuilder.Entity("Core.Entities.ToDos.ToDo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Ativo")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Deletado")
                        .HasColumnType("boolean");

                    b.Property<string>("Descricao")
                        .IsUnicode(true)
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .IsUnicode(true)
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("todo", "public");
                });

            modelBuilder.Entity("Core.Entities.Usuarios.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Ativo")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Deletado")
                        .HasColumnType("boolean");

                    b.Property<string>("Email")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("text");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("usuarios", "public");
                });

            modelBuilder.Entity("Core.Entities.Relacionamentos.UserToDo", b =>
                {
                    b.HasOne("Core.Entities.Usuarios.Usuario", "Designado")
                        .WithMany("UserToDo")
                        .HasForeignKey("DesignadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.ToDos.ToDo", "ToDo")
                        .WithMany("UserToDo")
                        .HasForeignKey("ToDoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Designado");

                    b.Navigation("ToDo");
                });

            modelBuilder.Entity("Core.Entities.ToDos.ToDo", b =>
                {
                    b.Navigation("UserToDo");
                });

            modelBuilder.Entity("Core.Entities.Usuarios.Usuario", b =>
                {
                    b.Navigation("UserToDo");
                });
#pragma warning restore 612, 618
        }
    }
}
