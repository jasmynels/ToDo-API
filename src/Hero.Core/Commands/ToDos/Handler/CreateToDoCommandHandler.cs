using AutoMapper;
using Core.Entities.Relacionamentos;
using Core.Entities.ToDos;
using Core.Interfaces;
using Core.Interfaces.Repositories.Relacionamentos;
using Core.Interfaces.Repositories.ToDos;
using Core.Models.Responses;
using MediatR;
using Shared.Core;

namespace Core.Commands.ToDos.Handler
{
    public class CreateToDoCommandHandler : IRequestHandler<CreateToDoCommand, Result<ToDoResponse>>
    {
        private readonly IToDoRepository _toDoRepository;
        private readonly IUserToDoRepository _userToDoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateToDoCommandHandler(IToDoRepository toDoRepository, IUnitOfWork unitOfWork, IMapper mapper, IUserToDoRepository userToDoRepository)
        {
            _toDoRepository = toDoRepository;
            _userToDoRepository = userToDoRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<ToDoResponse>> Handle(CreateToDoCommand request, CancellationToken cancellationToken)
        {
            var result = new Result<ToDoResponse>();
            var todo = _mapper.Map<ToDo>(request);

            try
            {
                _unitOfWork.OpenTransaction();
                await _toDoRepository.AddAsync(todo);

                UserToDo userTodo = new()
                {
                    ToDoId = todo.Id,
                    DesignadoId = request.DesignadoId
                };

                UserToDo? entityUserToDo = _mapper.Map<UserToDo>(userTodo);

                await _userToDoRepository.AddAsync(entityUserToDo);

                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                result.WithException("Erro ao criar Tarefa - " + ex);
                return result;
            }

            result.Value = _mapper.Map<ToDoResponse>(todo);
            return result;
        }
    }
}