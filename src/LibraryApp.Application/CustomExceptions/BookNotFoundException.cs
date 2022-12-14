namespace LibraryApp.Application.CustomExceptions;

public class BookNotFoundException : Exception
{
    private static readonly new string Message = "No Book Found";

    public BookNotFoundException()
        : base(Message)
    {
    }

    public BookNotFoundException(string message)
        : base(message)
    {
    }
}
