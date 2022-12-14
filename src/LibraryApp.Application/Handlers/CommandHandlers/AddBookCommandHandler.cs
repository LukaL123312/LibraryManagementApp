using FluentValidation;

using LibraryApp.Application.Commands;
using LibraryApp.Application.CustomExceptions;
using LibraryApp.Application.Interfaces.IUnitOfWork;
using LibraryApp.Domain.BookEntity;

using MediatR;

namespace LibraryApp.Application.Handlers.CommandHandlers;

public class AddBookCommandHandler : IRequestHandler<AddBookCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<AddBookCommand> _validator;

    public AddBookCommandHandler(IUnitOfWork unitOfWork, IValidator<AddBookCommand> validator)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _validator = validator ?? throw new ArgumentNullException();
    }

    public async Task<int> Handle(AddBookCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid)
        {
            var author = await _unitOfWork.AuthorRepository
                .FindAsync(x => x.Id == request.AuthorId, cancellationToken);

            if (author is null)
            {
                throw new BookCreationFailedException();
            }

            await _unitOfWork.BookRepository.AddAsync(new Book
            {
                Title = request.Title,
                AuthorId = author.Id,
                Description = request.Description
            }, cancellationToken);

            return await _unitOfWork.SaveChangeAsync();
        }

        var failures = validationResult.Errors;

        throw new ValidationException(failures);
    }
}
