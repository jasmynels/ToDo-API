using AutoMapper;
using Core.Interfaces.Repositories.Relacionamentos;
using Core.Interfaces.Repositories.ToDos;
using Core.Models.Responses;
using MediatR;
using Shared.Core;

namespace Core.Queries.ToDos.Handler
{
    public class GetToDoByUsuarioQueryHandler : IRequestHandler<GetToDoByUsuarioQuery, Result<IEnumerable<ToDoResponse>>>
    {
        private readonly IToDoRepository _toDoRepository;
        private readonly IUserToDoRepository _userToDoRepository;
        private readonly IMapper _mapper;

        public GetToDoByUsuarioQueryHandler(IToDoRepository toDoRepository, IMapper mapper, IUserToDoRepository userToDoRepository)
        {
            _toDoRepository = toDoRepository;
            _mapper = mapper;
            _userToDoRepository = userToDoRepository;
        }

        public async Task<Result<IEnumerable<ToDoResponse>>> Handle(GetToDoByUsuarioQuery request, CancellationToken cancellationToken)
        {
            var result = new Result<IEnumerable<ToDoResponse>>();
            var todos = await _userToDoRepository.GetByUser(request.Id);

            if (todos == null)
            {
                result.WithError("Erro ao encontrar tarefa(s)");
                return result;
            }

            result.Value = _mapper.Map<IEnumerable<ToDoResponse>>(todos);
            result.Count = todos.Count();

            return result;
        }
    }
}
