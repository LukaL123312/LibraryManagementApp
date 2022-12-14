using FluentValidation;

using LibraryApp.Application.Commands;
using LibraryApp.Application.Interfaces.IUnitOfWork;
using LibraryApp.Domain.AuthorEntity;

using MediatR;

namespace LibraryApp.Application.Handlers.CommandHandlers;

public class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<AddAuthorCommand> _validator;

    public AddAuthorCommandHandler(IUnitOfWork unitOfWork, IValidator<AddAuthorCommand> validator)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public async Task<int> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid)
        {
            await _unitOfWork.AuthorRepository.AddAsync(new Author
            {
                Name = request.Name,
                Surname = request.Surname
            }, cancellationToken);

            return await _unitOfWork.SaveChangeAsync();
        }

        var failures = validationResult.Errors;

        throw new ValidationException(failures);
    }
}
