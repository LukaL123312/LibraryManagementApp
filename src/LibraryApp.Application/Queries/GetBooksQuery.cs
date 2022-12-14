using LibraryApp.Application.RequestQuery;
using LibraryApp.Domain.BookEntity;

using MediatR;

namespace LibraryApp.Application.Queries;

public class GetBooksQuery : IRequest<IEnumerable<Book>>
{
    public PaginationDetails? PaginationDetails { get; set; }
}
