using System.Text.Json.Serialization;

using LibraryApp.Api;
using LibraryApp.Api.Middlewares;
using LibraryApp.Api.Options;
using LibraryApp.Application;
using LibraryApp.Infrastructure;

using WatchDog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;

var watchDogOptions = configuration.GetSection(nameof(WatchDogOptions)).Get<WatchDogOptions>();

builder.Services.AddInfrastructure(configuration);
builder.Services.AddApplication();

builder.Services.AddModelValidation();

builder.Services.AddWatchdogLogging(configuration, watchDogOptions);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseWatchDogExceptionLogger();

app.UseWatchDog(options => {
    options.WatchPageUsername = watchDogOptions.WatchPageUsername;
    options.WatchPagePassword = watchDogOptions.WatchPagePassword;
});

app.Run();
