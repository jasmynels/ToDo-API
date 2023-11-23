using Core.Entities.Relacionamentos;
using Core.Entities.ToDos;

namespace Core.Interfaces.Repositories.Relacionamentos
{
    public interface IUserToDoRepository : IBaseRepository<UserToDo>
    {
        Task<IEnumerable<ToDo>> GetByUser(Guid userId);
    }
}
