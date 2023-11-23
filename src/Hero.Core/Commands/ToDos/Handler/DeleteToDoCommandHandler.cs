using AutoMapper;
using Core.Interfaces;
using Core.Interfaces.Repositories.ToDos;
using Core.Models.Responses;
using MediatR;
using Shared.Core;

namespace Core.Commands.ToDos.Handler
{
    public class DeleteToDoCommandHandler : IRequestHandler<DeleteToDoCommand, Result<ToDoResponse>>
    {
        private readonly IToDoRepository _toDoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteToDoCommandHandler(IToDoRepository toDoRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _toDoRepository = toDoRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<ToDoResponse>> Handle(DeleteToDoCommand request, CancellationToken cancellationToken)
        {
            var result = new Result<ToDoResponse>();
            var todoExiste = await _toDoRepository.GetById(request.Id);

            if (todoExiste == null)
            {
                result.WithError("Erro ao encontrar tarefa.");
                return result;
            }

            try
            {
                _unitOfWork.OpenTransaction();

                await _toDoRepository.DeleteAsync(todoExiste);

                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                result.WithException("Erro ao deletar Tarefa - " + ex);
                return result;
            }

            result.Value = _mapper.Map<ToDoResponse>(todoExiste);
            return result;
        }
    }
}