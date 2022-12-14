using FluentValidation;

using MediatR;

namespace LibraryApp.Application.Commands;

public class DeleteAuthorCommand : IRequest<int>
{
    public int Id { get; set; }
}

public class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
{
    public DeleteAuthorCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .GreaterThanOrEqualTo(1);
    }
}
