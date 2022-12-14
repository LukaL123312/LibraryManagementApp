using AutoMapper;

using FluentAssertions;

using FluentValidation;
using FluentValidation.Results;

using LibraryApp.Application.CustomExceptions;
using LibraryApp.Application.Handlers.QueryHandlers;
using LibraryApp.Application.Interfaces.IRepositories;
using LibraryApp.Application.Queries;
using LibraryApp.Domain;
using LibraryApp.Domain.BookEntity;

using Moq;

using Xunit;

namespace LibraryApp.Application.Tests.HandlerTests.QueryHandlerTests;

public class GetBooksByAuthorQueryHandlerTests
{
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly Mock<ValidationResult> _validationResult;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IValidator<GetBooksByAuthorQuery>> _validatorMock;

    public GetBooksByAuthorQueryHandlerTests()
    {
        _bookRepositoryMock = new();
        _validationResult = new();
        _mapperMock = new();
        _validatorMock = new();
    }

    [Fact]
    public async Task Handle_Should_Return_PagedResponseBook_When_Successful_And_ValidationIsValid()
    {
        // Arrange
        var getAllAsyncResponse = new List<Book>();

        var query = new GetBooksByAuthorQuery();

        var handler = new GetBooksByAuthorQueryHandler(
            _bookRepositoryMock.Object, _validatorMock.Object, _mapperMock.Object);

        _validationResult.Setup(x => x.IsValid).Returns(true);
        var validationResult = _validationResult.Object;

        _validatorMock.Setup(x => x.ValidateAsync(It.IsAny<GetBooksByAuthorQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationResult);

        _bookRepositoryMock.Setup(x => x.GetBooksByAuthor(It.IsAny<PaginationFilter>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(getAllAsyncResponse);

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.Should().BeOfType<List<Book>>();
    }

    [Fact]
    public async Task Handle_Should_Throw_BookByAuthotNotFoundException_When_Failed_And_ValidationIsValid()
    {
        // Arrange
        List<Book>? getAllAsyncResponse = null;

        var query = new GetBooksByAuthorQuery();

        var handler = new GetBooksByAuthorQueryHandler(
            _bookRepositoryMock.Object, _validatorMock.Object, _mapperMock.Object);

        _validationResult.Setup(x => x.IsValid).Returns(true);
        var validationResult = _validationResult.Object;

        _validatorMock.Setup(x => x.ValidateAsync(It.IsAny<GetBooksByAuthorQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationResult);

        _bookRepositoryMock.Setup(x => x.GetBooksByAuthor(It.IsAny<PaginationFilter>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(getAllAsyncResponse);

        // Assert
        await FluentActions.Awaiting(() => handler.Handle(query, default)).Should().ThrowAsync<BookByAuthorNotFoundException>();
    }

    [Fact]
    public async Task Handle_Should_Throw_ValidationException_When_ValidationIsNotValid()
    {
        // Arrange
        var query = new GetBooksByAuthorQuery();

        var handler = new GetBooksByAuthorQueryHandler(
            _bookRepositoryMock.Object, _validatorMock.Object, _mapperMock.Object);

        _validationResult.Setup(x => x.IsValid).Returns(false);
        var validationResult = _validationResult.Object;

        _validatorMock.Setup(x => x.ValidateAsync(It.IsAny<GetBooksByAuthorQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationResult);

        // Assert
        await FluentActions.Awaiting(() => handler.Handle(query, default)).Should().ThrowAsync<ValidationException>();
    }
}
