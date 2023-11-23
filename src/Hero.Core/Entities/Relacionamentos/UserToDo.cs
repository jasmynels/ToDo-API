using Core.Entities.ToDos;
using Core.Entities.Usuarios;
using Shared.Core.Entities;

namespace Core.Entities.Relacionamentos
{
    public class UserToDo : BaseEntity
    {
        public Guid DesignadoId { get; set; }
        public Guid ToDoId { get; set; }
        public virtual Usuario Designado { get; set; }
        public virtual ToDo ToDo { get; set; }
    }
}
