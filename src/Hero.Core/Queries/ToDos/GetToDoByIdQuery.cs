using Core.Models.Responses;
using MediatR;
using Shared.Core;
using Shared.Core.Models;

namespace Core.Queries.ToDos
{
    public class GetToDoByIdQuery : BaseRequestFilter, IRequest<Result<ToDoResponse>>
    {
        public GetToDoByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
