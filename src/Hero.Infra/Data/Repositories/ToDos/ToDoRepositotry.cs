using Core.Entities.ToDos;
using Core.Interfaces.Repositories.ToDos;
using Microsoft.EntityFrameworkCore;
using Shared.Infra;

namespace Infra.Data.Repositories.ToDos
{
    public class ToDoRepositotry : BaseRepository<ToDo>, IToDoRepository
    {
        private readonly AppDbContext dbContext;

        public ToDoRepositotry(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<AsyncOutResult<IEnumerable<ToDo>, int>> Get(string nome, int? take, int? offSet, string sortingProp, bool? asc)
        {
            var query = dbContext.ToDo
                .Where(e => !e.Deletado)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(nome))
                query = query.Where(e => e.Nome.ToLower().StartsWith(nome.ToLower()));

            if (!string.IsNullOrEmpty(sortingProp) && asc != null
                && DataHelpers.CheckExistingProperty<ToDo>(sortingProp))
                query = query.OrderByDynamic(sortingProp, (bool)asc);


            return new AsyncOutResult<IEnumerable<ToDo>, int>(await query.ToListAsync(), await query.CountAsync());
        }
    }
}