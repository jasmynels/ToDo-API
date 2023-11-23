using Core.Entities.Relacionamentos;
using Core.Enums;
using Shared.Core.Entities;

namespace Core.Entities.ToDos
{
    public class ToDo : BaseEntity
    {
        public string Nome { get; private set; }
        public string? Descricao { get; private set; }
        public EnumStatus Status { get; private set; }
        public virtual ICollection<UserToDo> UserToDo { get; private set; }
    }
}
