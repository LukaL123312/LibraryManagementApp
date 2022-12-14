namespace LibraryApp.Application.CustomExceptions;

public class AuthorDeletionFailedException : Exception
{
    private static readonly new string Message = "Author With this Id Not found";

    public AuthorDeletionFailedException()
        : base(Message)
    {
    }

    public AuthorDeletionFailedException(string message)
        : base(message)
    {
    }
}
