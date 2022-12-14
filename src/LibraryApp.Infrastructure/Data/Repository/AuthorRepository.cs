using LibraryApp.Application.Interfaces.IRepositories;
using LibraryApp.Domain.AuthorEntity;
using LibraryApp.Infrastructure.Data.DbContext;

namespace LibraryApp.Infrastructure.Data.Repository;

public class AuthorRepository : Repository<Author>, IAuthorRepository
{
    public AuthorRepository(LibraryAppDbContext context) : base(context)
    {

    }
}
