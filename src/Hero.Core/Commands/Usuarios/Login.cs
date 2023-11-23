using Core.Models.Responses;
using MediatR;
using Shared.Core;

namespace Core.Commands.Usuarios
{
    public class Login : IRequest<Result<LoginResponse>>
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
