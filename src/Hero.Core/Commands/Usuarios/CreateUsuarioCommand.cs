using Core.Models.Responses;
using MediatR;
using Shared.Core;

namespace Core.Commands.Usuarios
{
    public class CreateUsuarioCommand : IRequest<Result<UsuarioResponse>>
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
