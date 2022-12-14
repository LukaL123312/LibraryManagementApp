using System.Net;

using FluentValidation;

using LibraryApp.Api.Middlewares;
using LibraryApp.Application.CustomExceptions;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Moq;

using Xunit;

namespace LibraryApp.Api.Tests.MiddlewareTests;

public class ExceptionMiddlewareTests
{
    private readonly DefaultHttpContext httpContext;
    private readonly Mock<ILogger<ExceptionMiddleware>> iloggerMock;
    private ExceptionMiddleware? _sut;
    private RequestDelegate? next;

    public ExceptionMiddlewareTests()
    {
        httpContext = new();
        iloggerMock = new();
    }

    [Fact]
    public async void HandleExceptionAsync_SetsStatusCodeCorrectly_WhenValidationException()
    {
        // Arrange
        var exceptionMessage = "Validation Failed";
        var exception = new ValidationException(exceptionMessage);

        next = (httpContext) => {
            return Task.FromException(exception);
        };
        var contentType = "application/json";
        _sut = new ExceptionMiddleware(next, iloggerMock.Object);

        // Act
        await _sut.InvokeAsync(httpContext);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)httpContext.Response.StatusCode);
        Assert.Equal(contentType, httpContext.Response.ContentType);
    }

    [Fact]
    public async void HandleExceptionAsync_SetsStatusCodeAndErrorMessageCorrectly_When_AuthorDeletionFailedException()
    {
        // Arrange
        var exception = new AuthorDeletionFailedException();

        next = (httpContext) => {
            return Task.FromException(exception);
        };

        var contentType = "application/json";
        _sut = new ExceptionMiddleware(next, iloggerMock.Object);

        // Act
        await _sut.InvokeAsync(httpContext);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)httpContext.Response.StatusCode);
        Assert.Equal(contentType, httpContext.Response.ContentType);
    }

    [Fact]
    public async void HandleExceptionAsync_SetsStatusCodeAndErrorMessageCorrectly_When_BookCreationFailedException()
    {
        // Arrange
        var exception = new BookCreationFailedException();

        next = (httpContext) => {
            return Task.FromException(exception);
        };

        var contentType = "application/json";
        _sut = new ExceptionMiddleware(next, iloggerMock.Object);

        // Act
        await _sut.InvokeAsync(httpContext);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)httpContext.Response.StatusCode);
        Assert.Equal(contentType, httpContext.Response.ContentType);
    }

    [Fact]
    public async void HandleExceptionAsync_SetsStatusCodeAndErrorMessageCorrectly_When_BookByAuthotNotFoundException()
    {
        // Arrange
        var exception = new BookByAuthorNotFoundException();

        next = (httpContext) => {
            return Task.FromException(exception);
        };

        var contentType = "application/json";
        _sut = new ExceptionMiddleware(next, iloggerMock.Object);

        // Act
        await _sut.InvokeAsync(httpContext);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, (HttpStatusCode)httpContext.Response.StatusCode);
        Assert.Equal(contentType, httpContext.Response.ContentType);
    }

    [Fact]
    public async void HandleExceptionAsync_SetsStatusCodeAndErrorMessageCorrectly_When_BookNotFoundException()
    {
        // Arrange
        var exception = new BookNotFoundException();

        next = (httpContext) => {
            return Task.FromException(exception);
        };

        var contentType = "application/json";
        _sut = new ExceptionMiddleware(next, iloggerMock.Object);

        // Act
        await _sut.InvokeAsync(httpContext);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, (HttpStatusCode)httpContext.Response.StatusCode);
        Assert.Equal(contentType, httpContext.Response.ContentType);
    }

    [Fact]
    public async void HandleExceptionAsync_SetsStatusCodeAndErrorMessageCorrectly_WhenDefaultException()
    {
        // Arrange
        var exception = new Exception();
        next = (httpContext) => {
            return Task.FromException(exception);
        };
        var contentType = "application/json";
        _sut = new ExceptionMiddleware(next, iloggerMock.Object);

        // Act
        await _sut.InvokeAsync(httpContext);

        // Assert
        Assert.Equal(HttpStatusCode.InternalServerError, (HttpStatusCode)httpContext.Response.StatusCode);
        Assert.Equal(contentType, httpContext.Response.ContentType);
    }
}
