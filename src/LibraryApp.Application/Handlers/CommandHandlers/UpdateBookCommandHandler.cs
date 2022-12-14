using FluentValidation;

using LibraryApp.Application.Commands;
using LibraryApp.Application.CustomExceptions;
using LibraryApp.Application.Interfaces.IUnitOfWork;

using MediatR;

namespace LibraryApp.Application.Handlers.CommandHandlers;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateBookCommand> _validator;

    public UpdateBookCommandHandler(IUnitOfWork unitOfWork, IValidator<UpdateBookCommand> validator)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public async Task<int> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid)
        {
            var book = await _unitOfWork.BookRepository.FindAsync(x => x.Id == request.Id);

            if (book is null)
            {
                throw new BookNotFoundException();
            }

            book.Title = request.Title;
            book.Description = request.Description;

            await _unitOfWork.BookRepository.UpdateAsync(book);

            return await _unitOfWork.SaveChangeAsync();
        }

        var failures = validationResult.Errors;

        throw new ValidationException(failures);
    }
}
