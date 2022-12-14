namespace LibraryApp.Application.CustomExceptions;

public class BookCreationFailedException : Exception
{
    private static readonly new string Message = "Author with this Id doesn't exist.";

    public BookCreationFailedException()
        : base(Message)
    {
    }

    public BookCreationFailedException(string message)
        : base(message)
    {
    }
}
