using Core.Entities.Usuarios;
using Core.Enums;
using Shared.Core.Models;

namespace Core.Models.Responses
{
    public class ToDoResponse : BaseResponse
    {
        public string Nome { get; private set; }
        public string? Descricao { get; private set; }
        public EnumStatus Status { get; private set; }
        public virtual Usuario? Designado { get; private set; }
    }
}
