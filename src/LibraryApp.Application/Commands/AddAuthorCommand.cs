using FluentValidation;

using MediatR;

namespace LibraryApp.Application.Commands;

public class AddAuthorCommand : IRequest<int>
{
    public string Name { get; set; }

    public string Surname { get; set; }
}

public class AddAuthorCommandValidator : AbstractValidator<AddAuthorCommand>
{
    public AddAuthorCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.");

        RuleFor(x => x.Surname)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.");
    }
}
