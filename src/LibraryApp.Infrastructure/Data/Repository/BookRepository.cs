using LibraryApp.Application.Interfaces.IRepositories;
using LibraryApp.Domain;
using LibraryApp.Domain.BookEntity;
using LibraryApp.Infrastructure.Data.DbContext;

using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Infrastructure.Data.Repository;

public class BookRepository : Repository<Book>, IBookRepository
{
    public BookRepository(LibraryAppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Book>> GetBooksByAuthor(PaginationFilter paginationFilter, string authorName, string authorLastName, CancellationToken cancellationToken = default)
    {
        if (paginationFilter is null)
        {
            return await _dbContext.Set<Book>().Where(x => x.Author.Name == authorName && x.Author.Surname == authorLastName)
            .Include(x => x.Author)
            .ToListAsync(cancellationToken);
        }

        return await _dbContext.Set<Book>().Where(x => x.Author.Name == authorName && x.Author.Surname == authorLastName)
            .Include(x => x.Author)
            .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
            .Take(paginationFilter.PageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Book>> GetBooksAsync(PaginationFilter paginationFilter, CancellationToken cancellationToken = default)
    {
        if (paginationFilter is null)
        {
            return await _dbContext.Set<Book>().Include(x => x.Author)
            .ToListAsync(cancellationToken);
        }

        return await _dbContext.Set<Book>().Include(x => x.Author)
            .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
            .Take(paginationFilter.PageSize)
            .ToListAsync(cancellationToken);
    }

}
