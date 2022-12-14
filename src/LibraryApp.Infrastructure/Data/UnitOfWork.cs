using LibraryApp.Application.Interfaces.IRepositories;
using LibraryApp.Application.Interfaces.IUnitOfWork;
using LibraryApp.Infrastructure.Data.DbContext;
using LibraryApp.Infrastructure.Data.Repository;

namespace LibraryApp.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private LibraryAppDbContext _context;

    public UnitOfWork(LibraryAppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IAuthorRepository AuthorRepository => new AuthorRepository(_context);

    public IBookRepository BookRepository => new BookRepository(_context);

    public async Task<int> SaveChangeAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
