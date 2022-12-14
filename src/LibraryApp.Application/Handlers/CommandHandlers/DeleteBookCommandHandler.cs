using FluentValidation;

using LibraryApp.Application.Commands;
using LibraryApp.Application.CustomExceptions;
using LibraryApp.Application.Interfaces.IUnitOfWork;

using MediatR;

namespace LibraryApp.Application.Handlers.CommandHandlers;

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<DeleteBookCommand> _validator;

    public DeleteBookCommandHandler(IUnitOfWork unitOfWork, IValidator<DeleteBookCommand> validator)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public async Task<int> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid)
        {
            var book = await _unitOfWork.BookRepository.FindAsync(x => x.Id == request.Id, cancellationToken);

            if (book is null)
            {
                throw new BookNotFoundException();
            }

            _unitOfWork.BookRepository.Remove(book);

            return await _unitOfWork.SaveChangeAsync();
        }

        var failures = validationResult.Errors;

        throw new ValidationException(failures);
    }
}
