using FluentValidation;

using LibraryApp.Application.Commands;
using LibraryApp.Application.CustomExceptions;
using LibraryApp.Application.Interfaces.IUnitOfWork;

using MediatR;

namespace LibraryApp.Application.Handlers.CommandHandlers;

public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<DeleteAuthorCommand> _validator;

    public DeleteAuthorCommandHandler(IUnitOfWork unitOfWork, IValidator<DeleteAuthorCommand> validator)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public async Task<int> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid)
        {
            var author = await _unitOfWork.AuthorRepository.FindAsync(x => x.Id == request.Id);

            if (author is null)
            {
                throw new AuthorDeletionFailedException();
            }

            _unitOfWork.AuthorRepository.Remove(author);

            return await _unitOfWork.SaveChangeAsync();
        }

        var failures = validationResult.Errors;

        throw new ValidationException(failures);
    }
}