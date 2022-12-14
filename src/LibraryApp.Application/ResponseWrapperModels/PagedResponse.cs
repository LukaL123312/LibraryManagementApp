namespace LibraryApp.Application.ResponseWrapperModels;

public class PagedResponse<T>
{
    public PagedResponse()
    {

    }

    public PagedResponse(T? data)
    {
        Data = data;
    }

    public T? Data { get; set; }
}

