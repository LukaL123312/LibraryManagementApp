using LibraryApp.Domain.AuthorEntity;
using LibraryApp.Domain.BookEntity;

using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Infrastructure.Data.DbContext;

public class LibraryAppDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public LibraryAppDbContext(DbContextOptions<LibraryAppDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>()
        .HasMany(c => c.Books)
        .WithOne(e => e.Author);

        base.OnModelCreating(modelBuilder);
    }
    public DbSet<Author> Authors { get; set; }

    public DbSet<Book> Books { get; set; }

}