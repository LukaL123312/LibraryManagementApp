using FluentValidation;

using LibraryApp.Application.RequestQuery;
using LibraryApp.Domain.BookEntity;

using MediatR;

namespace LibraryApp.Application.Queries;

public class GetBooksByAuthorQuery : IRequest<IEnumerable<Book>>
{
    public string AuthorName { get; set; }

    public string AuthorLastName { get; set; }

    public PaginationDetails? PaginationDetails { get; set; }
}

public class GetBooksByAuthorQueryValidator : AbstractValidator<GetBooksByAuthorQuery>
{
    public GetBooksByAuthorQueryValidator()
    {
        RuleFor(x => x.AuthorName)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.");

        RuleFor(x => x.AuthorLastName)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.");

        RuleFor(x => x.PaginationDetails)
            .SetValidator(new PaginationDetailsValidator())
            .When(x => x.PaginationDetails is not null);

    }
}
