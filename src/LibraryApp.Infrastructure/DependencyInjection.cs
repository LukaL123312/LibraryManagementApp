using LibraryApp.Application.Interfaces.IRepositories;
using LibraryApp.Application.Interfaces.IUnitOfWork;
using LibraryApp.Infrastructure.Data;
using LibraryApp.Infrastructure.Data.DbContext;
using LibraryApp.Infrastructure.Data.Repository;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddDbContext<LibraryAppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("LibraryAppDbConnectionString")));
        var buildServiceProvider = services.BuildServiceProvider();
        if ((buildServiceProvider.GetService(typeof(LibraryAppDbContext)) is LibraryAppDbContext libraryAppDbContext))
        {
            libraryAppDbContext.Database.EnsureCreated();
            libraryAppDbContext.Dispose();
        }

        return services;
    }
}
