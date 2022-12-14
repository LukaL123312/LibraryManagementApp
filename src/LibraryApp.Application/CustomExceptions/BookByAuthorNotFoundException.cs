namespace LibraryApp.Application.CustomExceptions;

public class BookByAuthorNotFoundException : Exception
{
    private static readonly new string Message = "No books by this author exist";

    public BookByAuthorNotFoundException()
        : base(Message)
    {
    }

    public BookByAuthorNotFoundException(string message)
        : base(message)
    {
    }
}
