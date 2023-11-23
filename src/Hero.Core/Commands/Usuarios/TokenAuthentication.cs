using Microsoft.AspNetCore.Authentication;

namespace Core.Commands.Usuarios
{
    public class TokenAuthenticationOptions : AuthenticationSchemeOptions
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
