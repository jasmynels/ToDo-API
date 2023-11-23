using Core.Commands.Usuarios;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Api;

namespace Api.Controllers
{
    [ApiController]
    [BaseRoute("usuario")]

    public class UsuarioController : Controller
    {
        private readonly IMediator mediator;

        public UsuarioController(IMediator mediator)
            => this.mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUsuarioCommand request)
        {
            return Ok(await mediator.Send(request));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login request)
        {
            return Ok(await mediator.Send(request));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            return Ok(await mediator.Send(new DeleteUsuarioCommand(id)));
        }
    }
}