using AutoMapper;

using FluentValidation;

using LibraryApp.Application.CustomExceptions;
using LibraryApp.Application.Interfaces.IRepositories;
using LibraryApp.Application.Queries;
using LibraryApp.Domain;
using LibraryApp.Domain.BookEntity;

using MediatR;

namespace LibraryApp.Application.Handlers.QueryHandlers;

public class GetBooksByAuthorQueryHandler : IRequestHandler<GetBooksByAuthorQuery, IEnumerable<Book>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IValidator<GetBooksByAuthorQuery> _validator;
    private readonly IMapper _mapper;

    public GetBooksByAuthorQueryHandler(IBookRepository bookRepository, IValidator<GetBooksByAuthorQuery> validator, IMapper mapper)
    {
        _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
    }
    public async Task<IEnumerable<Book>> Handle(GetBooksByAuthorQuery request, CancellationToken cancellationToken)
    {
        var paginationFilter = _mapper.Map<PaginationFilter>(request.PaginationDetails);

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid)
        {
            var response = await _bookRepository.GetBooksByAuthor(paginationFilter, request.AuthorName, request.AuthorLastName, cancellationToken);

            if (response is null)
            {
                throw new BookByAuthorNotFoundException();
            }

            return response;
        }

        var failures = validationResult.Errors;

        throw new ValidationException(failures);
    }
}
