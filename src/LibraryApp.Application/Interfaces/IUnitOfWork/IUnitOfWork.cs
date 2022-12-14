using LibraryApp.Application.Interfaces.IRepositories;

namespace LibraryApp.Application.Interfaces.IUnitOfWork;

public interface IUnitOfWork
{
    public IAuthorRepository AuthorRepository { get; }
    public IBookRepository BookRepository { get; }
    Task<int> SaveChangeAsync();
}
