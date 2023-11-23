using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AutoMapper;
using Core.Interfaces;
using Core.Interfaces.Repositories.Usuarios;
using Core.Models.Responses;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Shared.Core;

namespace Core.Commands.Usuarios.Handler
{
    public class LoginHandler : IRequestHandler<Login, Result<LoginResponse>>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LoginHandler(IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<LoginResponse>> Handle(Login request, CancellationToken cancellationToken)
        {
            var result = new Result<LoginResponse>();

            var emailExiste = _usuarioRepository.EmailExiste(request.Email);

            if (!emailExiste)
            {
                result.WithError("Email não existente");
                return result;
            }

            var user = _usuarioRepository.GetUserByEmail(request.Email);

            var emailMatch = request.Email == user.Email;

            var senha = user.Senha;

            var senhaMatch = BCrypt.Net.BCrypt.Verify(request.Senha, senha);

            if (!emailMatch || !senhaMatch)
            {
                result.WithError("Email ou senha incorretos");
                return result;
            }


            result.Value = _mapper.Map<LoginResponse>(new LoginResponse { Token = GerarToken() });
            return result;
        }

        public string GerarToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sua_chave_secreta_com_pelo_menos_128_bits"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "seu_issuer",
                audience: "seu_audience",
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}