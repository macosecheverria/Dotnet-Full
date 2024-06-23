using Api_Author.Dtos.Authors;

namespace Api_Author.Dtos.Books;

public class BookDtoWithAuthor : BookDto {
    public List<AuthorDto> Authors { get; set; }
}