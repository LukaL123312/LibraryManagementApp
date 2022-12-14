namespace LibraryApp.Application.CustomExceptions;

public class AuthorNotFoundException : Exception
{
    private static readonly new string Message = "No Author Found";

    public AuthorNotFoundException()
        : base(Message)
    {
    }

    public AuthorNotFoundException(string message)
        : base(message)
    {
    }
}
