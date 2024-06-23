using Api_Author.Dtos.Books;

namespace Api_Author.Dtos.Authors;

public class AuthorDtoWithBook :AuthorDto {
    public List<BookDto> Books { get; set; }

}