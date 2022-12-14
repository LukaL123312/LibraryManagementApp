using FluentAssertions;

using FluentValidation;
using FluentValidation.Results;

using LibraryApp.Application.Commands;
using LibraryApp.Application.Handlers.CommandHandlers;
using LibraryApp.Application.Interfaces.IUnitOfWork;
using LibraryApp.Domain.AuthorEntity;

using Moq;

using Xunit;

namespace LibraryApp.Application.Tests.HandlerTests.CommandHandlerTests;

public class AddAuthorCommandHandlerTests
{
    private readonly Mock<ValidationResult> _validationResult;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IValidator<AddAuthorCommand>> _validatorMock;

    public AddAuthorCommandHandlerTests()
    {
        _validationResult = new();
        _unitOfWorkMock = new();
        _validatorMock = new();
    }

    [Fact]
    public async Task Handle_Should_Return_Integer_When_Successful_And_ValidationIsValid()
    {
        // Arrange
        _validationResult.Setup(x => x.IsValid).Returns(true);
        var validationResult = _validationResult.Object;

        var command = new AddAuthorCommand();

        var handler = new AddAuthorCommandHandler(_unitOfWorkMock.Object, _validatorMock.Object);

        _validatorMock.Setup(x => x.ValidateAsync(It.IsAny<AddAuthorCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationResult);

        _unitOfWorkMock.Setup(x => x.AuthorRepository.AddAsync(It.IsAny<Author>(), It.IsAny<CancellationToken>())).ReturnsAsync(It.IsAny<Author>);

        _unitOfWorkMock.Setup(x => x.SaveChangeAsync()).ReturnsAsync(It.IsAny<int>);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        Assert.IsType<int>(result);
    }

    [Fact]
    public async Task Handle_Should_Throw_ValidationException_When_ValidationIsNotValid()
    {
        // Arrange
        var command = new AddAuthorCommand();

        var handler = new AddAuthorCommandHandler(_unitOfWorkMock.Object, _validatorMock.Object);

        _validationResult.Setup(x => x.IsValid).Returns(false);
        var validationResult = _validationResult.Object;

        _validatorMock.Setup(x => x.ValidateAsync(It.IsAny<AddAuthorCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationResult);

        // Assert
        await FluentActions.Awaiting(() => handler.Handle(command, default)).Should().ThrowAsync<ValidationException>();
    }
}
