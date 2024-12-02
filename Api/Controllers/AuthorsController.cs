using Application.Commands.AuthorCommands;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/authors
        [HttpGet]
        public async Task<IActionResult> GetAuthors(CancellationToken cancellationToken)
        {
            var query = new GetAuthorsQuery();
            var authors = await _mediator.Send(query, cancellationToken);
            return Ok(authors);
        }

        
        // GET: api/authors/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(int id, CancellationToken cancellationToken)
        {
            var query = new GetAuthorByIdQuery(id); 
            var author = await _mediator.Send(query, cancellationToken);

            if (author == null)
                return NotFound();

            return Ok(author);
        }

        // POST: api/authors
        [HttpPost]
        public async Task<IActionResult> AddAuthor([FromBody] AddAuthorCommand command, CancellationToken cancellationToken)
        {
            var author = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetAuthorById), new { id = author.Id }, author);
        }

        // PUT: api/authors/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] UpdateAuthorCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
                return BadRequest("Author ID mismatch");

            var author = await _mediator.Send(command, cancellationToken);

            if (author == null)
                return NotFound();

            return Ok(author);
        }

        // DELETE: api/authors/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id, CancellationToken cancellationToken)
        {
            var command = new DeleteAuthorCommand(id);
            var result = await _mediator.Send(command, cancellationToken);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
