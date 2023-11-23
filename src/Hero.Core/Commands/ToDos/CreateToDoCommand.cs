using Core.Enums;
using Core.Models.Responses;
using MediatR;
using Shared.Core;

namespace Core.Commands.ToDos
{
    public class CreateToDoCommand : IRequest<Result<ToDoResponse>>
    {
        public string Nome { get; set; }
        public string? Descricao { get; set; }
        public EnumStatus Status { get; set; }
        public Guid DesignadoId { get; set; }
    }
}
