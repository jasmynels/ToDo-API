using Core.Models.Responses;
using MediatR;
using Shared.Core;

namespace Core.Commands.Usuarios
{
    public class DeleteUsuarioCommand : IRequest<Result<UsuarioResponse>>
    {
        public Guid Id { get; set; }
        public DeleteUsuarioCommand(Guid id)
        {
            Id = id;
        }

    }
}