using FluentValidation;

using MediatR;

namespace LibraryApp.Application.Commands;

public class UpdateBookCommand : IRequest<int>
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }
}

public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.");
    }
}