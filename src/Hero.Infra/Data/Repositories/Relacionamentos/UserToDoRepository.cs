using Core.Entities.Relacionamentos;
using Core.Entities.ToDos;
using Core.Interfaces.Repositories.Relacionamentos;
using Microsoft.EntityFrameworkCore;
using Shared.Infra;

namespace Infra.Data.Repositories.Relacionamentos
{
    public class UserToDoRepository : BaseRepository<UserToDo>, IUserToDoRepository
    {
        private readonly AppDbContext dbContext;

        public UserToDoRepository(AppDbContext dbContext) : base(dbContext)
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

        public async Task<IEnumerable<ToDo>> GetByUser(Guid userId)
        {
            var queryToDo = await dbContext.UserToDo
                 .Where(e => !e.Deletado && e.DesignadoId.Equals(userId))
                 .Select(e => e.ToDoId)
                .ToListAsync();

            var listTodo = new List<ToDo>();

            foreach (var item in queryToDo)
            {
                var todos = await dbContext.ToDo
                .Where(e => e.Id == item)
                .FirstOrDefaultAsync();

                listTodo.Add(todos);
            }

            return listTodo;
        }
    }
}