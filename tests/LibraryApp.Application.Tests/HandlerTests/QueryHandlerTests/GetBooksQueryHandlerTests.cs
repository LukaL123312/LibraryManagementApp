using AutoMapper;

using FluentAssertions;

using LibraryApp.Application.CustomExceptions;
using LibraryApp.Application.Handlers.QueryHandlers;
using LibraryApp.Application.Interfaces.IRepositories;
using LibraryApp.Application.Queries;
using LibraryApp.Domain;
using LibraryApp.Domain.BookEntity;

using Moq;

using Xunit;

namespace LibraryApp.Application.Tests.HandlerTests.QueryHandlerTests;

public class GetBooksQueryHandlerTests
{
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;

    public GetBooksQueryHandlerTests()
    {
        _bookRepositoryMock = new();
        _mapperMock = new();
    }

    [Fact]
    public async Task Handle_Should_Return_PagedResponseBook_When_Successful()
    {
        // Arrange
        IEnumerable<Book> books = new List<Book>();
        var query = new GetBooksQuery();

        var handler = new GetBooksQueryHandler(
            _bookRepositoryMock.Object, _mapperMock.Object);

        _bookRepositoryMock.Setup(x => x.GetBooksAsync(It.IsAny<PaginationFilter>(), It.IsAny<CancellationToken>())).ReturnsAsync(books);

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.Should().BeOfType<List<Book>>();
    }

    [Fact]
    public async Task Handle_Should_Throw_BookNotFoundException_When_Failed()
    {
        // Arrange
        List<Book>? getAllAsyncResponse = null;

        var query = new GetBooksQuery();

        var handler = new GetBooksQueryHandler(
            _bookRepositoryMock.Object, _mapperMock.Object);

        _bookRepositoryMock.Setup(x => x.GetAllAsync(It.IsAny<PaginationFilter>(), It.IsAny<CancellationToken>())).ReturnsAsync(getAllAsyncResponse);

        // Assert
        await FluentActions.Awaiting(() => handler.Handle(query, default)).Should().ThrowAsync<BookNotFoundException>();
    }
}
