using AutoMapper;

using LibraryApp.Application.CustomExceptions;
using LibraryApp.Application.Interfaces.IRepositories;
using LibraryApp.Application.Queries;
using LibraryApp.Domain;
using LibraryApp.Domain.BookEntity;

using MediatR;

namespace LibraryApp.Application.Handlers.QueryHandlers;

public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, IEnumerable<Book>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public GetBooksQueryHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
    }
    public async Task<IEnumerable<Book>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        var paginationFilter = _mapper.Map<PaginationFilter>(request.PaginationDetails);

        var response = await _bookRepository.GetBooksAsync(paginationFilter, cancellationToken);

        if (response is null)
        {
            throw new BookNotFoundException();
        }

        return response;
    }
}
