using Core.Entities.Relacionamentos;
using Shared.Core.Entities;

namespace Core.Entities.Usuarios
{
    public class Usuario : BaseEntity
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public virtual ICollection<UserToDo> UserToDo { get; private set; }
    }
}
