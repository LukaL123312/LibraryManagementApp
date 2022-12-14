using FluentValidation;

using LibraryApp.Api.Options;
using LibraryApp.Application.Commands;
using LibraryApp.Application.Queries;

using WatchDog;

namespace LibraryApp.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddModelValidation(this IServiceCollection services)
    {
        services.AddScoped<IValidator<GetBooksByAuthorQuery>, GetBooksByAuthorQueryValidator>();
        services.AddScoped<IValidator<AddAuthorCommand>, AddAuthorCommandValidator>();
        services.AddScoped<IValidator<AddBookCommand>, AddBookCommandValidator>();
        services.AddScoped<IValidator<DeleteAuthorCommand>, DeleteAuthorCommandValidator>();
        services.AddScoped<IValidator<DeleteBookCommand>, DeleteBookCommandValidator>();
        services.AddScoped<IValidator<UpdateBookCommand>, UpdateBookCommandValidator>();

        return services;
    }

    public static IServiceCollection AddWatchdogLogging(this IServiceCollection services, IConfiguration configuration, WatchDogOptions watchDogOptions)
    {
        if (watchDogOptions == null)
            throw new ArgumentNullException(nameof(watchDogOptions));

        services.AddSingleton(watchDogOptions);

        services.AddWatchDogServices(options => {
            options.IsAutoClear = false;
            options.SetExternalDbConnString = configuration.GetConnectionString("LibraryAppDbConnectionString");
            options.SqlDriverOption = WatchDog.src.Enums.WatchDogSqlDriverEnum.MSSQL;
        });

        return services;
    }
}
