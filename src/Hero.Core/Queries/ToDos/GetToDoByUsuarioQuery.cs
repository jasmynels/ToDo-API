using Core.Models.Responses;
using MediatR;
using Shared.Core;
using Shared.Core.Models;

namespace Core.Queries.ToDos
{
    public class GetToDoByUsuarioQuery : BaseRequestFilter, IRequest<Result<IEnumerable<ToDoResponse>>>
    {
        public Guid Id { get; set; }

        public GetToDoByUsuarioQuery(Guid id)
        {
            Id = id;
        }
    }
}
