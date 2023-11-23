using Core.Models.Requests;
using Core.Models.Responses;
using MediatR;
using Shared.Core;

namespace Core.Commands.ToDos
{
    public class UpdateToDoCommand : IRequest<Result<ToDoResponse>>
    {
        public Guid Id { get; set; }
        public UpdateToDoRequest Request;
        public UpdateToDoCommand(Guid id, UpdateToDoRequest request)
        {
            Id = id;
            Request = request;
        }
    }
}
