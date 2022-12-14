using System.Linq.Expressions;

using FluentAssertions;

using FluentValidation;
using FluentValidation.Results;

using LibraryApp.Application.Commands;
using LibraryApp.Application.CustomExceptions;
using LibraryApp.Application.Handlers.CommandHandlers;
using LibraryApp.Application.Interfaces.IUnitOfWork;
using LibraryApp.Domain.BookEntity;

using Moq;

using Xunit;

namespace LibraryApp.Application.Tests.HandlerTests.CommandHandlerTests;

public class DeleteBookCommandHandlerTests
{
    private readonly Mock<ValidationResult> _validationResult;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IValidator<DeleteBookCommand>> _validatorMock;

    public DeleteBookCommandHandlerTests()
    {
        _validationResult = new();
        _unitOfWorkMock = new();
        _validatorMock = new();
    }

    [Fact]
    public async Task Handle_Should_Return_Integer_When_Successful_And_ValidationIsValid()
    {
        // Arrange
        var book = new Book();
        var command = new DeleteBookCommand();

        var handler = new DeleteBookCommandHandler(_unitOfWorkMock.Object, _validatorMock.Object);

        _validationResult.Setup(x => x.IsValid).Returns(true);
        var validationResult = _validationResult.Object;

        _validatorMock.Setup(x => x.ValidateAsync(It.IsAny<DeleteBookCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationResult);

        _unitOfWorkMock.Setup(x => x.BookRepository.FindAsync(It.IsAny<Expression<Func<Book, bool>>>(), It.IsAny<CancellationToken>())).ReturnsAsync(book);

        _unitOfWorkMock.Setup(x => x.BookRepository.Remove(It.IsAny<Book>()));

        _unitOfWorkMock.Setup(x => x.SaveChangeAsync()).ReturnsAsync(It.IsAny<int>);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        Assert.IsType<int>(result);
    }

    [Fact]
    public async Task Handle_Should_Throw_BookNotFoundException_When_ValidationIsValid_And_AuthorIsNull()
    {
        // Arrange
        Book? book = null;
        var command = new DeleteBookCommand();

        var handler = new DeleteBookCommandHandler(_unitOfWorkMock.Object, _validatorMock.Object);

        _validationResult.Setup(x => x.IsValid).Returns(true);
        var validationResult = _validationResult.Object;

        _validatorMock.Setup(x => x.ValidateAsync(It.IsAny<DeleteBookCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationResult);

        _unitOfWorkMock.Setup(x => x.BookRepository.FindAsync(It.IsAny<Expression<Func<Book, bool>>>(), It.IsAny<CancellationToken>())).ReturnsAsync(book);


        // Assert
        await FluentActions.Awaiting(() => handler.Handle(command, default)).Should().ThrowAsync<BookNotFoundException>();
    }

    [Fact]
    public async Task Handle_Should_Throw_ValidationException_When_ValidationIsNotValid()
    {
        // Arrange
        var command = new DeleteBookCommand();

        var handler = new DeleteBookCommandHandler(_unitOfWorkMock.Object, _validatorMock.Object);

        _validationResult.Setup(x => x.IsValid).Returns(false);
        var validationResult = _validationResult.Object;

        _validatorMock.Setup(x => x.ValidateAsync(It.IsAny<DeleteBookCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationResult);

        // Assert
        await FluentActions.Awaiting(() => handler.Handle(command, default)).Should().ThrowAsync<ValidationException>();
    }
}
