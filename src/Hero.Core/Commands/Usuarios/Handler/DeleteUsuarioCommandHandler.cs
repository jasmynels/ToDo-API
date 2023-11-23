using AutoMapper;
using Core.Interfaces;
using Core.Interfaces.Repositories.Usuarios;
using Core.Models.Responses;
using MediatR;
using Shared.Core;

namespace Core.Commands.Usuarios.Handler
{
    public class DeleteUsuarioCommandHandler : IRequestHandler<DeleteUsuarioCommand, Result<UsuarioResponse>>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteUsuarioCommandHandler(IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<UsuarioResponse>> Handle(DeleteUsuarioCommand request, CancellationToken cancellationToken)
        {
            var result = new Result<UsuarioResponse>();
            var usuarioExiste = await _usuarioRepository.GetById(request.Id);

            if (usuarioExiste == null)
            {
                result.WithError("Erro ao encontrar tarefa.");
                return result;
            }

            try
            {
                _unitOfWork.OpenTransaction();

                await _usuarioRepository.DeleteAsync(usuarioExiste);

                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                result.WithException("Erro ao deletar Tarefa - " + ex);
                return result;
            }

            result.Value = _mapper.Map<UsuarioResponse>(usuarioExiste);
            return result;
        }
    }
}