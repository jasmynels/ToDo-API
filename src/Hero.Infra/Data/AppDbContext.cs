using Core.Entities.Relacionamentos;
using Core.Entities.ToDos;
using Core.Entities.Usuarios;
using Infra.Data.Configurations.Relacionamentos;
using Infra.Data.Configurations.ToDos;
using Infra.Data.Configurations.Usuarios;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Data
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<ToDo> ToDo { get; set; }
        public virtual DbSet<UserToDo> UserToDo { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("public");

            modelBuilder.ApplyConfiguration(new ToDoModelConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioModelConfiguration());
            modelBuilder.ApplyConfiguration(new UserToDoModelConfiguration());

            foreach (var property in modelBuilder.Model.GetEntityTypes()
                 .SelectMany(t => t.GetProperties())
                 .Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?)))
            {
                property.SetColumnType("timestamp without time zone");
            }
        }

        public static void InitializeDatabase(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
            serviceScope?.ServiceProvider.GetRequiredService<AppDbContext>().Database.Migrate();
        }
    }
}