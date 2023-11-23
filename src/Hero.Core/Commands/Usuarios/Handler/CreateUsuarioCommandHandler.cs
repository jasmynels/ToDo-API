using AutoMapper;
using Core.Entities.Usuarios;
using Core.Interfaces;
using Core.Interfaces.Repositories.Usuarios;
using Core.Models.Responses;
using MediatR;
using Shared.Core;

namespace Core.Commands.Usuarios.Handler
{
    public class CreateUsuarioCommandHandler : IRequestHandler<CreateUsuarioCommand, Result<UsuarioResponse>>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateUsuarioCommandHandler(IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<UsuarioResponse>> Handle(CreateUsuarioCommand request, CancellationToken cancellationToken)
        {
            var result = new Result<UsuarioResponse>();

            var emailExiste = _usuarioRepository.EmailExiste(request.Email);
            if (emailExiste)
            {
                result.WithError("Email já cadastrado");
                return result;
            }

            var senha = request.Senha;
            request.Senha = BCrypt.Net.BCrypt.HashPassword(senha);

            var usuario = _mapper.Map<Usuario>(request);

            try
            {
                _unitOfWork.OpenTransaction();
                await _usuarioRepository.AddAsync(usuario);
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                result.WithException("Erro ao criar usuario - " + ex);
                return result;
            }
            result.Value = _mapper.Map<UsuarioResponse>(usuario);
            return result;
        }
    }
}
