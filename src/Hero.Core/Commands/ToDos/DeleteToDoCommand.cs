using Core.Models.Responses;
using MediatR;
using Shared.Core;

namespace Core.Commands.ToDos
{
    public class DeleteToDoCommand : IRequest<Result<ToDoResponse>>
    {
        public Guid Id { get; set; }
        public DeleteToDoCommand(Guid id)
        {
            Id = id;
        }

    }
}
