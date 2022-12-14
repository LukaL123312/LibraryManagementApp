using FluentValidation;

namespace LibraryApp.Application.RequestQuery;

public class PaginationDetails
{
    public PaginationDetails()
    {
        PageNumber = 1;
        PageSize = 100;
    }

    public PaginationDetails(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

public class PaginationDetailsValidator : AbstractValidator<PaginationDetails?>
{
    public PaginationDetailsValidator()
    {
        RuleFor(x => x.PageNumber)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .GreaterThanOrEqualTo(0)
            .WithMessage("{PropertyName} must be non negative.")
            .When(x => x.PageNumber < 0);

        RuleFor(x => x.PageSize)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .GreaterThanOrEqualTo(0)
            .WithMessage("{PropertyName} must be non negative.")
            .When(x => x.PageSize < 0);
    }
}
