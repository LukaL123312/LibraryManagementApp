using System.Text.Json.Serialization;

using LibraryApp.Domain.BookEntity;

namespace LibraryApp.Domain.AuthorEntity;

public record Author
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    [JsonIgnore]
    public List<Book> Books { get; set; }
}
