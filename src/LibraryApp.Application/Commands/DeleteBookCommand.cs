using FluentValidation;

using MediatR;

namespace LibraryApp.Application.Commands;

public class DeleteBookCommand : IRequest<int>
{
    public int Id { get; set; }
}

public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
{
    public DeleteBookCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .GreaterThanOrEqualTo(1);
    }
}
