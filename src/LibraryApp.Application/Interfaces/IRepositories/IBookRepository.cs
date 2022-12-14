using LibraryApp.Domain;
using LibraryApp.Domain.BookEntity;

namespace LibraryApp.Application.Interfaces.IRepositories;

public interface IBookRepository : IRepository<Book>
{
    Task<IEnumerable<Book>> GetBooksByAuthor(PaginationFilter paginationFilter, string authorName, string authorLastName, CancellationToken cancellationToken);
    Task<IEnumerable<Book>> GetBooksAsync(PaginationFilter paginationFilter, CancellationToken cancellationToken = default);
}
