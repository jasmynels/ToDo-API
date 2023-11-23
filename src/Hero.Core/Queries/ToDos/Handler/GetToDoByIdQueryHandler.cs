using AutoMapper;
using Core.Interfaces.Repositories.ToDos;
using Core.Models.Responses;
using MediatR;
using Shared.Core;

namespace Core.Queries.ToDos.Handler
{
    public class GetToDoByIdQueryHandler : IRequestHandler<GetToDoByIdQuery, Result<ToDoResponse>>
    {
        private readonly IToDoRepository _toDoRepository;
        private readonly IMapper _mapper;

        public GetToDoByIdQueryHandler(IToDoRepository toDoRepository, IMapper mapper)
        {
            _toDoRepository = toDoRepository;
            _mapper = mapper;
        }

        public async Task<Result<ToDoResponse>> Handle(GetToDoByIdQuery request, CancellationToken cancellationToken)
        {
            var result = new Result<ToDoResponse>();
            var todo = await _toDoRepository.GetById(request.Id);

            if (todo == null)
            {
                result.WithError("Erro ao encontrar tarefa");
                return result;
            }

            result.Value = _mapper.Map<ToDoResponse>(todo);

            return result;
        }
    }
}
