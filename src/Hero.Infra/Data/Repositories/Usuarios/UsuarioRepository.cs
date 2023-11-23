using Core.Entities.Usuarios;
using Core.Interfaces.Repositories.Usuarios;
using Microsoft.EntityFrameworkCore;
using Shared.Infra;

namespace Infra.Data.Repositories.Usuarios
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        private readonly AppDbContext dbContext;

        public UsuarioRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<AsyncOutResult<IEnumerable<Usuario>, int>> Get(string nome, int? take, int? offSet, string sortingProp, bool? asc)
        {
            var query = dbContext.Usuario
                .Where(e => !e.Deletado)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(nome))
                query = query.Where(e => e.Nome.ToLower().StartsWith(nome.ToLower()));

            if (!string.IsNullOrEmpty(sortingProp) && asc != null
                && DataHelpers.CheckExistingProperty<Usuario>(sortingProp))
                query = query.OrderByDynamic(sortingProp, (bool)asc);



            return new AsyncOutResult<IEnumerable<Usuario>, int>(await query.ToListAsync(), await query.CountAsync());
        }

        public bool EmailExiste(string email)
        {
            return dbContext.Usuario.Any(e => e.Email == email);
        }

        public Usuario GetUserByEmail(string email)
        {
            return dbContext.Usuario.Where(e => e.Email == email).First();
        }
    }
}