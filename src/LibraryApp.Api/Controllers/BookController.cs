using LibraryApp.Application.Commands;
using LibraryApp.Application.Queries;
using LibraryApp.Application.RequestQuery;
using LibraryApp.Application.ResponseWrapperModels;
using LibraryApp.Domain.BookEntity;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IMediator _mediatr;
    public BookController(IMediator mediatr)
    {
        _mediatr = mediatr;
    }

    [HttpGet("get-books")]
    public async Task<IActionResult> GetBooks([FromQuery] PaginationDetails paginationDetails, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(new GetBooksQuery { PaginationDetails = paginationDetails }, cancellationToken);
        return Ok(new PagedResponse<IEnumerable<Book>>(result));
    }

    [HttpGet("get-books-by-author-name-and-surname")]
    public async Task<IActionResult> GetBooksByAuthor([FromQuery] PaginationDetails paginationDetails, string authorName, string authorLastName, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(new GetBooksByAuthorQuery { AuthorName = authorName, AuthorLastName = authorLastName, PaginationDetails = paginationDetails }, cancellationToken);
        return Ok(new PagedResponse<IEnumerable<Book>>(result));
    }

    [HttpPost("add-book")]
    public async Task<IActionResult> AddBook([FromBody] AddBookCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("delete-book")]
    public async Task<IActionResult> DeleteBook(int bookId, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(new DeleteBookCommand { Id = bookId }, cancellationToken);
        return Ok(result);
    }

    [HttpPatch("update-book")]
    public async Task<IActionResult> UpdateBook([FromBody] UpdateBookCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(command, cancellationToken);
        return Ok(result);
    }

}
