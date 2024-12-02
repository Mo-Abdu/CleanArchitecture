using Application.Commands.BookCommands;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {

        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/books
        [HttpGet]
        public async Task<IActionResult> GetBooks(CancellationToken cancellationToken)
        {
            var query = new GetBooksQuery();
            var books = await _mediator.Send(query, cancellationToken);
            return Ok(books);
        }

        // GET: api/books/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id, CancellationToken cancellationToken)
        {
            var query = new GetBookByIdQuery(id); 
            var book = await _mediator.Send(query, cancellationToken);

            if (book == null)
                return NotFound();

            return Ok(book);
        }

        // POST: api/books
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] AddBookCommand command, CancellationToken cancellationToken)
        {
            var book = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }

        // PUT: api/books/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
                return BadRequest("Book ID mismatch");

            var book = await _mediator.Send(command, cancellationToken);

            if (book == null)
                return NotFound();

            return Ok(book);
        }

        // DELETE: api/books/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id, CancellationToken cancellationToken)
        {
            var command = new DeleteBookCommand(id);
            var result = await _mediator.Send(command, cancellationToken);

            if (!result)
                return NotFound();

            return NoContent();
        }

    }
}
