using Shared.Core.Models;

namespace Core.Models.Responses
{
    public class UsuarioResponse : BaseResponse
    {
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}
