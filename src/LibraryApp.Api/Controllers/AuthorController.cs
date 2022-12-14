using LibraryApp.Application.Commands;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorController : ControllerBase
{
    private readonly IMediator _mediatr;
    public AuthorController(IMediator mediatr)
    {
        _mediatr = mediatr;
    }

    [HttpPost("add-author")]
    public async Task<IActionResult> AddAuthor([FromBody] AddAuthorCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("delete-author")]
    public async Task<IActionResult> DeleteAuthor(int authorId, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(new DeleteAuthorCommand { Id = authorId }, cancellationToken);
        return Ok(result);
    }
}
