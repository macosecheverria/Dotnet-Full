using Api_Author.Dtos.Authors;

namespace Api_Author.Dtos.Books;

public class BookDto {
    public int Id { get; set; }

    public  string Title { get; set; }

    public DateTime PublishDate { get; set; }

    // public required List<CommentDto> Comments { get; set; }
}