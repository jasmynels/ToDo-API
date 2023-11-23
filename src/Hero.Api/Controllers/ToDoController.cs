using Core.Commands.ToDos;
using Core.Models.Requests;
using Core.Queries.ToDos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Api;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers
{
    [ApiController]
    [BaseRoute("to-do")]

    public class ToDoController : Controller
    {
        private readonly IMediator mediator;

        public ToDoController(IMediator mediator)
            => this.mediator = mediator;

        [HttpGet("{id}")]
        public async Task<IActionResult> ListById([FromRoute] Guid id)
        {
            var response = await mediator.Send(new GetToDoByIdQuery(id));

            return Ok(response);
        }

        [HttpGet("{id}/user")]
        public async Task<IActionResult> ListByUser([FromRoute] Guid id)
        {
            var response = await mediator.Send(new GetToDoByUsuarioQuery(id));

            return Ok(response);
        }

        [HttpPost]
        [SwaggerOperation(
             Summary = "Rota usada para criar uma tarefa.",
                Description = "Rota usada para criar uma tarefa.")]
        public async Task<IActionResult> Create([FromBody] CreateToDoCommand request)
        {
            return Ok(await mediator.Send(request));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateToDoRequest request)
        {
            return Ok(await mediator.Send(new UpdateToDoCommand(id, request)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            return Ok(await mediator.Send(new DeleteToDoCommand(id)));
        }
    }
}