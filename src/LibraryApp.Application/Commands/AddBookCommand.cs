using FluentValidation;

using MediatR;

namespace LibraryApp.Application.Commands;

public class AddBookCommand : IRequest<int>
{
    public string Title { get; set; }

    public int AuthorId { get; set; }

    public string Description { get; set; }
}

public class AddBookCommandValidator : AbstractValidator<AddBookCommand>
{
    public AddBookCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.");

        RuleFor(x => x.AuthorId)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.");
    }
}

